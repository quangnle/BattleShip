using BattleShip.AIInterface;
using BattleShip.Processor;
using Microsoft.Win32;
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

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPlayer _p1;
        private IPlayer _p2;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dll files (*.dll)|*.dll";
            dlg.ShowDialog(this);

            var path = dlg.FileName;
            if ((sender as Button).Name == "Button1")
                _p1 = DllLoader.LoadPlayer(path);
            else
                _p2 = DllLoader.LoadPlayer(path);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            BattleController controller = new BattleController();
            controller.Initialize("P1", _p1, "P2", _p2);
            controller.OnAttack += Attack;
            controller.Battle();
        }
        private void Attack(string playerName, Position pos, bool isHit)
        {
            TxtContent.Text += string.Format("{0} attacks at ({1},{2}), {3}\r\n", playerName, pos.Row, pos.Column, isHit? "hit":"missed");
        }
    }
}
