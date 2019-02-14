using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KM.Bosman.WPFClient.Behaviors
{
    // Install-Package Microsoft.Xaml.Behaviors.Wpf

    public class MyButtonBehavior : Behavior<Button>
    {
        public Color Color { get; set; }

        // public string Text { get; set; }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), 
                typeof(MyButtonBehavior), 
                new PropertyMetadata(default(string)));




        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        }

        private void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.AssociatedObject.Background = new SolidColorBrush(Color);
            this.AssociatedObject.Content = Text;
        }
    }
}
