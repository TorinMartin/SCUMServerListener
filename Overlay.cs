﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

		public bool isCreated = false;

		public bool disableBackground = true;
		public bool overlayAllWindows = false;
		public bool showName, showPlayers, showTime, showPing;

		public string onlineColor, offlineColor, bgColor;

		private IntPtr hWnd = IntPtr.Zero;
		string windowName = "SCUM  ";
		string className = "UnrealWindow";

		private Process gameProcess = null;

		private string name, players, status, time, ping;

		int x = 20, y = 20;

		IDictionary<string, string> settings = new Dictionary<string, string>();

		RECT rect;

		public struct RECT
		{
			public int left, top, right, bottom;
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
		public string Players
		{
			get { return this.players; }
			set { this.players = value; }
		}
		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public string Time
		{
			get { return this.time; }
			set { this.time = value; }
		}
		public string Ping
		{
			get { return this.ping; }
			set { this.ping = value; }
		}
		public int X
		{
			get { return this.x; }
			set { this.x = value; }
		}
		public int Y
		{
			get { return this.y; }
			set { this.y = value; }
		}

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool CloseHandle(IntPtr hHandle);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

		public Overlay()
		{
			_brushes = new Dictionary<string, SolidBrush>();
			_fonts = new Dictionary<string, Font>();

			var gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
			};

			settings = SettingsManager.LoadAllSettings();

			disableBackground = bool.Parse(settings["disableBackground"]);
			overlayAllWindows = bool.Parse(settings["overlayAllWindows"]);

			showName = bool.Parse(settings["showName"]);
			showPlayers = bool.Parse(settings["showPlayers"]);
			showTime = bool.Parse(settings["showTime"]);
			showPing = bool.Parse(settings["showPing"]);

			this.x = int.Parse(settings["posx"]);
			this.y = int.Parse(settings["posy"]);

			onlineColor = settings["onlineColor"].ToLower();
			offlineColor = settings["offlineColor"].ToLower();
			bgColor = settings["bgColor"].ToLower();

			try
			{
				if (!overlayAllWindows)
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

			if (showName)
				infoText += name;

			if (showPlayers)
				infoText += "\nPlayers: " + players;

			if (showTime)
				infoText += "\nTime: " + time;

			if(showPing)
				infoText += "\nPing: " + ping;

			gfx.ClearScene();

			if (!disableBackground)
			{
				if (status == "online")
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes[onlineColor], _brushes[bgColor], this.x, this.y, infoText);
				else
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes[onlineColor], _brushes[bgColor], this.x, this.y, infoText);
			}
			else
			{
				if (status == "online")
					gfx.DrawText(_fonts["consolas"], _brushes[onlineColor], this.x, this.y, infoText);
				else
					gfx.DrawText(_fonts["consolas"], _brushes[offlineColor], this.x, this.y, infoText);
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

		public bool HasProcessExited()
		{
			return this.gameProcess.HasExited;
		}

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

		~Overlay()
		{
			Dispose(false);
		}

		#region IDisposable Support
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (isCreated)
			{
				if (hWnd != IntPtr.Zero)
					CloseHandle(hWnd);

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
