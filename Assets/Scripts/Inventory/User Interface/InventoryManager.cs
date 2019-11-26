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
public class InventoryManager : MonoBehaviour
{
    //
    public static InventoryManager m_gInstance;

    //
    public GameObject m_gSlotPrefab;

    //
    public List<ContainerGetter> m_aoContainers = new List<ContainerGetter>();
    
    //
    private Container m_oCurrentOpenContainer;

    //
    private ItemStack m_oCurrentSelectedStack = ItemStack.m_oEmpty;

    //
    private GameObject m_gSelectedStackObject;

    //
    private SelectedStack m_gSelectedStack;

    //
    private Tooltip m_gToolTip;

    //
    private bool m_bIsInventoryOpen = false;

    //
    private Player m_gPlayer;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        // set instance
        m_gInstance = this;

        // get the selected stack component
        m_gSelectedStack = GetComponentInChildren<SelectedStack>();

        // get the tooltip component
        m_gToolTip = GetComponentInChildren<Tooltip>();

        // get the player object
        m_gPlayer = FindObjectOfType<Player>();
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Update()
    {
        // if the inventory is currently opened
        if (m_bIsInventoryOpen)
        {
            // if the escape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // set the inventory opened to false
                m_bIsInventoryOpen = false;

                //
                m_gPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public GameObject GetContainerPrefab(string strTitle)
    {
        // for each container in containers
        foreach (ContainerGetter i in m_aoContainers)
        {
            // Do the container titles match
            if (i.m_strTitle == strTitle)
            {
                // return prefab
                return i.m_gPrefab;
            }
        }

        // return null
        return null;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void OpenContainer(Container nContainer)
    {
        // if the current open container is not null
        if (m_oCurrentOpenContainer != null)
        {
            // close the current open container
            m_oCurrentOpenContainer.Close();
        }

        // Set the current open container to passed in container
        m_oCurrentOpenContainer = nContainer;

        // set the inventory opened to true
        m_bIsInventoryOpen = true;

        //
        m_gPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void CloseContainer()
    {
        // if the current open container is not null
        if (m_oCurrentOpenContainer != null)
        {
            // close the current open container
            m_oCurrentOpenContainer.Close();
        }

        // set the inventory opened to false
        m_bIsInventoryOpen = false;

        //
        m_gPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool IsInventoryOpen()
    {
        // return if the inventory is open
        return m_bIsInventoryOpen;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void ResetInventoryStatus()
    {
        // reset the inventory open status back to false
        m_bIsInventoryOpen = false;

        //
        m_gPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack GetSelectedStack()
    {
        // return the currently selected stack
        return m_oCurrentSelectedStack;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetSelectedStack(ItemStack oStack)
    {
        // set the currently selected stack
        m_gSelectedStack.SetSelectedStack(m_oCurrentSelectedStack = oStack);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void ActivateToolTip(string strTitle)
    {
        // set the tooltip
        m_gToolTip.SetTooltip(strTitle);
    }
}

//--------------------------------------------------------------------------------------
// f
//--------------------------------------------------------------------------------------
[System.Serializable]
public class ContainerGetter
{
    //
    public string m_strTitle;

    //
    public GameObject m_gPrefab;
}