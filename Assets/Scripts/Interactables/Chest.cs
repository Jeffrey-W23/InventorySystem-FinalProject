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
// Shop object. Inheriting from Interactable.
//--------------------------------------------------------------------------------------
public class Chest : Interactable
{



    //
    private InventoryManager m_gInventoryManger;

    //
    public Inventory m_oInventory = new Inventory(6);





    //--------------------------------------------------------------------------------------
    // initialization.
    //--------------------------------------------------------------------------------------
    new void Awake()
    {
        // Run the base awake
        base.Awake();
    }







    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Start()
    {
        //
        m_gInventoryManger = InventoryManager.m_gInstance;
    }






    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // If the interaction button or exc is pressed.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // turn off interaction and set chest to closed
            m_bInteracted = false;
            m_bInteractable = false;
        }
    }






    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void OpenCloseInventory()
    {
        // Open the chest inventory
        if (!InventoryManager.m_gInstance.IsInventoryOpen())
            InventoryManager.m_gInstance.OpenContainer(new ChestContainer(m_oInventory, m_sPlayerObject.GetInventory(), 6));
    }








    //--------------------------------------------------------------------------------------
    // InteractedWith: override function from base class for what Interactable objects do 
    // once they have been interacted with.
    //--------------------------------------------------------------------------------------
    protected override void InteractedWith()
    {
        // Run the base interactedWith function.
        base.InteractedWith();

        // open the chest
        OpenCloseInventory();
    }
}