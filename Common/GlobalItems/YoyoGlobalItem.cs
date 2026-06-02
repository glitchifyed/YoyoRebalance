using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
            
            tooltips.Insert(5, new TooltipLine(Mod, "YoyoLifetime", lifetime == -1 ? Language.GetTextValue("Mods.YoyoRebalance.Tooltips.YoyoLifetimeUnlimited") : Language.GetText("Mods.YoyoRebalance.Tooltips.YoyoLifetime").Format(lifetime)));
            tooltips.Insert(6, new TooltipLine(Mod, "YoyoRange", Language.GetText("Mods.YoyoRebalance.Tooltips.YoyoRange").Format(Math.Floor(ProjectileID.Sets.YoyosMaximumRange[item.shoot] * 0.0625))));
            tooltips.Insert(7, new TooltipLine(Mod, "YoyoSpeed", Language.GetText("Mods.YoyoRebalance.Tooltips.YoyoSpeed").Format(Math.Round(ProjectileID.Sets.YoyosTopSpeed[item.shoot] * (46f / 9f)))));

            var key = $"Mods.YoyoRebalance.Lore.{item.type}";
            
            if (Language.Exists(key))
            {
                tooltips.Insert(8, new TooltipLine(Mod, "YoyoSpeed", Language.GetTextValue(key)));
            }
        }
    }
}