using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace SCUMServerListener
{
    public class Overlay : IDisposable
    {
		private readonly GraphicsWindow _window;
		private readonly Dictionary<string, SolidBrush> _brushes;
		private readonly Dictionary<string, Font> _fonts;
		private bool disposedValue;
		private bool isDragging = false;
		private IntPtr hWnd = IntPtr.Zero;
		private string windowName = "SCUM  ";
		private string className = "UnrealWindow";
		private Process gameProcess;
		private GUI gui;

		public bool isCreated = false;
		public string Name, Players, Status, Time, Ping;
		public int X = AppSettings.Instance.PositionX, Y = AppSettings.Instance.PositionY;

		RECT rect;

		private struct RECT
		{
			public int left, top, right, bottom;
		}
		private struct MOUSE
		{
			public int X;
			public int Y;
		}

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
		private static extern int GetAsyncKeyState(System.Windows.Forms.Keys vKey);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

		public Overlay(GUI gui)
		{
			this.gui = gui;
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
					hWnd = FindWindow(className, windowName);
					GetWindowRect(hWnd, out rect);
					this.gameProcess = GetGameProcess();

					_window = new StickyWindow(hWnd, gfx)
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

				isCreated = true;
			}
			catch (System.ArgumentOutOfRangeException) { MessageBox.Show("SCUM is not running!", "SCUM.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

			if (!AppSettings.Instance.DisableBackground)
			{
				if (Status == "online")
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes[AppSettings.Instance.OnlineColor], _brushes[AppSettings.Instance.BackgroundColor], X, Y, infoText);
				else
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes[AppSettings.Instance.OnlineColor], _brushes[AppSettings.Instance.BackgroundColor], X, Y, infoText);
			}
			else
			{
				if (Status == "online")
					gfx.DrawText(_fonts["consolas"], _brushes[AppSettings.Instance.OnlineColor], X, Y, infoText);
				else
					gfx.DrawText(_fonts["consolas"], _brushes[AppSettings.Instance.OfflineColor], X, Y, infoText);
			}
		}

		public void Run()
		{
			if (isCreated)
			{
				_window.Create();
				_window.Join();
			}
		}

		public bool HasProcessExited() => this.gameProcess.HasExited;

		// Credit https://stackoverflow.com/questions/7162834/determine-if-current-application-is-activated-has-focus
		private bool ApplicationIsActivated()
		{
			var activehWnd = GetForegroundWindow();
			if (activehWnd == IntPtr.Zero)
				return false;

			int activeProcId;
			GetWindowThreadProcessId(activehWnd, out activeProcId);

			return activeProcId == gameProcess.Id;
		}

		public void SetWindowVisibility()
		{
			_window.IsVisible = ApplicationIsActivated();
		}

		private Process GetGameProcess()
		{
			Process[] procList = Process.GetProcessesByName("SCUM");
			if (procList.Length > 0)
			{
				return procList[0];
			}
			return null;
		}

		public void DragOverlay()
        {
			MOUSE mouse;
			isDragging = true;

			if (!AppSettings.Instance.OverlayAllWindows && hWnd != IntPtr.Zero)
				SetForegroundWindow(hWnd);

			var dragThread = new Thread(() =>
			{
				while (this.isDragging)
				{
					GetCursorPos(out mouse);
					this.X = mouse.X;
					this.Y = mouse.Y;
					if (GetAsyncKeyState(Keys.LButton) > 0) {
						break;
					}
				}

				AppSettings.Instance.PositionX = X;
				AppSettings.Instance.PositionY = Y;
				Configuration.Save(AppSettings.Instance);
				Action del = delegate() { gui.toggle_overlay_btn(true); };
				gui.InvokeOnUIThread(del);
			});

			dragThread.Start();
        }

		~Overlay()
		{
			Dispose(false);
		}

		#region IDisposable Support

		protected virtual void Dispose(bool disposing)
		{
			if (isCreated)
			{
				if (hWnd != IntPtr.Zero)
                {
					try
                    {
						CloseHandle(hWnd);
					} 
					catch (SEHException)
                    {
						// TODO: log error
						hWnd = IntPtr.Zero;
                    } 
                }

				if (!disposedValue)
				{
					_window.Dispose();

					disposedValue = true;
				}
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
