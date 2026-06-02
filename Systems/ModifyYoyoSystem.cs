using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YoyoRebalance.Helpers;

namespace YoyoRebalance.Systems
{
    public class ModifyYoyoSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            YoyoHelper.ModifyYoyo(ItemID.WoodYoyo, 12, -1f, 8f, 30f);
            YoyoHelper.ModifyYoyo(ItemID.CrimsonYoyo, 15, 2f, 7f, 120f);
            YoyoHelper.ModifyYoyo(ItemID.CorruptYoyo, 17, 7f, 10f, 50f);
            YoyoHelper.ModifyYoyo(ItemID.Valor, 25, 20f, 24f, 100f);
            YoyoHelper.ModifyYoyo(ItemID.Cascade, 33, 10f, 14f, 70f);
            YoyoHelper.ModifyYoyo(ItemID.HelFire, 62, 18f, 14f, 80f);
            YoyoHelper.ModifyYoyo(ItemID.Yelets, 50, 24f, 19f, 60f);
            YoyoHelper.ModifyYoyo(ItemID.Kraken, 32, 16f, 16f, 120f);
            YoyoHelper.ModifyYoyo(ItemID.TheEyeOfCthulhu, 100, 30f, 18f, 60f);
            
            YoyoHelper.ModifyYoyo(ItemID.Rally, damage: 16);
            YoyoHelper.ModifyYoyo(ItemID.JungleYoyo, damage: 14);
            YoyoHelper.ModifyYoyo(ItemID.Amarok, damage: 49, tileRange: 22f);
            YoyoHelper.ModifyYoyo(ItemID.Chik, damage: 25, tileRange: 40f, lifetime: 12f, speedMph: 120f);
        }

        public override void AddRecipeGroups()
        {
            RecipeHelper.CreateRecipeGroups();
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.Rally)
                .AddRecipeGroup(RecipeHelper.SilverBarRecipeGroup, 12)
                .AddRecipeGroup(RecipeHelper.GoldBarRecipeGroup, 5)
                .AddIngredient(ItemID.Cobweb, 20)
                
                .AddTile(TileID.Anvils)

                .Register();
            
            Recipe.Create(ItemID.Cascade)
                .AddIngredient(ItemID.Valor)
                .AddIngredient(ItemID.HellstoneBar, 18)
                
                .AddTile(TileID.Anvils)

                .Register();
            
            Recipe.Create(ItemID.Amarok)
                .AddRecipeGroup(RecipeHelper.CobaltBarRecipeGroup, 14)
                .AddIngredient(ItemID.FrostCore)
                .AddIngredient(ItemID.PinkIceBlock, 20)

                .AddTile(TileID.Anvils)

                .Register();

            Recipe.Create(ItemID.HelFire)
                .AddIngredient(ItemID.Cascade)
                .AddIngredient(ItemID.HallowedBar, 12)
                //.AddIngredient(ItemID.LivingFireBlock, 20) // TODO: make souls of sunlight, blight, flamelight, firelight, scorialight

                .AddTile(TileID.MythrilAnvil)

                .Register();

            Recipe.Create(ItemID.TheEyeOfCthulhu)
                .AddIngredient(ItemID.ChlorophyteBar, 18)
                .AddIngredient(ItemID.HallowedBar, 14)
                .AddIngredient(ItemID.SoulofSight, 12)
                .AddIngredient(ItemID.Lens, 10)
                .AddIngredient(ItemID.BeetleHusk, 8)

                .AddTile(TileID.MythrilAnvil)

                .Register();
        }

        public override void PostAddRecipes()
        {
            for (var i = 0; i < Recipe.numRecipes; i++)
            {
                var recipe = Main.recipe[i];
                
                if (recipe.HasResult(ItemID.JungleYoyo))
                    recipe.AddRecipeGroup(RecipeHelper.CorruptYoyoRecipeGroup);
                else if (recipe.HasResult(ItemID.Chik))
                    recipe.AddIngredient(ItemID.GelBalloon, 25);
            }
        }
    }
}