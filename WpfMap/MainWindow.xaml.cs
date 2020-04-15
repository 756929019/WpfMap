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
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Media.Animation;
using MapData;

namespace WpfMap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = GetData(12);

            try
            {
                MapGrid.Children.Clear();
                string fullName = "Map.Controls.MapChina";//命名空间.类型名
                UserControl map = (UserControl)Assembly.Load("Map.Controls").CreateInstance(fullName);
                map.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                map.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                map.Margin = new Thickness(0);
                if (map is Map.Controls.MapChina)
                {
                    (map as Map.Controls.MapChina).MapClick += new RoutedPropertyChangedEventHandler<object>(MainWindow_MapClick);
                }
                MapGrid.Children.Add(map);

            }
            catch
            {
                ;
            }
        }
        public static List<ChartData> GetData(int dataSize)
        {
            Random rnd = new Random(0);
            var result = new List<ChartData>();

            for (int i = 0; i < dataSize; i++)
            {
                result.Add(new ChartData()
                {
                    Category = i,
                    Value = rnd.Next(1, 100),
                    Color = new SolidColorBrush(
                        Color.FromArgb(255, (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256)))
                });
            }

            return result;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MapGrid.Children.Clear();
                ListBoxItem lbi = listBox1.SelectedItem as ListBoxItem;

                string fullName = "Map.Controls." + lbi.Tag.ToString();//命名空间.类型名
                UserControl map = (UserControl)Assembly.Load("Map.Controls").CreateInstance(fullName);
                map.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                map.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                map.Margin = new Thickness(0);
                if (map is Map.Controls.MapChina)
                {
                    (map as Map.Controls.MapChina).MapClick += new RoutedPropertyChangedEventHandler<object>(MainWindow_MapClick);
                }
                MapGrid.Children.Add(map);

            }
            catch
            {
                ;
            }
        }

        void MainWindow_MapClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is Path)
            {

            }
            MapGrid.Children.Clear();
            UserControl map = new Map.Controls.MapHeNan();
            map.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            map.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            map.Margin = new Thickness(0);
            map.Name = "CMap";
            map.Loaded += new RoutedEventHandler(map_Loaded);

            MapGrid.Children.Add(map);
        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {
            //Storyboard storyboard = new Storyboard();
            ////添加X轴方向的动画  
            //ThicknessAnimationUsingKeyFrames doubleAnimation = new ThicknessAnimationUsingKeyFrames();
            //EasingThicknessKeyFrame ef = new EasingThicknessKeyFrame();
            //ef.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));
            //ef.Value = new Thickness(150);
            //doubleAnimation.KeyFrames.Add(ef);
            //ef = new EasingThicknessKeyFrame();
            //ef.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2));
            //ef.Value = new Thickness(0);
            //doubleAnimation.KeyFrames.Add(ef);
            //Storyboard.SetTarget(doubleAnimation, MapGrid);
            //Storyboard.SetTargetProperty(doubleAnimation, new System.Windows.PropertyPath(MarginProperty));//new PropertyPath("(Canvas.Left)")--(FrameworkElement.Margin)
            //storyboard.Children.Add(doubleAnimation);
            //storyboard.Begin();

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImageBrush b = new ImageBrush();
            b.ImageSource = (sender as Image).Source;
            b.Stretch = Stretch.Fill;
            RootGrid.Background = b;
        }
    }
}
