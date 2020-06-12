using System;
using System.Runtime.InteropServices;

namespace loader
{
	public static class Win32
	{
		[DllImport("user32", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool GetComboBoxInfo(IntPtr hwnd, out Win32.COMBOBOXINFO info);

		[DllImport("user32", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool InvalidateRect(IntPtr hwnd, IntPtr rect, bool bErase);

		[DllImport("user32", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, Win32.RedrawWindowFlags flags);

		public struct COMBOBOXINFO
		{
			public int size;

			public Win32.RECT item;

			public Win32.RECT button;

			public int state;

			public IntPtr comboHwnd;

			public IntPtr itemHwnd;

			public IntPtr listHwnd;
		}

		public struct RECT
		{
			public int left;

			public int top;

			public int right;

			public int bottom;
		}

		[Flags]
		public enum RedrawWindowFlags : uint
		{
			Invalidate = 1,
			InternalPaint = 2,
			Erase = 4,
			Validate = 8,
			NoInternalPaint = 16,
			NoErase = 32,
			NoChildren = 64,
			AllChildren = 128,
			UpdateNow = 256,
			EraseNow = 512,
			Frame = 1024,
			NoFrame = 2048
		}
	}
}