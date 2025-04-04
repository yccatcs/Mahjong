namespace Mahjong.Constants
{
    internal static class Terminal
    {
        /// <summary>
        /// 一
        /// </summary>
        internal const int Li = 1;
        /// <summary>
        /// 九
        /// </summary>
        internal const int Kyuu = 9;
    }

    internal static class Honour
    {
        internal enum Wind
        {
            /// <summary>
            /// 东
            /// </summary>
            Ton = 1,
            /// <summary>
            /// 南
            /// </summary>
            Nan = 2,
            /// <summary>
            /// 西
            /// </summary>
            Shaa = 3,
            /// <summary>
            /// 北
            /// </summary>
            Pei = 4,
        }

        internal enum Dragon
        {
            /// <summary>
            /// 白
            /// </summary>
            Haku = 5,
            /// <summary>
            /// 发
            /// </summary>
            Hatsu = 6,
            /// <summary>
            /// 中
            /// </summary>
            Chun = 7,
        }
    }

    /// <summary>
    /// 赤宝牌
    /// </summary>
    internal static class Akadora
    {
        /// <summary>
        /// 五
        /// </summary>
        internal const int Uu = 5;
        /// <summary>
        /// 赤五
        /// </summary>
        internal const int DoraUu = 0;
        /// <summary>
        /// 赤宝牌数量
        /// </summary>
        internal const int Count = 1;
    }

    internal static class GameConfig
    {
        internal const int CountPerTile = 4;
    }
}