using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinition;
    public CharacterInventory inventory;
    public GameObject characterWeaponSlot;

    #region Constructors

    public CharacterStats()
    {
        inventory = CharacterInventory.instance;
    }
    #endregion

    #region Initializations
    private void Start()
    {
        if (!characterDefinition.setManually)
        {
            characterDefinition.maxHealth = 100;
            characterDefinition.currentHealth = 80;

            characterDefinition.maxEnergy = 100;
            characterDefinition.currentEnergy = 100;

            characterDefinition.currentWealth = 0;

            characterDefinition.baseResistance = 0f;
            characterDefinition.currentResistance = 0f;

            characterDefinition.maxEncumbrance = 10f;
            characterDefinition.currentEncumbrance = 0f;

            characterDefinition.charExperience = 0;
            characterDefinition.charLevel = 1;
        }
    }

    #endregion

    #region Stat Increasers
    public void ApplyHealth(int amount)
    {
        characterDefinition.ApplyHealth(amount);
    }

    public void ApplyEnergy(int amount)
    {
        characterDefinition.ApplyEnergy(amount);
    }

    public void ApplyWealth(int amount)
    {
        characterDefinition.ApplyWealth(amount);
    }

    public void ApplyBuff(int amount, ItemPickUp buffPickUp)
    {
        characterDefinition.ApplyBuff(amount, buffPickUp);
    }
    #endregion

    #region Stat Reducers
    public void ReduceHealth(int amount)
    {
        characterDefinition.ReduceHealth(amount);
    }

    public void ReduceEnergy(int amount)
    {
        characterDefinition.ReduceEnergy(amount);
    }

    #endregion

    #region Weapon and armor change

    public void ChangeWeapon(ItemPickUp weaponPickUp)
    {
        if (!characterDefinition.UnequipWeapon(weaponPickUp, inventory, characterWeaponSlot))
        {
            characterDefinition.EquipWeapon(weaponPickUp, inventory, characterWeaponSlot);
        }
    }

    public void ChangeArmor(ItemPickUp armorPickUp)
    {
        if (!characterDefinition.UnequipArmor(armorPickUp, inventory))
        {
            characterDefinition.EquipArmor(armorPickUp, inventory);
        }
    }

    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.currentHealth;
    }

    public int GetEnergy()
    {
        return characterDefinition.currentEnergy;
    }

    public int GetWealth()
    {
        return characterDefinition.currentWealth;
    }

    public float GetResistance()
    {
        return characterDefinition.currentResistance;
    }

    public int GetDamage()
    {
        return characterDefinition.currentDamage;
    }

    public float GetEncumbrance()
    {
        return characterDefinition.currentEncumbrance;
    }

    public int GetLevel()
    {
        return characterDefinition.charLevel;
    }

    public int GetExperience()
    {
        return characterDefinition.charExperience;
    }

    public ItemPickUp GetCurrentWeapon()
    {
        return characterDefinition.weapon;
    }

    #endregion
}
