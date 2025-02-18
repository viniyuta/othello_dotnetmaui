using System;
using OthelloLogic;

namespace OthelloUI;
public static class Images
{
    private static readonly ImageSource blackStoneSource = ImageSource.FromFile("black_stone.png");
    private static readonly ImageSource blackPlayableSource = ImageSource.FromFile("black_playable.png");
    private static readonly ImageSource whiteStoneSource = ImageSource.FromFile("white_stone.png");
    private static readonly ImageSource whitePlayableSource = ImageSource.FromFile("white_playable.png");

    public static ImageSource GetStoneImage(Player stoneColor)
    {
        return stoneColor switch
        {
            Player.Black => blackStoneSource,
            Player.White => whiteStoneSource,
            _ => ImageSource.FromFile("")
        };
    }

    public static ImageSource GetPlayableImage(Player playerColor)
    {
        return playerColor switch
        {
            Player.Black => blackPlayableSource,
            Player.White => whitePlayableSource,
            _ => ImageSource.FromFile("")
        };
    }
}
