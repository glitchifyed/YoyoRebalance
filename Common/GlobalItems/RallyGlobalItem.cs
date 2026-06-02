using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YoyoRebalance.Common.GlobalItems
{
    public class RallyGlobalItem : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Rally;
        }

        public override void SetDefaults(Item item)
        {
            //item.StatsModifiedBy.Add(Mod);


        }
    }
}