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
            if(!CheckingWindowOpen<GameWindow>())       //  Ellenőrzi, hogy van-e már ilyen ablak megnyitva
            {
                GameWindow gw = new GameWindow();       //  Ha nincs, újat hozunk létre
                gw.Show();
            }
        }

        private void RulesClick(object sender, RoutedEventArgs e)
        {
            if (!CheckingWindowOpen<RulesWindow>())       //  Ellenőrzi, hogy van-e már ilyen ablak megnyitva
            {
                RulesWindow rw = new RulesWindow();       //  Ha nincs, újat hozunk létre
                rw.Show();
            }
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static bool CheckingWindowOpen<T>() where T : Window
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is T)
                {
                    (w as T).Activate();    //  Ha már van a típusparaméterként kapott fajta ablak megnyitva, akkor azt aktívra váltja
                    return true;
                }
            }
            return false;
        }
    }
}
