using System;

namespace TicTacToe
{
	public class GameOverEventArgs : EventArgs
	{
		public GameOverEventArgs(GameWin winner)
		{
			Winning = winner;
		}

		public GameWin Winning { get; }
	}
}