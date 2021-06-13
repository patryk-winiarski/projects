using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    #region Variable Declarations
    public static CharacterInventory instance;
    #endregion

    #region Initializations
    private void Start()
    {
        instance = this;
    }
    #endregion

    public void StoreItem(ItemPickUp itemToStore)
    {

    }

    private void TryPickUp()
    {

    }

    private bool AddItemToInv(bool finishedAdding)
    {
        return true;
    }

    //private void AddItemToBar(InventoryEntry itemforHotBar)
    //{

    //}

    private void DisplayInventory()
    {

    }

    private void FillInventoryDisplay()
    {

    }

    public void TriggerItemUse(int itemToUseID)
    {

    }
}
