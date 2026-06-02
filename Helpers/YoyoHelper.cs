using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace YoyoRebalance.Helpers;

public static class YoyoHelper
{
    public static readonly Dictionary<int, int> YoyoDamage = new();
    
    public static void ModifyYoyo(int itemId, int damage = -1, float lifetime = -1f, float tileRange = -1f, float speedMph = -1f)
    {
        var item = new Item();
        item.SetDefaults(itemId);

        var projectileId = item.shoot;

        if (damage > 0)
            YoyoDamage.Add(itemId, damage);
        
        if (lifetime > 0)
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectileId] = lifetime;
        
        if (tileRange > 0)
            ProjectileID.Sets.YoyosMaximumRange[projectileId] = tileRange * 16f;
        
        if (speedMph > 0)
            ProjectileID.Sets.YoyosTopSpeed[projectileId] = speedMph * (9f / 46f);
    }
}