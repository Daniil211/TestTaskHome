using Microsoft.SqlServer.Server;
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
using System.Windows.Shapes;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для NavigationMenu.xaml
    /// </summary>
    public partial class NavigationMenu : Window
    {
        public NavigationMenu()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var form = new GetWeatherData();
            form.Show();
            this.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var form = new SelectQueryOutput();
            form.Show();
            this.Close();
        }
    }
}
