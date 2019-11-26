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
public class ItemStack
{
    //
    public static ItemStack m_oEmpty = new ItemStack();

    //
    public Item m_oItem;

    //
    public int m_nItemCount;

    //
    public int m_nSlotId;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack()
    {
        // set default values
        m_oItem = null;
        m_nItemCount = 0;
        m_nSlotId = -1;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack(int nSlotId)
    {
        // set default values
        m_oItem = null;
        m_nItemCount = 0;
        m_nSlotId = nSlotId;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack(Item oItem, int nItemCount)
    {
        // set default values
        m_oItem = oItem;
        m_nItemCount = nItemCount;
        m_nSlotId = -1;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack(Item oItem, int nItemCount, int nSlotId)
    {
        // set default values
        m_oItem = oItem;
        m_nItemCount = nItemCount;
        m_nSlotId = nSlotId;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Item GetItem()
    {
        // return the item
        return m_oItem;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public int GetItemCount()
    {
        // return the item count
        return m_nItemCount;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetItemCount(int nAmount)
    {
        // set the count of the item
        m_nItemCount = nAmount;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetStack(ItemStack oStack)
    {
        // set the item to the passed in stacks item
        m_oItem = oStack.GetItem();

        // set the count to the passed in stacks count
        m_nItemCount = oStack.GetItemCount();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack SplitStack(int nAmount)
    {
        // get the min between amount and count
        int i = Mathf.Min(nAmount, m_nItemCount);

        // make a copy of the current stack
        ItemStack oStackCopy = CopyStack();

        // set the copied stack count to result of min and decrease current stack count
        oStackCopy.SetItemCount(i);
        DecreaseStack(i);

        // return copied stack
        return oStackCopy;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack CopyStack()
    {
        //return a copy of the stack
        return new ItemStack(m_oItem, m_nItemCount, m_nSlotId);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool IsStackEmpty()
    {
        // return if the stack is empty or not
        return m_nItemCount < 1;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool IsItemEqual(ItemStack oStack)
    {
        // Check if an item is equal
        return !oStack.IsStackEmpty() && m_oItem == oStack.GetItem();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public static bool AreItemsEqual(ItemStack oStack1, ItemStack oStack2)
    {
        // check if items are equal in different stacks.
        return oStack1 == oStack2 ? true : (!oStack1.IsStackEmpty() && !oStack2.IsStackEmpty()
            ? oStack1.IsItemEqual(oStack2) : false);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool IsItemAddable(int nAmount)
    {
        // check if the stack has room for another item
        return (m_nItemCount + nAmount) <= m_oItem.m_nMaxStackSize;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void IncreaseStack(int nAmount)
    {
        // increase the count of the stack
        m_nItemCount += nAmount;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void DecreaseStack(int nAmount)
    {
        // drecrease the count of the stack
        m_nItemCount -= nAmount;
    }
}