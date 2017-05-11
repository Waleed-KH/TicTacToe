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

namespace TicTacToe.GUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Game game;
		private Button[] playIndexButtons;
		public MainWindow()
		{
			InitializeComponent();
			playIndexButtons = new Button[] { PlayIndex0, PlayIndex1, PlayIndex2, PlayIndex3, PlayIndex4, PlayIndex5, PlayIndex6, PlayIndex7, PlayIndex8 };
			NewGame();
		}

		private void PlayIndex(object sender, RoutedEventArgs e)
		{
			try
			{
				game.Play(int.Parse(((FrameworkElement)sender).Name[9].ToString()));
			}
			catch (GameException ex)
			{
				MessageBox.Show(ex.Message, "Game Error!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				UpdateBoard();
			}
		}
		private void NewGame(object sender, RoutedEventArgs e)
		{
			NewGame();
		}
		private void NewGame()
		{
			if (game?.Winning?.WinLine != null)
				foreach (var winCell in game.Winning.WinLine)
				{
					playIndexButtons[winCell].Foreground = Brushes.Black;
					playIndexButtons[winCell].FontWeight = FontWeights.Normal;
				}
			game = new Game();
			//game.Played += (object sender, EventArgs e) => UpdateBoard();
			UpdateBoard();
		}
		private void UpdateBoard()
		{
			for (int i = 0; i < game.Plays.Count; i++)
			{
				if (game.Plays[i] == GamePlay.None)
				{
					playIndexButtons[i].Content = "";
					playIndexButtons[i].IsEnabled = !game.IsGameOver;
				}
				else
				{
					playIndexButtons[i].Content = game.Plays[i].ToString();
					playIndexButtons[i].IsEnabled = false;
				}
			}

			if (game.IsGameOver)
			{
				GameStatus.Text = "Game Over - " + (game.Winning.Winner == GamePlay.None ? "No winner" : "Winner is " + game.Winning.Winner);
				if (game.Winning.WinLine != null)
					foreach (var winCell in game.Winning.WinLine)
					{
						playIndexButtons[winCell].Foreground = Brushes.RoyalBlue;
						playIndexButtons[winCell].FontWeight = FontWeights.Bold;
					}
			}
			else
				GameStatus.Text = "Current Player: " + game.CurrentPlayer;


		}
	}
}
