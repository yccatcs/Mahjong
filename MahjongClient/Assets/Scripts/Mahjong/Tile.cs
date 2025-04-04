using UnityEngine.Assertions;
using Mahjong.Constants;
using System;

namespace Mahjong
{
    internal class Tile : IComparable
    {
        /// <summary>
        /// 数字
        /// </summary>
        private readonly int m_number;

        /// <summary>
        /// 花色
        /// </summary>
        private readonly Suit m_suit;

        internal Tile(int number, Suit suit)
        {
            m_number = number;
            m_suit = suit;
        }

        internal Tile(string s)
        {
            Assert.IsTrue(s.Length == 2, "Mahjong.Tile: The length of tile must be 2!");
            m_number = s[0] - '0';
            m_suit = s[1].ToSuit();
        }

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
        internal Suit GetSuit() => m_suit;

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
        /// 幺九牌
        /// </summary>
        internal bool IsTerminalAndHonour()
        {
            return IsTerminal() || IsHonour();
        }
    }
}