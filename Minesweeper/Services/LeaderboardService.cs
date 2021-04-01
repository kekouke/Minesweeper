using System.Threading.Tasks;
using Minesweeper.Models;
using Newtonsoft.Json;

namespace Minesweeper.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly string _filePath = "";
        
        public void ShowBoard(GameConfiguration configuration)
        {
            var difficultyName = configuration.ToString();

            var correctPath = _filePath + "/" + difficultyName + "json";

        }

        public Task SaveBoard()
        {
            throw new System.NotImplementedException();
        }

        public Task LoadBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}