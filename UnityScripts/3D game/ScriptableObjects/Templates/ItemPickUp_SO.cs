using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { Health, Wealth, Energy, Weapon, Armor, Buff, Empty };

public enum ItemArmorSubType { None, Head, Chest, Hands, Legs, Feet };

public enum ItemBuffSubType { None, Health, Energy, Experience, Damage, Resistance, Encumbrance }

[CreateAssetMenu(fileName = "New Item", menuName = "Spawnable Item/New pick-up", order = 1)]
public class ItemPickup_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.Empty;
    public ItemArmorSubType itemArmorSubType = ItemArmorSubType.None;
    public ItemBuffSubType itemBuffSubType = ItemBuffSubType.None;
    public int itemAmount = 0;
    public int spawnChanceWeight = 0;

    public Material itemMaterial = null;
    public Sprite itemIcon = null;
    public GameObject itemSpawnObject = null;
    public GameObject weaponSlotObject = null;

    public bool isEquipped = false;
    public bool isInteractable = false;
    public bool isStorable = false;
    public bool isUnique = false;
    public bool isIndestructable = false;
    public bool isQuestItem = false;
    public bool isStackable = false;
    public bool destroyOnUse = false;
    public float itemWeight = 0f;
}
