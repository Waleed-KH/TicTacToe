using System.Collections.Generic;

namespace TicTacToe
{
	public class GameWin
	{
		public GameWin(GamePlay winner, IReadOnlyList<int> winLine)
		{
			Winner = winner;
			WinLine = winLine;
		}
		public GamePlay Winner { get; }
		public IReadOnlyList<int> WinLine { get; }
	}
}
