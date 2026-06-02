using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace YoyoRebalance.Helpers;

public static class RecipeHelper
{
    public static RecipeGroup SilverBarRecipeGroup;
    public static RecipeGroup GoldBarRecipeGroup;
    public static RecipeGroup CobaltBarRecipeGroup;
    
    public static RecipeGroup CorruptYoyoRecipeGroup;

    public static void CreateRecipeGroups()
    {
        SilverBarRecipeGroup = CreateRecipeGroup("SilverBar", Lang.GetItemNameValue(ItemID.SilverBar), ItemID.SilverBar, ItemID.TungstenBar);
        GoldBarRecipeGroup = CreateRecipeGroup("GoldBar", Lang.GetItemNameValue(ItemID.GoldBar), ItemID.GoldBar, ItemID.PlatinumBar);
        CobaltBarRecipeGroup = CreateRecipeGroup("CobaltBar", Lang.GetItemNameValue(ItemID.CobaltBar), ItemID.CobaltBar, ItemID.PalladiumBar);
        
        CorruptYoyoRecipeGroup = CreateRecipeGroup("CorruptYoyo", $"{Language.GetTextValue("RandomWorldName_Noun.Corruption")} {Language.GetTextValue("ProjectileName.CorruptYoyo")}", ItemID.CorruptYoyo, ItemID.CrimsonYoyo);
    }
    
    private static RecipeGroup CreateRecipeGroup(string name, string type, params int[] itemIds)
    {
        var group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {type}", itemIds);
        
        RecipeGroup.RegisterGroup(name, group);

        return group;
    }
}