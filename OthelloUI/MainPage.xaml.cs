using OthelloLogic;

namespace OthelloUI;

public partial class MainPage : ContentPage
{
	private static readonly int COLS = Board.COLS;
	private static readonly int ROWS = Board.ROWS;

	private readonly Image[,] pieceImages = new Image[COLS, ROWS];
	private List<Position> playablesCache = [];
	private Position? lastPlay;

	private GameState gameState;

    public MainPage()
	{
		InitializeComponent();
		InitializeBoard();
		
		gameState = new GameState(Player.Black, Board.Initial());
		UpdateBoard(gameState);
	}

	private void InitializeBoard()
	{
		for (int col = 0; col < COLS; col++)
		{
			for (int row = 0; row < ROWS; row++)
			{
				Image image = new ();
				pieceImages[col, row] = image;
				PieceGrid.Add(image, col, row);
			}
		}
	}

	private void DrawBoard(Board board)
	{
		for (int col = 0; col < COLS; col++)
		{
			for (int row = 0; row < ROWS; row++)
			{
				if (!board.IsEmpty(col, row))
				{
					Stone stone = board[col, row];
					Player stoneColor = stone.Color;
					pieceImages[col, row].Source = Images.GetStoneImage(stoneColor);
				} 
			}
		}
	}

	private void ShowPlayables(Player currentPlayer)
	{
		foreach(Position pos in playablesCache)
		{
			pieceImages[pos.Column, pos.Row].Source = Images.GetPlayableImage(currentPlayer);
			pieceImages[pos.Column, pos.Row].GestureRecognizers.Add(TapListener(pos.Column, pos.Row));
		}
	}
	private void HidePlayables()
	{
		foreach(Position pos in playablesCache)
		{
			pieceImages[pos.Column, pos.Row].Source = null;
			pieceImages[pos.Column, pos.Row].GestureRecognizers.Clear();
		}
	}

	private void ShowLastPlay()
	{
		if (lastPlay != null)
		{
			LastPlayBoxView.Color = Color.FromHex("E00000");
			PieceGrid.Add(LastPlayBoxView, lastPlay.Column, lastPlay.Row);
		}
	}

	private void ShowCounts(Dictionary<Player, int> counts)
	{
		string BlackPlayerCount = counts[Player.Black].ToString();
		string WhitePlayerCounts = counts[Player.White].ToString();
		BlackPlayerCountLabel.Text = BlackPlayerCount;
		WhitePlayerCountLabel.Text = WhitePlayerCounts;
	}

	private void CachePlayables(Playables playables)
	{
		playablesCache = [];
		for(int col = 0; col < COLS; col++)
		{
			for(int row = 0; row < ROWS; row++)
			{
				if (playables.IsPlayable(col, row))
				{
					playablesCache.Add(new Position(col, row));
				}
			}
		}
	}

	private TapGestureRecognizer TapListener(int col, int row)
	{
		TapGestureRecognizer tapGestureRecognizer = new();
		tapGestureRecognizer.Tapped += (s, e) =>
		{
			Position clickedPos = new (col, row);
			PlaceStone(clickedPos);
		};
		return tapGestureRecognizer;
	}

	private void PlaceStone(Position pos)
	{
		gameState.PlayStone(pos);
		lastPlay = pos;
		UpdateBoard(gameState);
	}

	private void UpdateBoard(GameState gameState)
	{
		HidePlayables();
		CachePlayables(gameState.Playables);
		DrawBoard(gameState.Board);
		ShowPlayables(gameState.CurrentPlayer);
		ShowLastPlay();
		ShowCounts(gameState.StoneCounts);
	}


}


