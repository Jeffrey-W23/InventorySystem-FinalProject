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
using UnityEngine.EventSystems;

//--------------------------------------------------------------------------------------
// f
//--------------------------------------------------------------------------------------
public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //
    public Item m_oItem;

    //
    private Image m_iSprite;

    //
    private UIItem m_gSelectedItem;

    //
    private UIInventoryTooltip m_gTooltip;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        //
        m_iSprite = GetComponent<Image>();

        //
        UpdateItem(m_oItem);

        //
        m_gSelectedItem = GameObject.Find("Selected Item").GetComponent<UIItem>();

        //
        m_gTooltip = GameObject.Find("Tooltip").GetComponent<UIInventoryTooltip>();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void UpdateItem(Item oItem)
    {
        //
        m_oItem = oItem;

        //
        if (m_oItem != null)
        {
            //
            m_iSprite.color = Color.white;

            //
            m_iSprite.sprite = m_oItem.m_sIcon;
        }

        //
        else
        {
            //
            m_iSprite.color = Color.clear;
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerClick(PointerEventData eventData)
    {
        //
        if (m_oItem != null)
        {
            //
            if (m_gSelectedItem.m_oItem != null)
            {
                //
                Item oSelected = new Item(m_gSelectedItem.m_oItem);

                //
                m_gSelectedItem.UpdateItem(m_oItem);

                //
                UpdateItem(oSelected);
            }

            //
            else
            {
                //
                m_gSelectedItem.UpdateItem(m_oItem);

                //
                UpdateItem(null);
            }
        }

        //
        else if (m_gSelectedItem.m_oItem != null)
        {
            //
            UpdateItem(m_gSelectedItem.m_oItem);

            //
            m_gSelectedItem.UpdateItem(null);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        //
        if (m_oItem != null)
        {
            //
            m_gTooltip.GenerateTooltip(m_oItem);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        //
        m_gTooltip.gameObject.SetActive(false);
    }
}