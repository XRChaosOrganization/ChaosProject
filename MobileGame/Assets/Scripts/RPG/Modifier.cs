using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public StatSO statSO;
    public enum Mode {ADD, MINUS, MULT, DIVIDE};
    public Mode mode;
    public enum ModeType {FLAT, PERCENT};
    public ModeType modeType;
    public float modifierValue; 
}