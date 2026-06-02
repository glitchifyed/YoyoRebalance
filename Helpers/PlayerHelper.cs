using Terraria;
using YoyoRebalance.Common;

namespace YoyoRebalance.Helpers;

public static class PlayerHelper
{
    public static Player GetPlayer(int index = -1)
    {
        if (index == -1)
            index = Main.myPlayer;
        
        return Main.player[index];
    }

    public static YoyoModPlayer GetModPlayer(int index = -1)
    {
        return GetPlayer(index).GetModPlayer<YoyoModPlayer>();
    }
}