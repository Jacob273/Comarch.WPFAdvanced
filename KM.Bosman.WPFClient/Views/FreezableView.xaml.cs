using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KM.Bosman.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for FreezableView.xaml
    /// </summary>
    public partial class FreezableView : Page
    {
        public FreezableView()
        {
            InitializeComponent();


            // klasa Freezable zapewnia:
            // - wyłączenie śledzenia zmian
            // - bezpieczne renderowanie w przypadku wielu wątków

            MyFreezable myFreezable = new MyFreezable();
            myFreezable.Changed += MyFreezable_Changed;

            myFreezable.Size = 100;
            myFreezable.Text = "Hello";

            if (myFreezable.CanFreeze)
            {
                myFreezable.Freeze();
            }

            myFreezable.Size = 100;

            // ..

            if (myFreezable.IsFrozen)
            {
                MyFreezable copy = myFreezable.Clone() as MyFreezable;

                copy.Text = "Hello Copy";
            }
            else
            {
                myFreezable.Text = "Hello World";
            }
            

        }

        private void MyFreezable_Changed(object sender, EventArgs e)
        {
            
        }
    }

    public class MyFreezable : Freezable
    {
        public int Size { get; set; }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MyFreezable), new PropertyMetadata(""));



        protected override Freezable CreateInstanceCore()
        {
            return new MyFreezable();
        }
    }
}
