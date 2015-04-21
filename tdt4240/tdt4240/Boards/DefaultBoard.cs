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

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToArrow;
            _positions.Last().SpriteEffects = SpriteEffects.FlipVertically;
            _positions.Last().MoveAmount = 18;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToArrow;
            _positions.Last().MoveAmount = -8;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Last().Icon = _downTexture;
            _positions.Last().NavigateTo += NavigateToArrow;
            _positions.Last().MoveAmount = -13;

            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += prevX;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture)); pos += nextY;
            _positions.Add(new BoardPosition(pos, PositionType.Default, _tileTexture));
        }
    }
}
