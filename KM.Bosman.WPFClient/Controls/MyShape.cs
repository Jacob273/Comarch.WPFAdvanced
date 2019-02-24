using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KM.Bosman.WPFClient.Controls
{
    public class MyShape : Shape
    {
        protected override Geometry DefiningGeometry => GetGeometry();

        private Geometry GetGeometry()
        {
            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext context = streamGeometry.Open())
            {
                context.BeginFigure(new Point(0, 0), true, true);
                context.LineTo(new Point(250, 250), false, true);
                context.LineTo(new Point(350, 150), true, true);
            }

            streamGeometry.Freeze();

            return streamGeometry;
        }
    }

    
}
