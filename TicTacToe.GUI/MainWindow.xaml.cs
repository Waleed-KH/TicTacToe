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
			game = new Game();
			UpdateBoard();
		}
		private void UpdateBoard()
		{
			if (game.IsGameOver)
			{
				GameStatus.Text = "Game Over - " + (game.Winning.Winner == GamePlay.None ? "No winner" : "Winner is " + game.Winning.Winner);
				// TODO: Highlight the win line
			}
			else
				GameStatus.Text = "Current Player: " + game.CurrentPlayer;


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
		}
	}
}
