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

        public async Task<bool> ProcessTurnAsync( //would rather return "out bool gameOver" but async methods doesnt support such params
           int col,
           int row,
           BoardComponent oponentBoardComponent,
           ICollection<Ship> oponentShips,
           string gameOverMessage)
        {
            HitOrMishitShip(col, row, oponentBoardComponent, oponentShips);

            SunkShipsIfDestroyed(oponentBoardComponent, oponentShips);

            await oponentBoardComponent.RedrawAsync();

            var gameOver = CheckIfGameIsOverAsync(oponentShips);
            if (gameOver)
            {
                await _jsRuntime.InvokeAsync<string>("alert", gameOverMessage);
            }

            return gameOver;
        }

        public static bool ValidateTurn(int col, int row, BoardComponent oponentBoardComponent)
        {
            var cellState = oponentBoardComponent.Board.GetCellState(col, row);

            //rules (now only one):
            return cellState == CellState.Sea || cellState == CellState.Ship;
        }

        private static bool CheckIfGameIsOverAsync(ICollection<Ship> oponentShips)
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
                    }
                }
            }
        }

        private static void HitOrMishitShip(int col, int row, BoardComponent oponentBoardComponent, ICollection<Ship> oponentShips)
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
        }
    }
}
