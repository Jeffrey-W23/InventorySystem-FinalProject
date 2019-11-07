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
public class ItemDatabase : MonoBehaviour
{
    //
    public List<Item> m_aoItems = new List<Item>();

    //
    private void Awake()
    {
        //
        BuildDatabase();
    }

    //
    public Item GetItem(int nId)
    {
        //
        return m_aoItems.Find(item => item.m_nId == nId);
    }

    //
    public Item GetItem(string strTitle)
    {
        //
        return m_aoItems.Find(item => item.m_strTitle == strTitle);
    }

    //
    void BuildDatabase()
    {
        //
        m_aoItems = new List<Item>()
        {
            //
            new Item
            (
                //
                0, "Diamond Sword", "A Sword made with diamond.",
                
                //
                new Dictionary<string, int>
                {
                    {"Power", 15},
                    {"Defence", 10},
                }
            ),

            //
            new Item
            (
                //
                1, "Diamond Ore", "A beautfiul diamond",
                
                //
                new Dictionary<string, int>
                {
                    {"Value", 444}
                }
            ),

            //
            new Item
            (
                //
                2, "Silver Pick", "A very C# pick",
                
                //
                new Dictionary<string, int>
                {
                    {"Power", 5},
                    {"Mining", 333}
                }
            )
        };
    }
}