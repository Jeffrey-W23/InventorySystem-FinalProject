//--------------------------------------------------------------------------------------
// Purpose: 
//
// Description: 
//
// Author: Thomas Wiltshire
//--------------------------------------------------------------------------------------

// Using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// Gun object. Inheriting from MonoBehaviour.
//--------------------------------------------------------------------------------------
public class Gun : MonoBehaviour
{
    // BULLET //
    //--------------------------------------------------------------------------------------
    // Title for this section of public values.
    [Header("Bullet:")]

    // Public gameobject for bullet object.
    [LabelOverride("Bullet Object")] [Tooltip("The bullet that this gun will fire.")]
    public GameObject m_gBulletBlueprint;

    // public int for pool size.
    [LabelOverride("Pool Size")] [Tooltip("How many bullets allowed on screen at one time.")]
    public int m_nPoolSize;

    // Leave a space in the inspector.
    [Space]
    //--------------------------------------------------------------------------------------







    // Public texture for the cursor visuals.
    [LabelOverride("Cursor")] [Tooltip("The cursor object to replace the default unity cursor.")]
    public Texture2D m_tCursor;

    //
    public GameObject m_gBulletSpawn;

    //
    public Item m_oAmmo;

    //
    private int m_nCurrentAmmo = 0;

    //
    public int m_nAmmoUsage = 1;

    //
    private int m_nCurrentAmmoStack = -1;

    //
    private InventoryManager m_gInventoryManger;









    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // An Array of GameObjects for bullets.
    protected GameObject[] m_gBulletList;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    protected void Awake()
    {
        // initialize bullet list with size.
        m_gBulletList = new GameObject[m_nPoolSize];

        // Go through each bullet.
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // Instantiate and set active state.
            m_gBulletList[i] = Instantiate(m_gBulletBlueprint);
            m_gBulletList[i].SetActive(false);
        }
    }

    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    protected void Start()
    {
        // get the inventory manager instance
        m_gInventoryManger = InventoryManager.m_gInstance;

        // set the ammo for the gun
        CheckCurrentAmmo();
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    protected void Update()
    {
        // set the current ammo
        CheckCurrentAmmo();

        //
        if (!m_gInventoryManger.IsInventoryOpen())
        {
            // If the mouse is pressed.
            if (Input.GetMouseButtonDown(0) && m_nCurrentAmmo >= 1)
            {
                // Allocate a bullet to the pool.
                GameObject gBullet = Allocate();

                // if a valid bullet and not null.
                if (gBullet)
                {
                    // Update the postion, rotation and set direction of the bullet.
                    gBullet.transform.position = m_gBulletSpawn.transform.position;
                    gBullet.transform.rotation = m_gBulletSpawn.transform.rotation;
                    gBullet.GetComponent<Bullet>().SetDirection(transform.right);

                    // update the current ammo of the gun
                    UpdateCurrentAmmo(m_nAmmoUsage);
                }
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // Allocate: Allocate bullets to the pool.
    //
    // Return:
    //      GameObject: Current gameobject in the pool.
    //--------------------------------------------------------------------------------------
    private GameObject Allocate()
    {
        // For each in the pool.
        for (int i = 0; i < m_nPoolSize; ++i)
        {
            // Check if active.
            if (!m_gBulletList[i].activeInHierarchy)
            {
                // Set active state.
                m_gBulletList[i].SetActive(true);

                // return the bullet.
                return m_gBulletList[i];
            }
        }

        // if all fail return null.
        return null;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void CheckCurrentAmmo()
    {
        // get the player inventory
        Inventory oPlayerInventory = m_gInventoryManger.GetPlayerInventory();

        // bool to check if the player has ammo
        bool bHasAmmo = false;

        // loop through the player inventory
        for (int i = 0; i < oPlayerInventory.GetInventory().Count; i++)
        {
            // check the current indexs item is null
            if (!oPlayerInventory.GetStackInSlot(i).IsStackEmpty() && oPlayerInventory.GetStackInSlot(i).GetItem() != null)
            {
                // if the item at the current index is the ammo item
                if (oPlayerInventory.GetStackInSlot(i).GetItem().m_strTitle == m_oAmmo.m_strTitle)
                {
                    // Set the current ammo to stack to the id of the slot
                    m_nCurrentAmmoStack = oPlayerInventory.GetStackInSlot(i).m_nSlotId;

                    // set the current ammo to the amount in the stack
                    m_nCurrentAmmo = (oPlayerInventory.GetStackInSlot(i).GetItemCount());

                    // the player has ammo in the inventory
                    bHasAmmo = true;

                    // break the loop, ammo stack has been found
                    break;
                }
            }

            // the player has no ammo in the inventory
            bHasAmmo = false;
        }

        // if there is no ammo in the inventory
        if (!bHasAmmo)
        {
            // ammo is 0 and there is no current ammo stack
            m_nCurrentAmmo = 0;
            m_nCurrentAmmoStack = -1;
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void UpdateCurrentAmmo(int nAmount)
    {
        // get the player inventory
        Inventory oPlayerInventory = m_gInventoryManger.GetPlayerInventory();

        // loop through the player inventory
        for (int i = 0; i < oPlayerInventory.GetInventory().Count; i++)
        {
            // check the current indexs item is null
            if (!oPlayerInventory.GetStackInSlot(i).IsStackEmpty() && oPlayerInventory.GetStackInSlot(i).GetItem() != null)
                {
                // if the item at the current index is the ammo item
                if (oPlayerInventory.GetStackInSlot(i).GetItem().m_strTitle == m_oAmmo.m_strTitle)
                {
                    // if the slot id matches the current ammo stack
                    if (oPlayerInventory.GetStackInSlot(i).m_nSlotId == m_nCurrentAmmoStack)
                    {
                        // update the ammo in the player inventory
                        oPlayerInventory.GetStackInSlot(i).DecreaseStack(nAmount);

                        // set the current ammo to the amount in the stack
                        m_nCurrentAmmo = (oPlayerInventory.GetStackInSlot(i).GetItemCount());
                    }
                }
            }
        }

        // Update the hotbar UI
        m_gInventoryManger.GetCurrentlyOpenContainer().UpdateSlots();
    }   
}