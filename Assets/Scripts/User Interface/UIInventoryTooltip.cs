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
using UnityEngine.UI;

//--------------------------------------------------------------------------------------
// f
//--------------------------------------------------------------------------------------
public class UIInventoryTooltip : MonoBehaviour
{
    //
    private Text m_tTooltip;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    void Start()
    {
        //
        m_tTooltip = GetComponentInChildren<Text>();

        //
        gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void GenerateTooltip(Item oItem)
    {
        //
        string strStats = "";

        //
        if (oItem.m_dStats.Count > 0)
        {
            //
            foreach (var i in oItem.m_dStats)
            {
                //
                strStats += i.Key.ToString() + ": " + i.Value.ToString() + "\n";
            }
        }

        //
        string strTooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>", oItem.m_strTitle, oItem.m_strDescription, strStats);

        //
        m_tTooltip.text = strTooltip;

        //
        gameObject.SetActive(true);
    }
}
