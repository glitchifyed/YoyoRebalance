using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace YoyoRebalance.Helpers;

public static class WeaponHelper
{
    private static readonly Point[] BloodButchererProjectiles = new Point[20];
    
    public static void ApplyBloodButchererDebuff(Player player, NPC npc, Item sItem, int damage, float knockBack)
    {
        var mountedCenter = player.MountedCenter;

        if (!npc.CanBeChasedBy(player) || npc.buffImmune[BuffID.BloodButcherer])
            return;
        
        npc.AddBuff(BuffID.BloodButcherer, 9 * 60);
            
        var offset = npc.Center - mountedCenter;
        offset = offset.SafeNormalize(Vector2.Zero);
        
        var vector = npc.Hitbox.ClosestPointInRect(mountedCenter) + offset;
        var spinningPoint = (npc.Center - vector) * 0.8f;
        spinningPoint = spinningPoint.RotatedBy(Main.rand.NextFloatDirection() * (float)Math.PI * 0.25f);
        
        var num = Projectile.NewProjectile(player.GetSource_ItemUse(sItem), vector.X, vector.Y, spinningPoint.X, spinningPoint.Y, ProjectileID.BloodButcherer, damage, knockBack, player.whoAmI, 1f, npc.whoAmI);
        Main.projectile[num].StatusNPC(npc.whoAmI);
        
        Projectile.KillOldestJavelin(num, ProjectileID.BloodButcherer, npc.whoAmI, BloodButchererProjectiles);
    }

    public static void SpawnLightsBaneProjectile(Player player, Projectile projectile, NPC npc, Item item)
    {
        if (!npc.CanBeChasedBy(player))
            return;

        var direction = -projectile.velocity.SafeNormalize(Vector2.Zero);
        
        Projectile.NewProjectile(player.GetSource_ItemUse(item), projectile.Center.X, projectile.Center.Y, direction.X * 2f, direction.Y * 2f, ProjectileID.LightsBane, item.damage / 2, item.knockBack / 2, player.whoAmI, 1f, npc.whoAmI);
    }
    
    public static void SpawnStingerProjectile(Player player, Projectile projectile, NPC npc, Item item)
    {
        if (!npc.CanBeChasedBy(player))
            return;

        var direction = (npc.Center - player.Center).SafeNormalize(Vector2.Zero);
        
        Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center.X, player.Center.Y, direction.X * 9f, direction.Y * 9f, ProjectileID.HornetStinger, item.damage, item.knockBack, player.whoAmI, ProjAIStyleID.Hornet);
    }
    
    public static void FireVolcanoExplosion(Player player, Vector2 position, NPC npc, Item item)
    {
        if (!npc.CanBeChasedBy(player))
            return;
        
        var num = Projectile.NewProjectile(player.GetSource_ItemUse(item), position.X, position.Y, 0f, 0f, ProjectileID.Volcano, item.damage, item.knockBack * 2, player.whoAmI);
    }
    
    public static void FireSeedlerPellets(Player player, Projectile projectile, NPC npc, Item item)
    {
        if (!npc.CanBeChasedBy(player))
            return;

        var direction = Main.rand.NextVector2Circular(0.5f, 0.5f);
        var offset = (projectile.Center - npc.Center).SafeNormalize(Vector2.Zero);
        
        Projectile.NewProjectile(player.GetSource_ItemUse(item), projectile.Center.X + offset.X * 40f, projectile.Center.Y + offset.Y * 40f, direction.X * 20f, direction.Y * 20f, ProjectileID.SeedlerNut, item.damage, item.knockBack, player.whoAmI, 1f);
    }
    
    public static void FireBubbles(Player player, Projectile projectile, NPC npc, Item item)
    {
        if (!npc.CanBeChasedBy(player))
            return;

        for (int i = 0; i < 5; i++)
        {
            var direction = Main.rand.NextVector2Circular(0.5f, 0.5f);
            var offset = Main.rand.NextFloat();

            Projectile.NewProjectile(player.GetSource_ItemUse(item), projectile.Center.X + direction.X * 400f + direction.X * offset * 300f, projectile.Center.Y + direction.Y * 400f + direction.Y * offset * 300f, -direction.X * 15f - direction.X * offset * 10f, -direction.Y * 15f - direction.Y * offset * 10f, ProjectileID.Bubble, item.damage, item.knockBack * 2, player.whoAmI, ProjAIStyleID.Bubble);
        }
    }
    
    public static bool FireLaser(Player player, Projectile projectile, Item item)
    {
        var npcList = new List<NPC>();
        
        foreach (var checkNpc in Main.ActiveNPCs)
        {
            if (!checkNpc.CanBeChasedBy(player))
                continue;

            var distance = Vector2.Distance(projectile.Center, checkNpc.Center);
            if (distance is < 100f or > 800f)
                continue;
            
            npcList.Add(checkNpc);
        }

        if (npcList.Count == 0)
            return false;

        var npc = npcList[Main.rand.Next(npcList.Count)];

        var offset = npc.Hitbox.ClosestPointInRect(projectile.Center) - projectile.Center;

        var npcDistance = offset.Length();
        var speed = Main.rand.NextFloat() * 5f + 10f;

        var time = npcDistance / speed * 0.9f;
        var velocityInfluence = new Vector2(npc.velocity.X * time, npc.velocity.Y * time);

        offset = npc.Center + velocityInfluence - projectile.Center;
        var direction = offset.SafeNormalize(Vector2.Zero);

        var num = Projectile.NewProjectile(player.GetSource_ItemUse(item), projectile.Center.X, projectile.Center.Y, direction.X * speed, direction.Y * speed, ProjectileID.EyeLaser, item.damage, item.knockBack * 3, player.whoAmI);
        var laser = Main.projectile[num];

        laser.hostile = false;
        laser.friendly = true;
        laser.penetrate = 1;

        return true;
    }
}