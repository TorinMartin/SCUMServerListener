﻿using System;
using System.Collections.Generic;
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

		public bool disableBackground = false;

		private string name = "";
		private string players = "";
		private string status = "";
		private string ping = "";

		int x = 20;
		int y = 20;

		RECT rect;

		public struct RECT
		{
			public int left, top, right, bottom;
		}

		public void SetName(string arg) { this.name = arg; }
		public void SetPlayers(string arg) { this.players = arg; }
		public void SetStatus(string arg) { this.status = arg; }
		public void SetPing(string arg) { this.ping = arg; }

		public void SetX(int arg) { this.x = arg; }
		public void SetY(int arg) { this.y = arg; }
		public int GetX() { return this.x; }
		public int GetY() { return this.y; }

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);


		public Overlay()
		{
			_brushes = new Dictionary<string, SolidBrush>();
			_fonts = new Dictionary<string, Font>();

			var gfx = new Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true,
				WindowHandle = IntPtr.Zero
			};

			string windowName = "SCUM  ";
			string className = "UnrealWindow";
			var hWnd = FindWindow(className, windowName);

			GetWindowRect(hWnd, out rect);

			disableBackground = SettingsManager.LoadTextPref();
			int[] pos = SettingsManager.LoadPositions();
			this.x = pos[0];
			this.y = pos[1];

			try
			{
				if (!SettingsManager.LoadWindowPref())
				{
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
					_window = new GraphicsWindow(0, 0, 1920, 1080, gfx)
					{
						FPS = 60,
						IsTopmost = true,
						IsVisible = true
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
			_brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
			_brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
			_brushes["background"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);

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

			var infoText = name + "\nPlayers: " + players + "\nPing: " + ping;

			gfx.ClearScene();

			if (!disableBackground)
			{
				if (status == "online")
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes["green"], _brushes["background"], this.x, this.y, infoText);
				else
					gfx.DrawTextWithBackground(_fonts["consolas"], _brushes["red"], _brushes["background"], this.x, this.y, infoText);
			}
			else
			{
				if (status == "online")
					gfx.DrawText(_fonts["consolas"], _brushes["green"], this.x, this.y, infoText);
				else
					gfx.DrawText(_fonts["consolas"], _brushes["red"], this.x, this.y, infoText);
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
