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
public class Item
{
    //
    public int m_nId;

    //
    public string m_strTitle;

    //
    public string m_strDescription;

    //
    public Sprite m_sIcon;

    //
    public Dictionary<string, int> m_dStats = new Dictionary<string, int>();


    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Item(int nId, string strTitle, string strDescription, Dictionary<string, int> dStats)
    {
        //
        m_nId = nId;
        m_strTitle = strTitle;
        m_strDescription = strDescription;
        m_sIcon = Resources.Load<Sprite>("Sprites/Items/" + strTitle);
        m_dStats = dStats;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Item(Item oItem)
    {
        //
        m_nId = oItem.m_nId;
        m_strTitle = oItem.m_strTitle;
        m_strDescription = oItem.m_strDescription;
        m_sIcon = Resources.Load<Sprite>("Sprites/Items/" + oItem.m_strTitle);
        m_dStats = oItem.m_dStats;
    }
}