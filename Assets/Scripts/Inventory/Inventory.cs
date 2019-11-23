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
public class Inventory : MonoBehaviour
{
    //
    public List<Item> m_aoItems = new List<Item>();

    //
    public ItemDatabase m_oItemDatabase;

    //
    public UIInventory m_gInventoryUI;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Start()
    {
        //
        GiveItem(0);
        GiveItem(1);
        GiveItem(2);

        //
        m_gInventoryUI.gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.I))
        {
            //
            m_gInventoryUI.gameObject.SetActive(!m_gInventoryUI.gameObject.activeSelf);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void GiveItem(int nId)
    {
        //
        Item oItem = m_oItemDatabase.GetItem(nId);

        //
        m_aoItems.Add(oItem);

        //
        m_gInventoryUI.AddItem(oItem);

        //
        Debug.Log("Added Item: " + oItem.m_strTitle);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void GiveItem(string strTitle)
    {
        //
        Item oItem = m_oItemDatabase.GetItem(strTitle);

        //
        m_aoItems.Add(oItem);

        //
        m_gInventoryUI.AddItem(oItem);

        //
        Debug.Log("Added Item: " + oItem.m_strTitle);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Item CheckForItem(int nId)
    {
        //
        return m_aoItems.Find(item => item.m_nId == nId);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void RemoveItem(int nId)
    {
        //
        Item oItem = CheckForItem(nId);

        //
        if (oItem != null)
        {
            //
            m_aoItems.Remove(oItem);

            //
            m_gInventoryUI.RemoveItem(oItem);

            //
            Debug.Log("Item Removed:" + oItem.m_strTitle);
        }
    }
}