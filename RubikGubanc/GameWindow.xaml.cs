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
using RubikGubancViewModel;

namespace RubikGubanc
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        RubikGubancVM rgvm;
        public GameWindow()
        {
            InitializeComponent();
            rgvm = new RubikGubancVM();
            DataContext = rgvm;
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            rgvm.SolveOne();
            SecondSolutionButton.Visibility = Visibility.Visible;
        }

        private void ShuffleClick(object sender, RoutedEventArgs e)
        {
            rgvm.SetRandomOrder();
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SecondSolutionClick(object sender, RoutedEventArgs e)
        {
            rgvm.SolveTwo();
        }

        private void RulesClick(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.CheckingWindowOpen<RulesWindow>())  //  mindig csak 1 ilyen ablak legyen megnyitva
            {
                RulesWindow rw = new RulesWindow();
                rw.Show();
            }
        }
    }
}
