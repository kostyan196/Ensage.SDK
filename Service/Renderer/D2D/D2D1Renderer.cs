// <copyright file="D2D1Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using System;
    using System.ComponentModel.Composition;
    using System.Runtime.CompilerServices;

    using global::Ensage.SDK.Geometry;

    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;
    using SharpDX.Mathematics.Interop;

    public class D2D1Renderer : ID2D1Renderer
    {
        [Import(typeof(ID2D1BrushContainer))]
        public ID2D1BrushContainer Brushes { get; private set; }

        [Import(typeof(ID2D1Context))]
        public ID2D1Context Context { get; private set; }

        [Import(typeof(ID2D1FontContainer))]
        public ID2D1FontContainer Fonts { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawBox2D(
            int x,
            int y,
            int width,
            int height,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush interiorBrush)
        {
            this.Context.Target.DrawRectangle(new RawRectangleF(x, y, x + width, y + height), brush, stroke);
            this.Context.Target.FillRectangle(
                new RawRectangleF(x + stroke, y + stroke, x + width - stroke, y + height - stroke),
                interiorBrush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawBox3D(
            int x,
            int y,
            int width,
            int height,
            int length,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush interiorBrush)
        {
            var target = this.Context.Target;

            var first = new RawRectangleF(x, y, x + width, y + height);
            var second = new RawRectangleF(x + length, y - length, first.Right + length, first.Bottom - length);

            var lineStart = new RawVector2(x, y);
            var lineEnd = new RawVector2(second.Left, second.Top);

            target.DrawRectangle(first, brush, stroke);
            target.DrawRectangle(second, brush, stroke);

            target.FillRectangle(first, brush);
            target.FillRectangle(second, brush);

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.X += width;
            lineEnd.X = lineStart.X + length;

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.Y += height;
            lineEnd.Y += height;

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.X -= width;
            lineEnd.X -= width;

            target.DrawLine(lineStart, lineEnd, brush, stroke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawCircle(int x, int y, int radius, float stroke, SolidColorBrush brush)
        {
            this.Context.Target.DrawEllipse(new Ellipse(new RawVector2(x, y), radius, radius), brush, stroke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawLine(int startX, int startY, int endX, int endY, float stroke, SolidColorBrush brush)
        {
            this.Context.Target.DrawLine(new RawVector2(startX, startY), new RawVector2(endX, endY), brush, stroke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawPolygon(Polygon polygon)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRectangle(int x, int y, int width, int height, float stroke, SolidColorBrush brush)
        {
            this.Context.Target.DrawRectangle(new RawRectangleF(x, y, x + width, y + height), brush, stroke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawRectangle3D(
            int x,
            int y,
            int width,
            int height,
            int length,
            float stroke,
            SolidColorBrush brush)
        {
            var target = this.Context.Target;

            var first = new RawRectangleF(x, y, x + width, y + height);
            var second = new RawRectangleF(x + length, y - length, first.Right + length, first.Bottom - length);

            var lineStart = new RawVector2(x, y);
            var lineEnd = new RawVector2(second.Left, second.Top);

            target.DrawRectangle(first, brush, stroke);
            target.DrawRectangle(second, brush, stroke);

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.X += width;
            lineEnd.X = lineStart.X + length;

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.Y += height;
            lineEnd.Y += height;

            target.DrawLine(lineStart, lineEnd, brush, stroke);

            lineStart.X -= width;
            lineEnd.X -= width;

            target.DrawLine(lineStart, lineEnd, brush, stroke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawText(int x, int y, string text, TextFormat font, SolidColorBrush brush)
        {
            var layout = new TextLayout(this.Context.DirectWrite, text, font, float.MaxValue, float.MaxValue);
            this.Context.Target.DrawTextLayout(new RawVector2(x, y), layout, brush);
            layout.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FillCircle(int x, int y, int radius, SolidColorBrush brush)
        {
            this.Context.Target.FillEllipse(new Ellipse(new RawVector2(x, y), radius, radius), brush);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FillRectangle(int x, int y, int width, int height, SolidColorBrush brush)
        {
            this.Context.Target.FillRectangle(new RawRectangleF(x, y, x + width, y + height), brush);
        }
    }
}