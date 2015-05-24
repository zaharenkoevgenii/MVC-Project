using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MvcPL.Infrastructura
{
    public class CaptchaImage : IDisposable
    {
        public const string CaptchaValueKey = "CaptchaImageText";

        public string Text
        {
            get { return _text; }
        }

        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly string _text;
        private string _familyName;

        private readonly Random _random = new Random();

        public CaptchaImage(string s, int width, int height)
        {
            _text = s;
            SetDimensions(width, height);
            GenerateImage();
        }

        public CaptchaImage(string s, int width, int height, string familyName)
        {
            _text = s;
            SetDimensions(width, height);
            SetFamilyName(familyName);
            GenerateImage();
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Image.Dispose();
        }

        private void SetDimensions(int aWidth, int aHeight)
        {
            if (aWidth <= 0)
                throw new ArgumentOutOfRangeException("aWidth", aWidth, "Argument out of range, must be greater than zero.");
            if (aHeight <= 0)
                throw new ArgumentOutOfRangeException("aHeight", aHeight, "Argument out of range, must be greater than zero.");
            Width = aWidth;
            Height = aHeight;
        }

        private void SetFamilyName(string aFamilyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                var font = new Font(aFamilyName, 12F);
                _familyName = aFamilyName;
                font.Dispose();
            }
            catch (Exception)
            {
                _familyName = FontFamily.GenericSerif.Name;
            }
        }

        private void GenerateImage()
        {
            var bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width, Height);

            var hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);

            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            do
            {
                fontSize--;
                font = new Font(_familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(_text, font);
            } while (size.Width > rect.Width);

            var format = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

            var path = new GraphicsPath();
            path.AddString(_text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            const float v = 4F;
            PointF[] points =
            {
                new PointF(_random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
                new PointF(rect.Width - _random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
                new PointF(_random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v),
                new PointF(rect.Width - _random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v)
            };
            var matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            g.FillPath(hatchBrush, path);

            var m = Math.Max(rect.Width, rect.Height);
            for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                var x = _random.Next(rect.Width);
                var y = _random.Next(rect.Height);
                var w = _random.Next(m / 50);
                var h = _random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            Image = bitmap;
        }
    }
}