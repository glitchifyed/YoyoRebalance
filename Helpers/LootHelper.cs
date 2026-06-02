using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace YoyoRebalance.Helpers;

public static class LootHelper
{
    public static void ModifyFishronLoot(ILoot loot)
    {
        foreach (var rule in loot.Get())
        {
            var newRule = GetRuleFromRule(rule, filterRule => filterRule is OneFromOptionsDropRule);

            if (newRule == null)
                continue;
                
            AddItemToRuleFromOptions((OneFromOptionsDropRule)newRule, ItemID.Kraken);
        }
    }
    
    public static void AddItemToRuleFromOptions(OneFromOptionsDropRule rule, int itemId)
    {
        var itemIds = rule.dropIds.ToList();
                
        itemIds.Add(itemId);
        
        rule.dropIds = itemIds.ToArray();
    }

    public static IItemDropRule GetRuleFromRule(IItemDropRule rule, Func<IItemDropRule, bool> filter)
    {
        if (filter(rule))
            return rule;

        if (rule.ChainedRules == null)
            return null;

        foreach (var chain in rule.ChainedRules)
        {
            var chainedRule = GetRuleFromRule(chain.RuleToChain, filter);

            if (chainedRule != null)
                return chainedRule;
        }

        return null;
    }
    
    public static void RemoveItemFromLoot(ILoot loot, int itemId)
    {
        loot.RemoveWhere(rule => ProcessRule(rule, itemId));
    }

    private static bool ProcessRule(IItemDropRule rule, int itemId)
    {
        if (rule.ChainedRules != null)
        {
            foreach (var chain in rule.ChainedRules.ToList())
            {
                if (ProcessRule(chain.RuleToChain, itemId))
                    rule.ChainedRules.Remove(chain);
            }
        }

        if (rule is DropBasedOnExpertMode expertDrop)
        {
            if (expertDrop.ruleForNormalMode is CommonDrop normal && ProcessRule(normal, itemId))
            {
                expertDrop.ruleForNormalMode = ItemDropRule.DropNothing();
            }
            
            if (expertDrop.ruleForExpertMode is CommonDrop expert && ProcessRule(expert, itemId))
            {
                expertDrop.ruleForExpertMode = ItemDropRule.DropNothing();
            }

            return false;
        }
            //return expertDrop.ruleForNormalMode is CommonDrop normal && ProcessRule(normal, itemId) || expertDrop.ruleForExpertMode is CommonDrop expert && ProcessRule(expert, itemId);;
        
        if (rule is not OneFromOptionsDropRule oneFromOptionsDropRule)
            return rule is CommonDrop commonDrop && commonDrop.itemId == itemId;
        
        var filtered = new List<int>();
            
        foreach (var id in oneFromOptionsDropRule.dropIds)
        {
            if (id != itemId)
                filtered.Add(id);
        }
            
        oneFromOptionsDropRule.dropIds = filtered.ToArray();
            
        return false;
    }
}