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
public class UIHotbar : MonoBehaviour
{
    //
    public UIInventory m_gInventoryUI;

    //
    public List<UIItem> m_aoUIItems = new List<UIItem>();

    //
    public GameObject m_gSlotPrefab;

    //
    public GameObject m_tSlotPanel;

    //
    public int m_nSlots = 4;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        //
        for (int i = 0; i < m_nSlots; i++)
        {
            //
            GameObject gInstance = Instantiate(m_gSlotPrefab);

            //
            gInstance.transform.SetParent(m_tSlotPanel.transform);
        }

        //
        m_aoUIItems = m_gInventoryUI.m_aoUIItems;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void UpdateSlot(int nSlot, Item oItem)
    {
        //
        m_aoUIItems[nSlot].UpdateItem(oItem);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void AddItem(Item oItem)
    {
        //
        UpdateSlot(m_aoUIItems.FindIndex(i => i.m_oItem == null), oItem);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void RemoveItem(Item oItem)
    {
        //
        UpdateSlot(m_aoUIItems.FindIndex(i => i.m_oItem == oItem), null);
    }
}