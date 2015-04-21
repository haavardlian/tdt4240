using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tdt4240.Boards
{
    class DefaultBoard : Board
    {
        protected override void AddPositions()
        {
            var factor = -1;
            var nextX = new Vector2(96, 0);
            var prevX = new Vector2(-96, 0);
            var nextY = new Vector2(0, -96);
            var prevY = new Vector2(0, 96);
            var pos = new Vector2(466, 1000);

            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Last().Icon = DownTexture;
            Positions.Last().NavigateTo += NavigateToArrow;
            Positions.Last().SpriteEffects = SpriteEffects.FlipVertically;
            Positions.Last().MoveAmount = 18;

            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Last().Icon = DownTexture;
            Positions.Last().NavigateTo += NavigateToArrow;
            Positions.Last().MoveAmount = -8;

            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Last().Icon = DownTexture;
            Positions.Last().NavigateTo += NavigateToArrow;
            Positions.Last().MoveAmount = -13;

            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += prevX;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture)); pos += nextY;
            Positions.Add(new BoardPosition(pos, PositionType.Default, TileTexture));
        }
    }
}
