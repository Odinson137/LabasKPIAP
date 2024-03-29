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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba35.Task1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button1.Visibility = Visibility.Collapsed;
            AnimateWidthChange();
        }

        private void AnimateWidthChange()
        {
            Random random = new Random();
            double newWidth = random.Next(200, 600); 
            double currentWidth = ActualWidth;

            // Создаем анимацию изменения ширины
            DoubleAnimation animation = new DoubleAnimation(currentWidth, newWidth, TimeSpan.FromSeconds(1));

            //animation.Completed += (s, e) =>
            //{
            //    // Когда анимация завершена, вызываем ее снова для создания эффекта бесконечной анимации
            //    AnimateWidthChange();
            //};

            BeginAnimation(WidthProperty, animation);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button2.Visibility = Visibility.Collapsed;
            AnimateWidthChange();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button3.Visibility = Visibility.Collapsed;
            AnimateWidthChange();
        }
    }
}
