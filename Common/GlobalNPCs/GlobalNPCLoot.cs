using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YoyoRebalance.Helpers;

namespace YoyoRebalance.Common.GlobalNPCs
{
    public class GlobalNpcLoot : GlobalNPC
    {
        private static void RemoveGlobalDrops(ILoot loot)
        {
            LootHelper.RemoveItemFromLoot(loot, ItemID.Cascade);
            LootHelper.RemoveItemFromLoot(loot, ItemID.HelFire);
            LootHelper.RemoveItemFromLoot(loot, ItemID.Amarok);
            LootHelper.RemoveItemFromLoot(loot, ItemID.Yelets);
            LootHelper.RemoveItemFromLoot(loot, ItemID.Kraken);
        }
        
        public override void ModifyNPCLoot(NPC npc, NPCLoot loot)
        {
            if (npc.type == NPCID.DukeFishron)
            {
                LootHelper.ModifyFishronLoot(loot);

                return;
            }
            
            LootHelper.RemoveItemFromLoot(loot, ItemID.Rally);
            LootHelper.RemoveItemFromLoot(loot, ItemID.TheEyeOfCthulhu);
            
            RemoveGlobalDrops(loot);
        }

        public override void ModifyGlobalLoot(GlobalLoot loot)
        {
            RemoveGlobalDrops(loot);
        }
    }
}