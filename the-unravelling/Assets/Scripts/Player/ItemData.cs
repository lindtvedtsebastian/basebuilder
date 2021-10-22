using System;
using UnityEngine;

/// <summary>
/// Type of item represented by ItemData object.
/// </summary>
public enum ItemType {
    GameObject,
    Tile,
}

/// <summary>
/// ItemData is a generic class for storing information about an item.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData", order = 2)]
public class ItemData : ScriptableObject {
    // Numeric ID that uniquely identifies this item.
    public int id;
    
    // Name of the item.
    public string itemName;

    // Type of the item.
    public ItemType type;

    // Preview sprite for the item, useful for inventories etc.
    public Sprite preview;

    // The prefab representing the real physical manifestation of the item.
    public GameObject manifestation;
}
