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
using UnityEngine.EventSystems;
using UnityEngine.UI;

//--------------------------------------------------------------------------------------
// f
//--------------------------------------------------------------------------------------
public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    //
    public Image m_iIcon;

    //
    public Text m_tCount;

    //
    private int m_nId;

    //
    private ItemStack m_oCurrentStack;

    //
    private Container m_oCurrentContainer;

    //
    private InventoryManager m_gInventoryManger;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetSlot(Inventory oInventory, int nId, Container oContainer)
    {
        // set the id and container
        m_nId = nId;
        m_oCurrentContainer = oContainer;

        // set the current stack to the stack in the inventory
        m_oCurrentStack = oInventory.GetStackInSlot(nId);

        // get the inventory manager instance
        m_gInventoryManger = InventoryManager.m_gInstance;

        // update the slot
        UpdateSlot();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void SetSlotContent(ItemStack oStack)
    {
        // set the current stack to passed in stack
        m_oCurrentStack.SetStack(oStack);

        // update the slot
        UpdateSlot();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void SetTooltip(string strTitle)
    {
        // activate passed in tooltip
        m_gInventoryManger.ActivateToolTip(strTitle);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void UpdateSlot()
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
            // set the icon to false
            m_iIcon.enabled = false;

            // set the count text to string empty
            m_tCount.text = string.Empty;
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        // get the currently selected stack
        ItemStack oCurrentSelectedStack = m_gInventoryManger.GetSelectedStack();

        // make a copy of the current stack
        ItemStack oCurrentStackCopy = m_oCurrentStack.CopyStack();

        // if the mouse is left click
        if (eventData.pointerId == -1)
        {
            // run the left click method
            OnLeftClick(oCurrentSelectedStack, oCurrentStackCopy);
        }

        // if the mouse is right click
        if (eventData.pointerId == -2)
        {
            // run the right click method
            OnRightClick(oCurrentSelectedStack, oCurrentStackCopy);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        // get the currently selected item
        ItemStack oCurrentSelectedStack = m_gInventoryManger.GetSelectedStack();

        // if the current stack isnt empty and an item isnt selected
        if (!m_oCurrentStack.IsStackEmpty() && oCurrentSelectedStack.IsStackEmpty())
        {
            // activate the tooltip with the current stack information
            SetTooltip(m_oCurrentStack.GetItem().m_strTitle);
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        // deactivated the tooltip
        SetTooltip(string.Empty);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void OnLeftClick(ItemStack oCurrentSelectedStack, ItemStack oCurrentStackCopy)
    {
        // if the current stack is not empty and the selected stack is empty
        if (!m_oCurrentStack.IsStackEmpty() && oCurrentSelectedStack.IsStackEmpty())
        {
            // set the selected stack to the current stack copy
            m_gInventoryManger.SetSelectedStack(oCurrentStackCopy);

            // Set the slot content to empty
            SetSlotContent(ItemStack.m_oEmpty);

            // deactivate the tooltip
            SetTooltip(string.Empty);
        }

        // if the current stack is empty and the selected stack is not empty
        if (m_oCurrentStack.IsStackEmpty() && !oCurrentSelectedStack.IsStackEmpty())
        {
            // Set the slot content to the current selected stack
            SetSlotContent(oCurrentSelectedStack);

            // set the current selected stack to empty
            m_gInventoryManger.SetSelectedStack(ItemStack.m_oEmpty);

            // activate the tooltip
            SetTooltip(m_oCurrentStack.GetItem().m_strTitle);
        }

        // if both current stack and selected stack are empty
        if (!m_oCurrentStack.IsStackEmpty() && !oCurrentSelectedStack.IsStackEmpty())
        {
            // are the stacks equal
            if (ItemStack.AreItemsEqual(oCurrentStackCopy, oCurrentSelectedStack))
            {
                // Can you add to the stack copy
                if (oCurrentStackCopy.IsItemAddable(oCurrentSelectedStack.GetItemCount()))
                {
                    // increase the current stack copy count by the selected stack count
                    oCurrentStackCopy.IncreaseStack(oCurrentSelectedStack.GetItemCount());

                    // set the slot conent to the current stack copy
                    SetSlotContent(oCurrentStackCopy);

                    // set the currently selected stack to empty
                    m_gInventoryManger.SetSelectedStack(ItemStack.m_oEmpty);

                    // activate the tooltip
                    SetTooltip(m_oCurrentStack.GetItem().m_strTitle);
                }

                // else if the item is not addable
                else
                {
                    // new int var, get the difference between current copied stak and currently selected stack
                    int nDifference = (oCurrentStackCopy.GetItemCount() + oCurrentSelectedStack.GetItemCount()) - oCurrentStackCopy.GetItem().m_nMaxStackSize;

                    // set the current copy stack count to the max stack size of the stacks item
                    oCurrentStackCopy.SetItemCount(m_oCurrentStack.GetItem().m_nMaxStackSize);

                    // get a copy of the currently selected stack
                    ItemStack oCurrentSelectedStackCopy = oCurrentSelectedStack.CopyStack();

                    // set the count of the copied current selected stack to the differnce
                    oCurrentSelectedStackCopy.SetItemCount(nDifference);

                    // set the content of the stack to the current stack copy
                    SetSlotContent(oCurrentStackCopy);

                    // set the currently selected stack to the current selected stack copy
                    m_gInventoryManger.SetSelectedStack(oCurrentSelectedStackCopy);

                    // deactivate the tooltip
                    SetTooltip(string.Empty);
                }
            }

            // if the stacks arent equal
            else
            {
                // set the slot content to the currently selected stack
                SetSlotContent(oCurrentSelectedStack);

                // set the currently selected stack to the current selected stack copy
                m_gInventoryManger.SetSelectedStack(oCurrentStackCopy);

                // deactivate the tooltip
                SetTooltip(string.Empty);
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void OnRightClick(ItemStack oCurrentSelectedStack, ItemStack oCurrentStackCopy)
    {
        //
        if (!m_oCurrentStack.IsStackEmpty() && oCurrentSelectedStack.IsStackEmpty())
        {
            //
            ItemStack oSplitStack = oCurrentStackCopy.SplitStack(oCurrentStackCopy.GetItemCount() / 2);

            //
            m_gInventoryManger.SetSelectedStack(oSplitStack);

            //
            SetSlotContent(oCurrentStackCopy);

            // deactivate the tooltip
            SetTooltip(string.Empty);
        }

        //
        if (m_oCurrentStack.IsStackEmpty() && !oCurrentSelectedStack.IsStackEmpty())
        {
            //
            SetSlotContent(new ItemStack(oCurrentSelectedStack.GetItem(), 1));

            // get a copy of the currently selected stack
            ItemStack oCurrentSelectedStackCopy = oCurrentSelectedStack.CopyStack();

            //
            oCurrentSelectedStackCopy.DecreaseStack(1);

            //
            m_gInventoryManger.SetSelectedStack(oCurrentSelectedStackCopy);

            // deactivate the tooltip
            SetTooltip(string.Empty);
        }

        //
        if (!m_oCurrentStack.IsStackEmpty() && !oCurrentSelectedStack.IsStackEmpty())
        {
            //
            if (ItemStack.AreItemsEqual(oCurrentStackCopy, oCurrentSelectedStack))
            {
                //
                if (m_oCurrentStack.IsItemAddable(1))
                {
                    //
                    oCurrentStackCopy.IncreaseStack(1);

                    //
                    SetSlotContent(oCurrentStackCopy);

                    // get a copy of the currently selected stack
                    ItemStack oCurrentSelectedStackCopy = oCurrentSelectedStack.CopyStack();

                    //
                    oCurrentSelectedStackCopy.DecreaseStack(1);

                    //
                    m_gInventoryManger.SetSelectedStack(oCurrentSelectedStackCopy);

                    // deactivate the tooltip
                    SetTooltip(string.Empty);
                }
            }
        }
    }
}
