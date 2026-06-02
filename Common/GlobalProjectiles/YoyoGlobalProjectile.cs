using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using YoyoRebalance.Helpers;

namespace YoyoRebalance.Common.GlobalProjectiles
{
    public class YoyoGlobalProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] != 0;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.myPlayer != projectile.owner)
                return;
            
            var player = PlayerHelper.GetPlayer(projectile.owner);

            switch (projectile.type)
            {
                case ProjectileID.CrimsonYoyo:
                    WeaponHelper.ApplyBloodButchererDebuff(player, target, player.HeldItem, 4, 0);

                    return;
                case ProjectileID.CorruptYoyo:
                    WeaponHelper.SpawnLightsBaneProjectile(player, projectile, target, player.HeldItem);

                    return;
                case ProjectileID.JungleYoyo:
                    target.AddBuff(BuffID.Poisoned, 7 * 60);
                    
                    WeaponHelper.SpawnStingerProjectile(player, projectile, target, player.HeldItem);

                    return;
                
                case ProjectileID.HelFire:
                    WeaponHelper.FireVolcanoExplosion(player, projectile.Center, target, player.HeldItem);

                    return;
                case ProjectileID.Yelets:
                    WeaponHelper.FireSeedlerPellets(player, projectile, target, player.HeldItem);

                    return;
                case ProjectileID.Kraken:
                    WeaponHelper.FireBubbles(player, projectile, target, player.HeldItem);

                    return;
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.myPlayer != projectile.owner)
                return;

            if (projectile.type == ProjectileID.Chik)
            {
                projectile.ArmorPenetration += 40;

                return;
            }
            
            if (projectile.type != ProjectileID.TheEyeOfCthulhu)
                return;
            
            var modPlayer = PlayerHelper.GetModPlayer(projectile.owner);

            modPlayer.TicksSinceLastEyeYoyo = 0;
            modPlayer.TicksSinceEyeYoyoStart = 0;
            modPlayer.TicksSinceEyeYoyoSpawn = 0;
            modPlayer.EyeFirstFire = false;
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.type != ProjectileID.TheEyeOfCthulhu || Main.myPlayer != projectile.owner)
                return;

            var modPlayer = PlayerHelper.GetModPlayer(projectile.owner);

            modPlayer.TicksSinceEyeYoyoSpawn++;
            
            if (modPlayer.EyeFirstFire)
            {
                modPlayer.TicksSinceLastEyeYoyo++;
                modPlayer.TicksSinceEyeYoyoStart++;
                
                var debounce = 1800 / Math.Min(modPlayer.TicksSinceEyeYoyoStart, 300);

                if (modPlayer.TicksSinceLastEyeYoyo < debounce)
                    return;

                modPlayer.TicksSinceLastEyeYoyo -= debounce;
            }
            else if (modPlayer.TicksSinceEyeYoyoSpawn < 30f)
                return;
            
            var player = PlayerHelper.GetPlayer(projectile.owner);
            var success = WeaponHelper.FireLaser(player, projectile, player.HeldItem);
            
            if (success && !modPlayer.EyeFirstFire)
                modPlayer.EyeFirstFire = true;
        }
    }
}