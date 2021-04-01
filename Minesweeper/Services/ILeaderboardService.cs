using System.Threading.Tasks;
using Minesweeper.Models;

namespace Minesweeper.Services
{
    public interface ILeaderboardService
    {
        void ShowBoard(GameConfiguration configuration);
        Task SaveBoard();
        Task LoadBoard();
    }
}