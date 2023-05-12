using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using TestTask.ViewModels;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для SelectQueryOutput.xaml
    /// </summary>
    public partial class SelectQueryOutput : Window
    {
        public SelectQueryOutput()
        {
            InitializeComponent();
            DataContext = new ViewModelSelectQueryOutput();
        }
    }
}