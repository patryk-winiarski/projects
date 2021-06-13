using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    [System.Serializable]
    public class CharLevelUps
    {
        public int maxHealth;
        public int maxEnergy;
        public int baseDamage;
        public float baseResistance;
        public float maxEncumbrance;
    }

    #region Variable initialization

    public bool setManually = false;
    public bool saveDataOnClose = false;

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxEnergy = 0;
    public int currentEnergy = 0;

    public int currentWealth = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0f;
    public float currentResistance = 0f;

    public float maxEncumbrance = 0f;
    public float currentEncumbrance = 0f;

    public int charExperience = 0;
    public int charLevel = 0;

    public CharLevelUps[] charLevelUps;

    #region Inventory

    public ItemPickUp weapon { get; private set; }
    public ItemPickUp head { get; private set; }
    public ItemPickUp chest { get; private set; }
    public ItemPickUp hands { get; private set; }
    public ItemPickUp legs { get; private set; }
    public ItemPickUp feet { get; private set; }
    public ItemPickUp acc1 { get; private set; }
    public ItemPickUp acc2 { get; private set; }

    #endregion

    #region Components


    #endregion

    #endregion

    #region Stat Increasers

    public void ApplyHealth(int healthAmount)
    {
        if ((currentHealth + healthAmount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void ApplyEnergy(int energyEmount)
    {
        if ((currentEnergy + energyEmount) > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy += energyEmount;
        }
    }

    public void ApplyWealth(int wealthAmount)
    {
        currentWealth += wealthAmount;
    }

    public void ApplyBuff(int amount, ItemPickUp buffPickUp)
    {
        switch (buffPickUp.itemDefinition.itemBuffSubType)
        {
            case ItemBuffSubType.Damage:
                {

                    break;
                }
            case ItemBuffSubType.Health:
                {

                    break;
                }
            case ItemBuffSubType.Energy:
                {

                    break;
                }
            case ItemBuffSubType.Resistance:
                {

                    break;
                }
            case ItemBuffSubType.Experience:
                {

                    break;
                }
            case ItemBuffSubType.Encumbrance:
                {

                    break;
                }

        }
    }

    private void ApplyDamageBuff()
    {

    }

    #endregion

    #region Stat Reducers

    public void ReduceHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //Death();
        }
    }

    public void ReduceEnergy(int amount)
    {
        currentEnergy -= amount;

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }
    }

    #endregion

    #region Item Equip

    public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInventory inventory, GameObject weaponSlot)
    {
        weapon = weaponPickUp;
        currentDamage = baseDamage + weapon.itemDefinition.itemAmount;
    }

    public bool UnequipWeapon(ItemPickUp weaponPickUp, CharacterInventory inventory, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;

        if (weapon != null)
        {
            if (weapon == weaponPickUp)
            {
                previousWeaponSame = true;
            }

            Destroy(weaponSlot.transform.GetChild(0).gameObject);
            weapon = null;
            currentDamage = baseDamage;
        }

        return previousWeaponSame;
    }

    public void EquipArmor(ItemPickUp armorPickUp, CharacterInventory inventory)
    {
        switch (armorPickUp.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                head = armorPickUp;
                break;
            case ItemArmorSubType.Chest:
                chest = armorPickUp;
                break;
            case ItemArmorSubType.Hands:
                hands = armorPickUp;
                break;
            case ItemArmorSubType.Legs:
                legs = armorPickUp;
                break;
            case ItemArmorSubType.Feet:
                feet = armorPickUp;
                break;
        }
    }

    public bool UnequipArmor(ItemPickUp armorPickUp, CharacterInventory inventory)
    {
        bool previousArmorSame = false;

        switch (armorPickUp.itemDefinition.itemArmorSubType)
        {
            case ItemArmorSubType.Head:
                if (head != null)
                {
                    if (head == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= head.itemDefinition.itemAmount;
                    head = null;
                }
                break;
            case ItemArmorSubType.Chest:
                if (chest != null)
                {
                    if (chest == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= chest.itemDefinition.itemAmount;
                    chest = null;
                }
                break;
            case ItemArmorSubType.Hands:
                if (hands != null)
                {
                    if (hands == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= hands.itemDefinition.itemAmount;
                    hands = null;
                }
                break;
            case ItemArmorSubType.Legs:
                if (legs != null)
                {
                    if (legs == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= legs.itemDefinition.itemAmount;
                    legs = null;
                }
                break;
            case ItemArmorSubType.Feet:
                if (feet != null)
                {
                    if (feet == armorPickUp)
                    {
                        previousArmorSame = true;
                    }

                    currentResistance -= feet.itemDefinition.itemAmount;
                    feet = null;
                }
                break;
        }

        return previousArmorSame;
    }
    #endregion

    #region Character Level-Up and Death
    private void LevelUp()
    {
        charLevel += 1;

        maxHealth = charLevelUps[charLevel].maxHealth;
        maxEnergy = charLevelUps[charLevel].maxEnergy;
        maxEncumbrance = charLevelUps[charLevel].maxEncumbrance;
        baseDamage = charLevelUps[charLevel].baseDamage;
        baseResistance = charLevelUps[charLevel].baseResistance;
    }

    private void Death()
    {
        Debug.Log("You're dead");
    }
    #endregion

}
