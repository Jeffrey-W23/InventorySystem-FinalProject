﻿//--------------------------------------------------------------------------------------
// Purpose: 
//
// Description: 
//
// Author: Thomas Wiltshire
//--------------------------------------------------------------------------------------

// using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// f
//--------------------------------------------------------------------------------------
public class PlayerContainer : Container
{
    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public PlayerContainer(Inventory oInventory, Inventory oPlayerInventory, int nSlots) : base(oInventory, oPlayerInventory, nSlots)
    {
        // loop through each slot
        for (int i = 0; i < m_nSlots; i++)
        {
            // build the inventory slots
            AddSlot(oPlayerInventory, 3 + i);
        }

        // loop through each slot
        for (int i = 0; i < m_nSlots - 3; i++)
        {
            // build the inventory slots
            AddSlot(oPlayerInventory, i);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public override GameObject GetPrefab()
    {
        // return the inventory managers instance container prefab
        return InventoryManager.m_gInstance.GetContainerPrefab("Player Inventory");
    }
}