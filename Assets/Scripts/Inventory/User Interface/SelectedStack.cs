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
public class SelectedStack : MonoBehaviour
{
    //
    public Image m_iIcon;

    //
    public Text m_tCount;

    //
    private ItemStack m_oCurrentStack = ItemStack.m_oEmpty;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Update()
    {
        // update the selected stack
        UpdateSelectedStack();

        // set the postion to follow the mosue
        transform.position = Input.mousePosition;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetSelectedStack(ItemStack oStack)
    {
        // set the current stack to the selected stack
        m_oCurrentStack = oStack;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void UpdateSelectedStack()
    {
        // check if the current stack is empty
        if (!m_oCurrentStack.IsStackEmpty())
        {
            // enabled the slot icon
            m_iIcon.enabled = true;
            m_iIcon.sprite = m_oCurrentStack.GetItem().m_sIcon;

            // make sure the slot icon is the same height and wisth of the item icon
            m_iIcon.GetComponent<RectTransform>().sizeDelta = new Vector2
                (m_oCurrentStack.GetItem().m_sIcon.rect.width, m_oCurrentStack.GetItem().m_sIcon.rect.height);

            // if the current stack item count is greater than 1
            if (m_oCurrentStack.GetItemCount() > 1)
            {
                // set the count text to the current stacks count
                m_tCount.text = m_oCurrentStack.GetItemCount().ToString();
            }

            // else if the current stack wasnt greater than 1
            else
            {
                // set the count text to string empty
                m_tCount.text = string.Empty;
            }
        }

        // else if the current stack is empty
        else
        {
            // diabled the selected stack
            DisableSelectedStack();
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void DisableSelectedStack()
    {
        // set the icon to false
        m_iIcon.enabled = false;

        // set the count text to string empty
        m_tCount.text = string.Empty;
    }
}