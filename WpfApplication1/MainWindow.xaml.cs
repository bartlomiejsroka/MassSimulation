using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        double? mass, friction=2;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        static  int tick = 0;
      static  double xPos = 0;
        double oldValue = 0;
        bool inicial = false, released = false;
        static Rectangle masObj = new Rectangle()
        {
            Height = 20,
            Width = 20,
            Stroke = System.Windows.Media.Brushes.Black,
            Fill = System.Windows.Media.Brushes.Red,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            Margin  = new Thickness(0, 135, 0, 0),

    };
        DoubleAnimation da = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(1)));
        TranslateTransform t = new TranslateTransform();
        
        public MainWindow()
        {
            InitializeComponent();

            canvas.Children.Add(masObj);
            
        }
        Cinematic cinematic;
        private void MassApply_Click(object sender, RoutedEventArgs e)
        {
            
            mass = Parsing(MassTextBox);
            friction = Parsing(FrictionTextBox);
            if (mass != null && friction != null)
            {

               
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                dispatcherTimer.Start();
                cinematic = new Cinematic((double)mass, (double)friction);
            }
            
        }

        private double? Parsing(TextBox t)
        {
           
            double value;
            try
            {
                value = Double.Parse(t.Text.Replace('.',','));
                return value;
            }
            catch (Exception e) { return null; };

        }

        private void PushButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

       

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {



            if(PushButton.IsPressed)
            {
                if (released)
                {
                    tick = 0;
                    oldValue = xPos;
                }
                tick++;
                xPos = cinematic.CalculatePos(0.1 * tick) * 5  + oldValue;
                canvas.Children.RemoveAt(0);
                masObj.Margin = new Thickness(xPos, 135, 0, 0);
                canvas.Children.Add(masObj);
                inicial = true;
                released = false;
            }

            if (!PushButton.IsPressed && inicial && ! released)
            {
               
               
                    oldValue = xPos;
                    tick = 0;
                    released = true;
               
            }

            if (released && !PushButton.IsPressed && inicial)
            {

                xPos = cinematic.NoforcePosision(0.1 * tick) * 5 + oldValue;
                canvas.Children.RemoveAt(0);
                masObj.Margin = new Thickness(xPos, 135, 0, 0);
                canvas.Children.Add(masObj);
                tick++;
            }
        }




    }
   
}
