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

namespace Laba35.Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AnimateMarginLeft();
        }


        public void AnimateMarginLeft()
        {
            Thickness currentMargin = Circle.Margin;
            
            double day = GetPercentageOfDayPassed();
            
            Thickness newMargin = new Thickness(800 * day, currentMargin.Top, currentMargin.Right, currentMargin.Bottom);

            ThicknessAnimation animation = new ThicknessAnimation()
            {
                From = currentMargin,
                To = newMargin,
                Duration = TimeSpan.FromSeconds(1)
            };

            Circle.BeginAnimation(FrameworkElement.MarginProperty, animation);
        }

        public static double GetPercentageOfDayPassed()
        {
            DateTime currentTime = DateTime.Now;
            DateTime startOfDay = currentTime.Date;
            TimeSpan timePassed = currentTime - startOfDay;
            double percentage = (timePassed.TotalSeconds / 86400);
            return percentage;
        }
    }
}
