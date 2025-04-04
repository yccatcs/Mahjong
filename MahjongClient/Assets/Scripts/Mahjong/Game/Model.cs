    using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Mahjong.Game
{
    internal class Model
    {
        private GameMode m_gameMode;
        private List<Player> m_players;

        internal Model(GameMode gameMode, List<Player> players)
        {
            m_gameMode = gameMode;
            m_players = players;
            // TODO: THREE mode
            Assert.IsTrue(players.Count == 4);
        }
    }
}