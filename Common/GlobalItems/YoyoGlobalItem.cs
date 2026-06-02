using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YoyoRebalance.Helpers;

namespace YoyoRebalance.Common.GlobalItems
{
    public class YoyoGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return ItemID.Sets.Yoyo[item.type];
        }

        public override void SetDefaults(Item item)
        {
            if (!YoyoHelper.YoyoDamage.TryGetValue(item.type, out var damage))
                return;

            item.damage = damage;
            
            item.StatsModifiedBy.Add(Mod);
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[ItemID.Seedler] = ItemID.Yelets;
            ItemID.Sets.ShimmerTransformToItem[ItemID.Yelets] = ItemID.Seedler;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var lifetime = ProjectileID.Sets.YoyosLifeTimeMultiplier[item.shoot];
            
            tooltips.Insert(5, new TooltipLine(Mod, "YoyoLifetime", lifetime == -1 ? "Spins forever" : $"{lifetime} sec spin time"));
            tooltips.Insert(6, new TooltipLine(Mod, "YoyoRange", $"{Math.Floor(ProjectileID.Sets.YoyosMaximumRange[item.shoot] * 0.0625)} tile range"));
            tooltips.Insert(7, new TooltipLine(Mod, "YoyoSpeed", $"{Math.Round(ProjectileID.Sets.YoyosTopSpeed[item.shoot] * (46f / 9f))} mph top speed"));

            switch (item.type)
            {
                case ItemID.CorruptYoyo:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Ruptures the purity of the enemy"));

                    break;
                case ItemID.CrimsonYoyo:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Blood will be shed on the battlefield!"));

                    break;
                case ItemID.JungleYoyo:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Poisons enemies and stings them during combat"));

                    break;
                case ItemID.HelFire:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Explodes violently when attacking enemies"));

                    break;
                case ItemID.Yelets:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Fires fragmenting seeds on contact with enemies"));

                    break;
                case ItemID.Kraken:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Swarms enemies with dozens of detonating bubbles when hit"));

                    break;
                case ItemID.TheEyeOfCthulhu:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Rapidly fires death lasers at nearby enemies"));

                    break;
                case ItemID.Chik:
                    tooltips.Insert(8, new TooltipLine(Mod, "YoyoBonus", "Ignores 40 points of enemy Defense"));

                    break;
            }
        }
    }
}