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
            DataContext = rgvm;         //  A ViewModel példányra állítjuk a DataContext-et
        }

        private void SolveClick(object sender, RoutedEventArgs e)
        {
            string hiba = "";
            rgvm.Solve(ref hiba);
            if(hiba != "")
            {
                MessageBox.Show(hiba, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShuffleClick(object sender, RoutedEventArgs e)
        {
                rgvm.SetRandomOrder();
        }

        private void QuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RulesClick(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.CheckingWindowOpen<RulesWindow>())      //  Ellenőrzi, hogy van-e már ilyen ablak megnyitva
            {
                RulesWindow rw = new RulesWindow();                 //  Ha nincs, újat hozunk létre
                rw.Show();
            }
        }
    }

    public class BoolToColorConverter : IValueConverter     //  A kapott Boolean értéket színné alakítja, ha igaz akkor zöldé, ha nem akkor pirossá
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Brushes.LightGreen : Brushes.Salmon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
