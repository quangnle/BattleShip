using BattleShip.AIInterface;
using BattleShip.BattleEventArguments;
using BattleShip.Core;
using BattleShip.Models;
using BattleShip.Processor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        // ui constants
        const int BOARD_NUM_ROWS = 10;
        const int BOARD_NUM_COLS = 10;

        const int PLAYER_A_NO = 0;
        const int PLAYER_B_NO = 1;

        double _canvasWidth = 600;
        double _canvasHeight = 400;
        double _cellWidth;
        double _cellHeight;
        Vector2 _startPositionA;
        Vector2 _startPositionB;

        PlayerModel[] _playerModels;
        BattleController controller;

        // Rendering State
        int _currentPlayer = 0;

        Ellipse _bullet = new Ellipse() { Width = 20, Height = 20, Fill = Brushes.Blue };

        public MainWindow()
        {
            InitializeComponent();

            _playerModels = new PlayerModel[2];
            _playerModels[0] = new PlayerModel();
            _playerModels[1] = new PlayerModel();

            InitBoard();
        }

        private void InitBoard()
        {
            _cellWidth = _canvasWidth / (BOARD_NUM_COLS * 2 + 1);
            _cellHeight = _canvasHeight / BOARD_NUM_ROWS;

            _startPositionA = new Vector2() { X = 0, Y = 0 };
            _startPositionB = new Vector2() { X = (BOARD_NUM_COLS + 1) * _cellWidth, Y = 0 };

            _playerModels[PLAYER_A_NO].Stragegy = new Rectangle[BOARD_NUM_ROWS, BOARD_NUM_COLS];
            for (int r = 0; r < BOARD_NUM_ROWS; r++)
            {
                for (int c = 0; c < BOARD_NUM_COLS; c++)
                {
                    var cell = new Rectangle() { Width = _cellWidth, Height = _cellHeight, Stroke = Brushes.Red, StrokeThickness = 1 };
                    cell.SetValue(Canvas.TopProperty, _startPositionA.X + r * _cellHeight);
                    cell.SetValue(Canvas.LeftProperty, _startPositionA.Y + c * _cellWidth);
                    _playerModels[PLAYER_A_NO].Stragegy[r, c] = cell;
                    battleBoard.Children.Add(cell);
                }
            }

            _playerModels[PLAYER_B_NO].Stragegy = new Rectangle[BOARD_NUM_ROWS, BOARD_NUM_COLS];
            for (int r = 0; r < BOARD_NUM_ROWS; r++)
            {
                for (int c = 0; c < BOARD_NUM_COLS; c++)
                {
                    var cell = new Rectangle() { Width = _cellWidth, Height = _cellHeight, Stroke = Brushes.Red, StrokeThickness = 1 };
                    cell.SetValue(Canvas.LeftProperty, _startPositionB.X + c * _cellWidth);
                    cell.SetValue(Canvas.TopProperty, _startPositionB.Y + r * _cellHeight);
                    _playerModels[PLAYER_B_NO].Stragegy[r, c] = cell;
                    battleBoard.Children.Add(cell);
                }
            }

            battleBoard.Children.Add(_bullet);
        }

        private void DrawBoard()
        {
            var mapA = controller.MapPlayerA;
            var mapB = controller.MapPlayerB;

            for (int r = 0; r < BOARD_NUM_ROWS; r++)
            {
                for (int c = 0; c < BOARD_NUM_COLS; c++)
                {
                    var stt = mapA[r, c];
                    if (stt <= 0)
                        _playerModels[PLAYER_A_NO].Stragegy[r, c].Fill = Brushes.White;
                    else
                        _playerModels[PLAYER_A_NO].Stragegy[r, c].Fill = Brushes.Black;

                    var stt2 = mapB[r, c];
                    if (stt2 <= 0)
                        _playerModels[PLAYER_B_NO].Stragegy[r, c].Fill = Brushes.White;
                    else
                        _playerModels[PLAYER_B_NO].Stragegy[r, c].Fill = Brushes.Black;
                }
            }

            battleBoard.InvalidateVisual();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Dll files (*.dll)|*.dll";
            dlg.ShowDialog(this);

            var path = dlg.FileName;
            if ((sender as Button).Name == "Button1")
                _playerModels[PLAYER_A_NO].Controller = DllLoader.LoadPlayer(path);
            else
                _playerModels[PLAYER_B_NO].Controller = DllLoader.LoadPlayer(path);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            controller = new BattleController();

            

            controller.Initialize(_playerModels[PLAYER_A_NO].Controller, _playerModels[PLAYER_B_NO].Controller);

            controller.OnMapUpdated += (s, ee) =>
            {
                Dispatcher.Invoke((Action)delegate() { DrawBoard(); });
            };

            controller.OnTurnSwitched += (s, ee) =>
            {
                Dispatcher.Invoke((Action)delegate()
                {
                    var arg = ee as SwitchTurnArgs;
                    SwitchTurn(arg.PlayerId);
                });
            };

            controller.OnShot += (s, ee) =>
            {
                Dispatcher.Invoke((Action)delegate() 
                {
                    var arg = ee as ShotEventArgs;
                    Shot(arg.Row, arg.Column);
                });
            };

            controller.OnAttack += Attack;
            controller.OnBattleEnd += BattleEnd;

            DrawBoard();

            var battleThread = new Thread(controller.Battle);
            battleThread.Start();
        }

        private void StartEveryShot()
        {
            //_bullet.Visibility = Visibility.Hidden;
        }

        private void Shot(int row, int col)
        {
            Vector2 startPos;

            if (_currentPlayer == Constants.PLAYER_A_ID)
                startPos = _startPositionB;
            else
                startPos = _startPositionA;

            _bullet.SetValue(Canvas.LeftProperty, startPos.X + (col + 0.5) * _cellWidth - _bullet.Width/2);
            _bullet.SetValue(Canvas.TopProperty, startPos.Y + (row+0.5) * _cellHeight - _bullet.Height/2);
            _bullet.Visibility = Visibility.Visible;
            battleBoard.InvalidateVisual();
        }

        private void SwitchTurn(int playerId)
        {
            _currentPlayer = playerId;
        }

        private void BattleEnd(string winner)
        {
            MessageBox.Show(winner);
        }
        private void Attack(string playerName, Position pos, bool isHit)
        {
            //TxtContent.Text += string.Format("{0} attacks at ({1},{2}), {3}\r\n", playerName, pos.Row, pos.Column, isHit? "hit":"missed");
        }
    }
}
