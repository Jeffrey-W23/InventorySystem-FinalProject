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
public class Tooltip : MonoBehaviour
{
    //
    public Text m_tText;

    //
    private Image m_iImage;

    //
    private bool m_bHovering;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        // get the image component
        m_iImage = GetComponent<Image>();

        // diabaled the image
        m_iImage.enabled = false;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Update()
    {
        // if the mouse is hovering over an item
        if (m_bHovering)
        {
            // set the tooltip to follow the mouse
            transform.position = Input.mousePosition;
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetTooltip(string strTitle)
    {
        // if the string length is more than 0
        if (strTitle.Length > 0)
        {
            // actiavate the tooltip
            m_bHovering = true;

            // set the text of the tooltip 
            m_tText.text = strTitle;

            // enabled the image
            m_iImage.enabled = true;
        }

        // else if the length is less than 0
        else
        {
            // deactivate the tooltip
            m_bHovering = false;
            m_tText.text = string.Empty;
            m_iImage.enabled = false;
        }
    }
}