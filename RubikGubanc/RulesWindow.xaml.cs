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
            rulesTxb.Text = "Ez a program a Rubik Gubanc nevű játék egy szimulációs megoldója.\n\nA Rubik Gubanc 9 kis habkarton lapból álló logikai játék. Ezek a kartonlapok kétoldalasak, tehát összesen 18 db ábránk van. Minden ábrán 4 db különböző színű fonal látható, ami egymásba van gabalyodva. Minden ábra ugyanúgy néz ki, azzal a különbséggel, hogy más a fonalak színezése. A játék célja, hogy úgy helyezzük le ezt a kilenc kartonlapocskát, hogy a fonalak színezése folyamatos legyen, és az egész egy 3x3-as négyzetet alkosson. A kártyákat összesen 95 126 813 710 féleképpen lehet lerakni, amiből 1 a jó megoldás. Méretek: A lapok egyenként 6.5 x 6.5 cm, míg a 3x3-as négyzet kirakva 19.5 x 19.5 cm nagyságú.";
            rulesTxb.TextWrapping = TextWrapping.Wrap;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
