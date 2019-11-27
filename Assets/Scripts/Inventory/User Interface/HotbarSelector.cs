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
public class HotbarSelector : MonoBehaviour
{
    //
    public List<GameObject> m_agSelectors = new List<GameObject>();

    //
    private InventoryManager m_gInventoryManger;

    //
    private Player m_gPlayer;

    //
    private KeyCode[] m_akInitHotbarControls = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, 
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    //
    private KeyCode[] m_akHotbarControls;

    //
    private int m_nCurrentlySelected = 0;

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // get the player game object
        m_gPlayer = FindObjectOfType<Player>();

        // loop through all selectors
        for (int i = 0; i < m_agSelectors.Count; i++)
        {
            // disable all of the selector images
            m_agSelectors[i].GetComponent<Image>().enabled = false;

            // init the keycode array
            m_akHotbarControls = new KeyCode[m_agSelectors.Count];
        }

        // loop through all selectors
        for (int i = 0; i < m_agSelectors.Count; i++)
        {
            // set the keycode array to the amount of slots in the hotbar
            m_akHotbarControls[i] = m_akInitHotbarControls[i];
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Start()
    {
        // get the inventory manager instance
        m_gInventoryManger = InventoryManager.m_gInstance;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // Update the currently selected item slot
        UpdateCurrentlySelected(Input.GetAxis("Mouse ScrollWheel"));
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void UpdateCurrentlySelected (float nDirection)
    {
        // loop through each button in the hotbar
        for (int i = 0; i < m_akHotbarControls.Length; i++)
        {
            // check if a key has been pressed on the hotbar
            if (Input.GetKeyDown(m_akHotbarControls[i]) && !m_gInventoryManger.IsInventoryOpen())
            {
                // assign currently selected to the key pressed
                m_nCurrentlySelected = i;
            }
        }

        // check if an inventory is currently open
        if (!m_gInventoryManger.IsInventoryOpen())
        {
            // check direction of the scrool wheel
            if (nDirection > 0)
                nDirection = 1;
            if (nDirection < 0)
                nDirection = -1;

            // move the selector
            for (m_nCurrentlySelected -= (int)nDirection; m_nCurrentlySelected < 0; m_nCurrentlySelected += m_agSelectors.Count) ;

            // make sure the index stay within horbar slots
            while (m_nCurrentlySelected >= m_agSelectors.Count)
            {
                // keep the currently selected at or under the current amount of itemslots
                m_nCurrentlySelected -= m_agSelectors.Count;
            }
        }

        // loop through all the selectors
        for (int i = 0; i < m_agSelectors.Count; i++)
        {
            // if the slot is currently selected
            if (i == m_nCurrentlySelected && !m_gInventoryManger.IsInventoryOpen())
            {
                // set the image of the selector to true
                m_agSelectors[m_nCurrentlySelected].GetComponent<Image>().enabled = true;

                // set the curently selected slot
                m_nCurrentlySelected = i;
            }

            // else if the slot is not currently selected
            else if (i != m_nCurrentlySelected)
            {
                // set the image of the selector to false
                m_agSelectors[i].GetComponent<Image>().enabled = false;
            }

            // else for whatever reason
            else
            {
                // set the image of the selector to false
                m_agSelectors[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public int GetCurrentlySelected()
    {
        // return the currently selected index
        return m_nCurrentlySelected;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetCurrentlySelected(int nIndex)
    {
        // set the currently selected index
        m_nCurrentlySelected = nIndex;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public ItemStack GetCurrentlySelectedItemStack()
    {
        // return the currently selected item
        return m_gPlayer.GetInventory().GetStackInSlot(m_nCurrentlySelected);
    }
}