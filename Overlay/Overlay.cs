using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using SCUMServerListener.UI;

namespace SCUMServerListener.Overlay
{
    public class Overlay : IDisposable
    {
        private readonly GraphicsWindow _window;
        private readonly Dictionary<string, SolidBrush> _brushes;
        private readonly Dictionary<string, Font> _fonts;
        private bool _disposedValue;
        private IntPtr _hWnd = IntPtr.Zero;
        private readonly string _windowName = "SCUM  ";
        private readonly string _className = "UnrealWindow";
        private readonly Process _gameProcess;
        private readonly Gui _gui;

        public readonly bool IsCreated;
        public string Name, Players, Status, Time, Ping;
        public int X = AppSettings.Instance.PositionX, Y = AppSettings.Instance.PositionY;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out MOUSE mouse);

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(Keys vKey);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);

        public Overlay(Gui gui)
        {
            RECT rect;
            _gui = gui;
            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();

            var gfx = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
            };

            try
            {
                if (!AppSettings.Instance.OverlayAllWindows)
                {
                    _hWnd = FindWindow(_className, _windowName);
                    GetWindowRect(_hWnd, out rect);
                    _gameProcess = GetGameProcess();

                    _window = new StickyWindow(_hWnd, gfx)
                    {
                        FPS = 60,
                        IsTopmost = true,
                        IsVisible = true,
                        X = rect.left,
                        Y = rect.top
                    };
                }
                else
                {
                    // OVERLAY EVERY WINDOW:
                    var width = Screen.PrimaryScreen.Bounds.Width;
                    var height = Screen.PrimaryScreen.Bounds.Height;

                    _window = new GraphicsWindow(0, 0, width, height, gfx)
                    {
                        FPS = 60,
                        IsTopmost = true,
                        IsVisible = true,
                    };
                }

                _window.DestroyGraphics += _window_DestroyGraphics;
                _window.DrawGraphics += _window_DrawGraphics;
                _window.SetupGraphics += _window_SetupGraphics;

                IsCreated = true;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("SCUM is not running!", "SCUM.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
            }

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
            _brushes["blue"] = gfx.CreateSolidBrush(0, 0, 255);
            _brushes["yellow"] = gfx.CreateSolidBrush(255, 255, 0);
            _brushes["cyan"] = gfx.CreateSolidBrush(0, 255, 255);
            _brushes["dark"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);
            _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);

            if (e.RecreateResources) return;

            _fonts["arial"] = gfx.CreateFont("Arial", 12);
            _fonts["consolas"] = gfx.CreateFont("Consolas", 14);
        }

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            foreach (var pair in _brushes) pair.Value.Dispose();
            foreach (var pair in _fonts) pair.Value.Dispose();
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            var infoText = "";

            if (AppSettings.Instance.ShowName)
                infoText += Name;

            if (AppSettings.Instance.ShowPlayers)
                infoText += "\nPlayers: " + Players;

            if (AppSettings.Instance.ShowTime)
                infoText += "\nTime: " + Time;

            if(AppSettings.Instance.ShowPing)
                infoText += "\nPing: " + Ping;

            gfx.ClearScene();

            var drawColor = Status == "online" ? _brushes[AppSettings.Instance.OnlineColor] : _brushes[AppSettings.Instance.OfflineColor];

            if (!AppSettings.Instance.DisableBackground)
            {
                gfx.DrawTextWithBackground(_fonts["consolas"], drawColor, _brushes[AppSettings.Instance.BackgroundColor], X, Y, infoText);
            }
            else
            {
                gfx.DrawText(_fonts["consolas"], drawColor, X, Y, infoText);
            }
        }

        public void Run()
        {
            if (!IsCreated) return;
            _window.Create();
            _window.Join();
        }

        public bool HasProcessExited() => _gameProcess.HasExited;

        // Credit https://stackoverflow.com/questions/7162834/determine-if-current-application-is-activated-has-focus
        private bool ApplicationIsActivated()
        {
            var activehWnd = GetForegroundWindow();
            if (activehWnd == IntPtr.Zero) return false;

            GetWindowThreadProcessId(activehWnd, out var activeProcId);

            return activeProcId == _gameProcess.Id;
        }

        public void SetWindowVisibility()
        {
            _window.IsVisible = ApplicationIsActivated();
        }

        private static Process GetGameProcess()
        {
            var procList = Process.GetProcessesByName("SCUM");
            return procList.Length > 0 ? procList[0] : null;
        }

        public void DragOverlay()
        {
            if (!AppSettings.Instance.OverlayAllWindows && _hWnd != IntPtr.Zero)
            {
                SetForegroundWindow(_hWnd);
            }

            Task.Run(() =>
            {
                var isDragging = true;
                while (isDragging)
                {
                    GetCursorPos(out var mouse);
                    X = mouse.X;
                    Y = mouse.Y;
                    if ((GetAsyncKeyState(Keys.LButton) & 0x8000) == 0x8000)
                    {
                        isDragging = false;
                    }
                }
            });
            
            AppSettings.Instance.PositionX = X;
            AppSettings.Instance.PositionY = Y;
            Configuration.Save(AppSettings.Instance);
            _gui.toggle_overlay_btn(true);
        }

        ~Overlay()
        {
            Dispose(false);
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!IsCreated) return;
            if (_hWnd != IntPtr.Zero)
            {
                try
                {
                    CloseHandle(_hWnd);
                } 
                catch (SEHException)
                {
                    // TODO: log error
                    _hWnd = IntPtr.Zero;
                } 
            }

            if (!_disposedValue)
            {
                _window.Dispose();
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
