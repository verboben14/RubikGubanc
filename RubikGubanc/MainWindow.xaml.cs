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

namespace RubikGubanc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GameClick(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();

            gw.Show();
        }

        private void RulesClick(object sender, RoutedEventArgs e)
        {
            RulesWindow rw = new RulesWindow();

            rw.Show();
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
