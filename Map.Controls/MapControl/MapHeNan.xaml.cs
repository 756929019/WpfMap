using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;

namespace Map.Controls
{
    /// <summary>
    /// MapHeNan.xaml 的交互逻辑
    /// </summary>
    public partial class MapHeNan : UserControl
    {
        private bool isMouseLeftButtonDown = false;
        Point previousMousePoint = new Point(0, 0);
        public MapHeNan()
        {
            InitializeComponent();
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.sfr.CenterX = mymap.ActualWidth / 2;
            this.sfr.CenterY = mymap.ActualHeight / 2;
            if (sfr.ScaleX < 0.3 && sfr.ScaleY < 0.3 && e.Delta < 0)
            {
                return;
            }
            if ((e.Delta < 0 && sfr.ScaleX > 0.2) || (e.Delta > 0 && sfr.ScaleX < 5.0))
            {
                sfr.ScaleX += (double)e.Delta / 480;
                sfr.ScaleY += (double)e.Delta / 480;
            }
        }

        private void mymap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseLeftButtonDown = true;
            previousMousePoint = e.GetPosition(mymap);
        }

        private void mymap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseLeftButtonDown)
            {
                Point position = e.GetPosition(mymap);
                tlt.X += position.X - this.previousMousePoint.X;
                tlt.Y += position.Y - this.previousMousePoint.Y;
            }
        }

        private void mymap_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseLeftButtonDown = false;
        }

        private void mymap_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseLeftButtonDown = false;
        }

        //private void Path_MouseMove(object sender, MouseEventArgs e)
        //{
        //    (sender as Path).Fill = new SolidColorBrush(Colors.Red);
        //}

        //private void Path_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    (sender as Path).Fill = new SolidColorBrush(Colors.White);
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach (object obj in mymap.Children)
            //{
            //    if (obj is Path)
            //    {
            //        if ((obj as Path).Name != "Path_0")
            //        {
            //            (obj as Path).MouseMove += new MouseEventHandler(Path_MouseMove);
            //            (obj as Path).MouseLeave += new MouseEventHandler(Path_MouseLeave);
            //        }
            //    }
            //}

            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation(
                0.2, 0.5, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleX)"));//
            storyboard.Children.Add(doubleAnimation);

            doubleAnimation = new DoubleAnimation(
                0.5, 0.8, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleX)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();

            doubleAnimation = new DoubleAnimation(
               0.8, 1, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleX)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();

            doubleAnimation = new DoubleAnimation(
                0.2, 0.5, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleY)"));//
            storyboard.Children.Add(doubleAnimation);

            doubleAnimation = new DoubleAnimation(
                0.5, 0.8, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleY)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();

            doubleAnimation = new DoubleAnimation(
               0.8, 1, new Duration(TimeSpan.FromMilliseconds(500)));
            Storyboard.SetTarget(doubleAnimation, sfr);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(ScaleY)"));
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

    }
}
