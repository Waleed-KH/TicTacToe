using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
	public class Game
	{
		private static readonly int[][] Winners = {
			new int[] {0, 1, 2},
			new int[] {3, 4, 5},
			new int[] {6, 7, 8},
			new int[] {0, 3, 6},
			new int[] {1, 4, 7},
			new int[] {2, 5, 8},
			new int[] {0, 4, 8},
			new int[] {2, 4, 6}
		};

		private List<GamePlay> plays = new List<GamePlay>(Enumerable.Repeat(GamePlay.None, 9));

		public void Play(int index)
		{
			// Check if he can play
			if (IsGameOver)
				throw new GameException(GameErrorType.GameOver);
			if (index <= 0 && index >= 8)
				throw new GameException(GameErrorType.IndexOutOfRange);
			if (plays[index] != GamePlay.None)
				throw new GameException(GameErrorType.IndexPlayedOn);

			// Play
			plays[index] = CurrentPlayer;
			Played?.Invoke(this, EventArgs.Empty);


			// Chack if current player wins
			foreach (var winLine in Winners)
			{
				if (plays[winLine[0]] == GamePlay.None || plays[winLine[1]] == GamePlay.None || plays[winLine[2]] == GamePlay.None)
					continue;

				if (plays[winLine[0]] == plays[winLine[1]] && plays[winLine[1]] == plays[winLine[2]])
				{
					Winning = new GameWin(CurrentPlayer, winLine);

					IsGameOver = true;
					GameOver?.Invoke(this, new GameOverEventArgs(Winning));
					return;
				}
			}

			// Chack if game is over with no winner
			if (!plays.Contains(GamePlay.None))
			{
				Winning = new GameWin(GamePlay.None, null);

				IsGameOver = true;
				GameOver?.Invoke(this, new GameOverEventArgs(Winning));
				return;
			}

			// If not over get ready for next round
			CurrentPlayer = (GamePlay)((int)CurrentPlayer % 2 + 1);
		}

		public GamePlay CurrentPlayer { get; private set; } = GamePlay.X;
		public bool IsGameOver { get; private set; } = false;
		public GameWin Winning { get; private set; }
		public IReadOnlyList<GamePlay> Plays => plays;

		public event GamePlayedHandler Played;
		public event GameOverHandler GameOver;
	}
}
