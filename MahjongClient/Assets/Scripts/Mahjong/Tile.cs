using UnityEngine.Assertions;
using Mahjong.Constants;
using System;

namespace Mahjong
{
    /// <summary>
    /// 一张牌
    /// </summary>
    internal class Tile : IComparable
    {
#region Member
        /// <summary>
        /// 数字
        /// </summary>
        private readonly int m_number;

        /// <summary>
        /// 花色
        /// </summary>
        private readonly Suit m_suit;
#endregion Member

#region Constructor
        internal Tile(int number, Suit suit)
        {
            m_number = number;
            m_suit = suit;

            CheckValid();
        }

        internal Tile(string str)
        {
            Assert.IsTrue(str.Length == 2, "Mahjong.Tile: The length of tile must be 2!");
            m_number = str[0] - '0';
            m_suit = str[1].ToSuit();

            CheckValid();
        }

        private void CheckValid()
        {
            (int begin, int end) range = TileDefinition.GetRange(m_suit);
            int number = GetNumber();
            Assert.IsTrue(Generic.IsInRangeInclusive(number, range), $"Mahjong.Tile: The range of tile {m_suit} should be in [{range.begin}, {range.end}]! Current is {number}");
        }
#endregion Constructor

#region Override Object
        public override string ToString()
        {
            return $"{m_number}{m_suit}";
        }

        public override bool Equals(object obj)
        {
            if (obj is not Tile other)
                return false;
            return m_number == other.m_number && m_suit == other.m_suit;
        }

        public override int GetHashCode()
        {
            return m_number ^ m_suit.GetHashCode();
        }
#endregion Override Object

#region Interface IComparable
        public int CompareTo(object obj)
        {
            if (obj is not Tile other)
                return -1;
            if (GetSuit() != other.GetSuit())
                return GetSuit().CompareTo(other.GetSuit());
            if (GetNumber() != other.GetNumber())
                return GetNumber().CompareTo(other.GetNumber());
            return -IsAkadora().CompareTo(other.IsAkadora());
        }
#endregion Interface IComparable

#region API Get
        /// <summary>
        /// 数字
        /// </summary>
        internal int GetNumber()
        {
            if (m_number == Akadora.DoraUu)
                return Akadora.Uu;
            return m_number;
        }

        /// <summary>
        /// 花色
        /// </summary>
        internal Suit GetSuit()
        {
            return m_suit;
        }
#endregion API Get

#region API Predicate
        internal bool IsSame(Tile other)
        {
            return GetSuit() == other.GetSuit() && GetNumber() == other.GetNumber();
        }

        /// <summary>
        /// 赤宝牌
        /// </summary>
        internal bool IsAkadora()
        {
            return m_number == Akadora.DoraUu;
        }

        /// <summary>
        /// 老头牌
        /// </summary>
        internal bool IsTerminal()
        {
            return GetSuit() != Suit.z && (GetNumber() == Terminal.Li || GetNumber() == Terminal.Kyuu);
        }

        /// <summary>
        /// 字牌
        /// </summary>
        internal bool IsHonour()
        {
            return GetSuit() == Suit.z;
        }

        /// <summary>
        /// 风牌
        /// </summary>
        internal bool IsWind()
        {
            return IsHonour() && Generic.IsInRangeInclusive(GetNumber(), TileDefinition.GetWindRange());
        }

        /// <summary>
        /// 三元牌
        /// </summary>
        internal bool IsDragon()
        {
            return IsHonour() && Generic.IsInRangeInclusive(GetNumber(), TileDefinition.GetDragonRange());
        }

        /// <summary>
        /// 幺九牌
        /// </summary>
        internal bool IsTerminalOrHonour()
        {
            return IsTerminal() || IsHonour();
        }
#endregion API Predicate

#region Sequential
        internal Tile GetNext()
        {
            return TileDefinition.GetNextTile(this);
        }
#endregion Sequential
    }
}