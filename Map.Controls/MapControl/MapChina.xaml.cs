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
using System.ComponentModel;

namespace Map.Controls
{
    /// <summary>
    /// MapChina.xaml 的交互逻辑
    /// </summary>
    public partial class MapChina : UserControl
    {
        private bool isMouseLeftButtonDown = false;
        Point previousMousePoint = new Point(0, 0);
        public MapChina()
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (object obj in cityView.Children)
            {
                if (obj is Path)
                {
                    if ((obj as Path).Name != "Path_0")
                    {
                        (obj as Path).MouseLeftButtonUp += new MouseButtonEventHandler(MapChina_MouseLeftButtonUp);
                    }
                }
            }
            MapFlow mp = new MapFlow(spnl_Radio, grid_Animation, this, (Style)FindResource("ParticlePathStyle"));
        }

        // 定义事件属性
        public static readonly RoutedEvent MyButtonClickEvent =
             EventManager.RegisterRoutedEvent("MapClick", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(Path));
        void MapChina_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                 new RoutedPropertyChangedEventArgs<object>(sender, sender, MyButtonClickEvent);
            this.RaiseEvent(arg);
        }

        [Description("点击时发生")]
        public event RoutedPropertyChangedEventHandler<object> MapClick
        {
            add
            {
                this.AddHandler(MyButtonClickEvent, value);
            }

            remove
            {
                this.RemoveHandler(MyButtonClickEvent, value);
            }
        }

    }

}
