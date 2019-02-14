using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace KM.Bosman.WPFClient.MarkupExtensions
{
    public class MyMarkupExtension : MarkupExtension
    {
        public double Size { get; set; }

        public MyMarkupExtension()
        {

        }

        public MyMarkupExtension(double size)
            : this()
        {
            this.Size = size;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
