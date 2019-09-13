using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Battleships.Components;
using Battleships.Models;
using Microsoft.JSInterop;

namespace Battleships.Mechanics
{
    public class TurnProcessor
    {
        private readonly IJSRuntime _jsRuntime;

        public TurnProcessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<(bool Hit, bool GameOver)> ProcessTurnAsync( //would rather return "out bool gameOver" but async methods doesnt support such params
           int col,
           int row,
           BoardComponent oponentBoardComponent,
           ICollection<Ship> oponentShips,
           string gameOverMessage)
        {
            var hit = HitOrMishitShip(col, row, oponentBoardComponent, oponentShips);

            SunkShipsIfDestroyed(oponentBoardComponent, oponentShips);

            await oponentBoardComponent.RedrawAsync();

            var gameOver = CheckIfGameIsOver(oponentShips);
            if (gameOver)
            {
                await _jsRuntime.InvokeAsync<string>("alert", gameOverMessage);
            }

            return (hit, gameOver);
        }

        public static bool ValidateTurn(int col, int row, BoardComponent oponentBoardComponent)
        {
            var cellState = oponentBoardComponent.Board.GetCellState(col, row);

            //rules (now only one):
            return cellState == CellState.Sea || cellState == CellState.Ship;
        }

        private static bool CheckIfGameIsOver(ICollection<Ship> oponentShips)
        {
            return oponentShips.All(ship => ship.IsDestroyed);
        }

        private static void SunkShipsIfDestroyed(BoardComponent oponentBoardComponent, ICollection<Ship> oponentShips)
        {
            foreach (var oponentShip in oponentShips)
            {
                if (oponentShip.IsDestroyed)
                {
                    foreach (var segment in oponentShip.Segments)
                    {
                        oponentBoardComponent.Board.SetCellState(segment.X, segment.Y, CellState.Sunk);
                        DiscoverSurroundingCells(segment, oponentBoardComponent);
                    }
                }
            }
        }

        private static void DiscoverSurroundingCells(
            (int X, int Y, bool IsHit) segment,
            BoardComponent oponentBoardComponent)
        {
            for (var xAxis = -1; xAxis <= 1; xAxis++)
            {
                for (var yAxis = -1; yAxis <= 1; yAxis++)
                {
                    if(xAxis == 0 && yAxis == 0) continue;

                    var cellX = segment.X + xAxis;
                    var cellY = segment.Y + yAxis;

                    if (oponentBoardComponent.Board.IsValidCell(cellX, cellY) &&
                        oponentBoardComponent.Board.GetCellState(cellX, cellY) == CellState.Sea)
                    {
                        oponentBoardComponent.Board.SetCellState(cellX, cellY, CellState.Mishit);
                    }
                }
            }
        }

        private static bool HitOrMishitShip(int col, int row, BoardComponent oponentBoardComponent, ICollection<Ship> oponentShips)
        {
            var hitShip = oponentShips.SingleOrDefault(ship => ship.Collides(col, row));
            if (hitShip != null)
            {
                hitShip.DamageSegment(col, row);
                oponentBoardComponent.Board.SetCellState(col, row, CellState.Hit);
            }
            else
            {
                oponentBoardComponent.Board.SetCellState(col, row, CellState.Mishit);
            }

            return hitShip != null;
        }
    }
}
