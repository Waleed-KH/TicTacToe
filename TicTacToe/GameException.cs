using System;

namespace TicTacToe
{
	public class GameException : Exception
	{
		public GameException(string message, GameErrorType errorType) : base(message)
		{
			ErrorType = errorType;
		}
		public GameException(GameErrorType errorType) : this(ErrorMsg(errorType), errorType)
		{
		}
		public GameException(string message) : this(message, GameErrorType.Unknown)
		{
		}
		public GameException() : this(GameErrorType.Unknown)
		{
		}

		public GameErrorType ErrorType { get; }

		private static string ErrorMsg(GameErrorType errorType)
		{
			switch (errorType)
			{
				case GameErrorType.IndexOutOfRange:
					return "Index is out of the game range.";
				case GameErrorType.IndexPlayedOn:
					return "Index is already played on.";
				case GameErrorType.GameOver:
					return "The Game is Over.";
				default:
					return "Game Error!";
			}
		}
	}
}
