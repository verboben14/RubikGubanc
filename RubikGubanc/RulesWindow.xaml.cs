using System;
using System.Collections.Generic;
using System.IO;
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

namespace RubikGubanc
{
    /// <summary>
    /// Interaction logic for RulesWindow.xaml
    /// </summary>
    public partial class RulesWindow : Window
    {
        public RulesWindow()
        {
            InitializeComponent();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\Rules.txt";
            try
            {
                rulesTxb.Text = System.IO.File.ReadAllText(path);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("There was no file found with the given parameters: " + path, "File does not exist!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            rulesTxb.TextWrapping = TextWrapping.Wrap;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
