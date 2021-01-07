using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/RPG/Stat", order = 0)]
public class StatSO : ScriptableObject
{
    public StatInstance stat; 
}

[System.Serializable]
public class StatInstance 
{
    public string statName; 
    public float statBaseValue; 

    [SerializeField] private float currentValue;
    public float statCurrentValue
    {
        get { return currentValue; }
        set 
        { 
            if(value > 0)
                currentValue = value; 
        }
    }
    

    public StatInstance(string _name, float _baseValue) 
    {
        //Construct & Init our stat
        statName = _name;
        statBaseValue = _baseValue;
        statCurrentValue = statBaseValue;
    }
}
