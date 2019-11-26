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
public class Inventory
{
    //
    private List<ItemStack> m_aoItems = new List<ItemStack>();

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Inventory(int nSize)
    {
        // loop through the inventory size
        for (int i = 0; i < nSize; i++)
        {
            // add an item stack for every item
            m_aoItems.Add(new ItemStack(i));
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool AddItem(ItemStack oStack)
    {
        // loop through each item stack in the inventory
        foreach (ItemStack i in m_aoItems)
        {
            // is the stack empty
            if (i.IsStackEmpty())
            {
                // set the stack to passed in stack
                i.SetStack(oStack);

                // return true, item added
                return true;
            }

            // is the stack equal to passed in stack
            if (ItemStack.AreItemsEqual(oStack, i))
            {
                // is it possible to add items to this stack
                if (i.IsItemAddable(oStack.GetItemCount()))
                {
                    // increase the stack count
                    i.IncreaseStack(oStack.GetItemCount());

                    // return true, item added
                    return true;
                }

                // else if the item is not addable
                else
                {
                    // new int var, get the difference between passed in stack and current stack
                    int nDifference = (i.GetItemCount() + oStack.GetItemCount()) - i.GetItem().m_nMaxStackSize;

                    // set the count of the stack to the max stack size of the stacks item
                    i.SetItemCount(i.GetItem().m_nMaxStackSize);

                    // set the count of the passed in stack to the stack differnce
                    oStack.SetItemCount(nDifference);
                }
            }
        }

        // return false, item not added.
        return false;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack GetStackInSlot(int nIndex)
    {
        // return stack from index
        return m_aoItems[nIndex];
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public List<ItemStack> GetInventory()
    {
        // return the inventory
        return m_aoItems;
    }
}
