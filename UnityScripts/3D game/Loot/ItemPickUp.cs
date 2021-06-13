using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemPickup_SO itemDefinition;
    public CharacterStats stats;
    private CharacterInventory inventory;
    GameObject player;

    #region Constructors

    public ItemPickUp()
    {
        inventory = CharacterInventory.instance;
    }

    #endregion

    void Start()
    {
        if (stats != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            stats = player.GetComponent<CharacterStats>();
        }
    }

    void StoreItemInInventory()
    {
        inventory.StoreItem(this);
    }

    public void UseItem()
    {
        switch (itemDefinition.itemType)
        {
            case ItemTypeDefinitions.Health:
                {
                    stats.ApplyHealth(itemDefinition.itemAmount);
                    Destroy(this.gameObject);
                    Debug.Log("Current health: " + stats.GetHealth());
                    break;
                }
            case ItemTypeDefinitions.Energy:
                {
                    stats.ApplyEnergy(itemDefinition.itemAmount);
                    Destroy(this);
                    break;
                }
            case ItemTypeDefinitions.Armor:
                {
                    stats.ChangeArmor(this);
                    Destroy(this);
                    break;
                }
            case ItemTypeDefinitions.Buff:
                {
                    stats.ApplyBuff(itemDefinition.itemAmount, this);
                    Destroy(this);
                    break;
                }
            case ItemTypeDefinitions.Wealth:
                {
                    stats.ApplyWealth(itemDefinition.itemAmount);
                    Destroy(this);
                    break;
                }
            case ItemTypeDefinitions.Weapon:
                {
                    stats.ChangeWeapon(this);
                    Destroy(this);
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (itemDefinition.isStorable)
            {
                StoreItemInInventory();
            }
            else
            {
                UseItem();
            }
        }
    }
}
