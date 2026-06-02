using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YoyoRebalance.Helpers;

namespace YoyoRebalance.Common.GlobalItems;

public class BossBagLoot : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return ItemID.Sets.BossBag[item.type];
    }

    public override void ModifyItemLoot(Item item, ItemLoot loot)
    {
        if (item.type == ItemID.FishronBossBag)
            LootHelper.ModifyFishronLoot(loot);
    }
}