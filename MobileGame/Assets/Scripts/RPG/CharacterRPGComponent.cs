using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRPGComponent : MonoBehaviour
{
    public List<StatSO> baseStats; 
    public List<ItemSO> baseItems; 
    [SerializeField] private List<StatInstance> statInstances; 
    [SerializeField] private List<ItemInstance> itemInstances; 


    void Start()
    {
        //Inits
        InitCharacterStats();
        InitCharacterItems();

        //Apply our base items modifier to the character
        UpdateRPGStatsFromItems();
    }

    private void InitCharacterStats()
    {
        for (int i = 0; i < baseStats.Count; i++)
        {
            statInstances.Add(new StatInstance(baseStats[i].stat.statName, baseStats[i].stat.statBaseValue));
        }
    }

    private void InitCharacterItems ()
    {
        for (int i = 0; i < baseItems.Count; i++)
        {
            itemInstances.Add(new ItemInstance(baseItems[i]));
        }
    }

    //Go through all items & for each modifiers send them to our stats
    public void UpdateRPGStatsFromItems ()
    {
        for (int i = 0; i < baseItems.Count; i++)
        {
            for (int j = 0; j < baseItems[i].item.modifiers.Count; j++)
            {
                Modifier mod = baseItems[i].item.modifiers[j]; 
                ApplyModifierToStat(mod);
            }
        }
    }

    public void ApplyModifierToStat (Modifier _modifier)
    {
        StatInstance statToModify = null; 

        //Select the stat to modify
        for (int i = 0; i < statInstances.Count; i++)
        {
            if(statToModify == null && _modifier.statSO.stat.statName == statInstances[i].statName)
            {
                statToModify = statInstances[i];
            }
        }

        if(statToModify == null)
        {
            Debug.LogWarning("No stat to modify found on this RPG Component, did you forget to add this stat ? : " + _modifier.statSO.stat.statName);
            return; 
        }

        //A refaire plus proprement
        if(_modifier.mode == Modifier.Mode.ADD)
        {
            if(_modifier.modeType == Modifier.ModeType.FLAT)
                statToModify.statCurrentValue += _modifier.modifierValue;
            else if(_modifier.modeType == Modifier.ModeType.PERCENT)
                statToModify.statCurrentValue += (_modifier.modifierValue / 100) * statToModify.statCurrentValue;
        }
        else if(_modifier.mode == Modifier.Mode.DIVIDE)
        {
            if(_modifier.modeType == Modifier.ModeType.FLAT)
                statToModify.statCurrentValue /= _modifier.modifierValue;
            else if(_modifier.modeType == Modifier.ModeType.PERCENT)
                statToModify.statCurrentValue /= (_modifier.modifierValue / 100) * statToModify.statCurrentValue;
        }
        else if(_modifier.mode == Modifier.Mode.DIVIDE)
        {
            if(_modifier.modeType == Modifier.ModeType.FLAT)
                statToModify.statCurrentValue -= _modifier.modifierValue;
            else if(_modifier.modeType == Modifier.ModeType.PERCENT)
                statToModify.statCurrentValue -= (_modifier.modifierValue / 100) * statToModify.statCurrentValue;
        }
        else if(_modifier.mode == Modifier.Mode.DIVIDE)
        {
            if(_modifier.modeType == Modifier.ModeType.FLAT)
                statToModify.statCurrentValue *= _modifier.modifierValue;
            else if(_modifier.modeType == Modifier.ModeType.PERCENT)
                statToModify.statCurrentValue *= (_modifier.modifierValue / 100) * statToModify.statCurrentValue;
        }
        else
        {
            Debug.LogError("This modifier is trying to apply an incorrect/wrong mod to the value : " + _modifier.statSO.stat.statName);
        }
    }

    public List<ItemInstance> GetItems ()
    {
        return itemInstances; 
    }

    public List<StatInstance> GetStats ()
    {
        return statInstances; 
    }
}
