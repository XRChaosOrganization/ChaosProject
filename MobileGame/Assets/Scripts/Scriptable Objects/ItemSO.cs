using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "Data/RPG/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public ItemBaseSO itemBase; 
    public ItemInstance item; 
}

[System.Serializable]
public class ItemInstance 
{
    public string itemName; 
    public Sprite itemSprite; 
    public bool isStackable = false; 
    public enum ItemRarity {COMMON, RARE, EPIC, LEGENDARY}; 
    public ItemRarity itemRarity; 
    public List<Modifier> modifiers; 

    public ItemInstance (string _name, Sprite _sprite, bool _stackable, ItemRarity _rarity, List<Modifier> _mods = null)
    {
        itemName = _name; 
        itemSprite = _sprite;
        isStackable = _stackable;
        itemRarity = _rarity;
        
        //Generate Mods or not 
        if(_mods == null)
            modifiers = GenerateMods();
        else
            modifiers = _mods;
    }

    public ItemInstance (ItemSO _itemSO)
    {
        itemName = _itemSO.item.itemName; 
        itemSprite = _itemSO.item.itemSprite; 
        itemRarity = _itemSO.item.itemRarity; 
        modifiers = _itemSO.item.modifiers; 
    }

    private List<Modifier> GenerateMods ()
    {
        return new List<Modifier>();
    }
}

[CustomEditor(typeof(ItemSO))]
[CanEditMultipleObjects]
public class ItemSOEditor : Editor 
{
    SerializedProperty itemBaseProp;
    SerializedProperty itemProp;

    void OnEnable()
    {
        // Fetch the objects from the GameObject script to display in the inspector
        itemBaseProp = serializedObject.FindProperty("itemBase");
        itemProp = serializedObject.FindProperty("item");
    }

    public override void OnInspectorGUI()
    {
        var ie = (ItemSO)target;

        EditorGUILayout.PropertyField(itemBaseProp, new GUIContent("Item Base"));

        if(ie.itemBase != null)
        {
            if(EditorGUILayout.Foldout(true, "Item Base"))
            {
                EditorGUILayout.LabelField("Base Name : " + ie.itemBase.itemBase.baseName);
            }
        }

        EditorGUILayout.PropertyField(itemProp, new GUIContent("Item"));
        serializedObject.ApplyModifiedProperties();
    }
}