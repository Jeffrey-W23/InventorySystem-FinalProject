//--------------------------------------------------------------------------------------
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
public class Container
{
    //
    protected int m_nSlots;

    //
    protected GameObject m_gPrefab;

    //
    protected Inventory m_oInventory;

    //
    protected Inventory m_oPlayerInventory;

    //
    protected List<ItemSlot> m_agSlots = new List<ItemSlot>();

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Container(Inventory oInventory, Inventory oPlayerInventory, int nSlots)
    {
        // set default values
        m_oInventory = oInventory;
        m_oPlayerInventory = oPlayerInventory;
        m_nSlots = nSlots;

        // Open the container
        Open();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void AddSlot(Inventory oInventory, int nId, Transform m_tParent)
    {
        // get the slot component
        GameObject gInstance = Object.Instantiate(InventoryManager.m_gInstance.m_gSlotPrefab);
        ItemSlot gSlot = gInstance.GetComponent<ItemSlot>();

        // set the postion of the slot to the parent
        gInstance.transform.SetParent(m_tParent);

        // add the slot to the slots array
        m_agSlots.Add(gSlot);

        // set the slot to the slot object
        gSlot.SetSlot(oInventory, nId, this);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void UpdateSlots()
    {
        // for each slot in slots array
        foreach (ItemSlot i in m_agSlots)
        {
            // Update the slot
            i.UpdateSlot();
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void Open()
    {
        // instantiate the container prefab at the inventory manager
        m_gPrefab = Object.Instantiate(GetPrefab(), InventoryManager.m_gInstance.transform);

        // set the prefab to draw at the back
        m_gPrefab.transform.SetAsFirstSibling();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void Close()
    {
        // destroy prefab container object
        Object.Destroy(m_gPrefab);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public virtual GameObject GetPrefab()
    {
        // return null, will be handled in children
        return null;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public GameObject GetSpawnedContainer()
    {
        // return the prefab
        return m_gPrefab;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Inventory GetInventory()
    {
        // return the container inventory
        return m_oInventory;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Inventory GetPlayerInventory()
    {
        // return the player inventory
        return m_oPlayerInventory;
    }
}
