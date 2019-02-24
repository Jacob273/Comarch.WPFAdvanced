using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KM.Bosman.WPFClient.Controls
{
    public class HorizontalAxis : FrameworkElement
    {



        public float StrokeThinkness
        {
            get { return (float)GetValue(StrokeThinknessProperty); }
            set { SetValue(StrokeThinknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThinkness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThinknessProperty =
            DependencyProperty.Register("StrokeThinkness", typeof(float), typeof(HorizontalAxis),
                new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender));




        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen pen = new Pen(new SolidColorBrush(Colors.Red), StrokeThinkness);

            drawingContext.DrawLine(pen, new Point(0, ActualHeight / 2), new Point(100, ActualHeight / 2));

        }
    }
}
