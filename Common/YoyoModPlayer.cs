using Terraria.ModLoader;

namespace YoyoRebalance.Common;

public class YoyoModPlayer : ModPlayer
{
    public int TicksSinceLastEyeYoyo = 0;
    public int TicksSinceEyeYoyoStart = 0;
    public int TicksSinceEyeYoyoSpawn = 0;
    public bool EyeFirstFire = false;
}