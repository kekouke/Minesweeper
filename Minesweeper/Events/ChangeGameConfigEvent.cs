using Minesweeper.Models;
using Prism.Events;

namespace Minesweeper.Events
{
    public class ChangeGameConfigEvent : PubSubEvent<GameConfiguration>
    {
    }
}