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
// Player object. Inheriting from MonoBehaviour.
//--------------------------------------------------------------------------------------
public class Player : MonoBehaviour
{
    // MOVEMENT //
    //--------------------------------------------------------------------------------------
    // Title for this section of public values.
    [Header("Movement:")]

    // public float value for the walking speed.
    [LabelOverride("Walking Speed")] [Tooltip("The speed of which the player will walk in float value.")]
    public float m_fWalkSpeed = 5.0f;

    // public float value for the walking speed.
    [LabelOverride("Running Speed")] [Tooltip("The speed of which the player will run in float value.")]
    public float m_fRunSpeed = 7.0f;

    // public float value for max exhaust level.
    [LabelOverride("Running Exhaust")] [Tooltip("The max level of exhaustion the player can handle before running is false.")]
    public float m_fRunExhaust = 3.0f;

    // Leave a space in the inspector.
    [Space]
    //--------------------------------------------------------------------------------------

    // DEBUG //
    //--------------------------------------------------------------------------------------
    // Title for this section of public values.
    [Header("Debug:")]

    // public bool for turning the debug info off and on.
    [LabelOverride("Display Debug Info?")] [Tooltip("Turns off and on debug information in the unity console.")]
    public bool m_bDebugMode = true;

    // Leave a space in the inspector.
    [Space]
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private rigidbody.
    private Rigidbody2D m_rbRigidBody;

    // private float for the current speed of the player.
    private float m_fCurrentSpeed;

    // private flkoat for the current exhaust level of the player.
    private float m_fRunCurrentExhaust = 0.0f;

    // private bool for if ther player can run or not
    private bool m_bExhausted = false;

    // private gameobject for the players arm object
    private GameObject m_gArm;

    // private float value for distance between mouse and arm
    private float m_fArmDistanceFromMouse;
    //--------------------------------------------------------------------------------------

    // DELEGATES //
    //--------------------------------------------------------------------------------------
    // Create a new Delegate for handling the interaction functions.
    public delegate void InteractionEventHandler();

    // Create an event for the delegate for extra protection. 
    public InteractionEventHandler InteractionCallback;
    //--------------------------------------------------------------------------------------

    // REMOVE // TEMP // REMOVE //
    // Weapon prefab.
    public GameObject m_gWeaponPrefab;

    // The Pistol weapon.
    private GameObject m_gPistol;
    // REMOVE // TEMP // REMOVE //









    public Item[] itemsToAdd;

    //
    private Inventory m_oInventory = new Inventory(9);

    //
    private InventoryManager m_gInventoryManger;

    //
    private bool m_bFreezePlayer = false;

    //
    private HotbarSelector m_gHotbarSelector;








    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        //
        m_gHotbarSelector = FindObjectOfType<HotbarSelector>();

        // Get the Rigidbody.
        m_rbRigidBody = GetComponent<Rigidbody2D>();

        // get the player arm object.
        m_gArm = transform.Find("Arm").gameObject;

        // set the current speed of the player to walk
        m_fCurrentSpeed = m_fWalkSpeed;

        // REMOVE // TEMP // REMOVE // POSSIBLTY //
        // Set the parenting of pistol prefab.
        m_gPistol = Instantiate(m_gWeaponPrefab);
        m_gPistol.transform.parent = m_gArm.transform;
        m_gPistol.SetActive(false);
        // REMOVE // TEMP // REMOVE // POSSIBLTY //
    }










    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Start()
    {
        // for each item in items to add
        foreach (Item i in itemsToAdd)
        {
            // add an item to the inventory
            m_oInventory.AddItem(new ItemStack(i, 1));
        }

        // get the inventory manager instance
        m_gInventoryManger = InventoryManager.m_gInstance;

        // open player hotbar and make sure the inventory is not open
        m_gInventoryManger.OpenContainer(new PlayerHotbarContainer(null, m_oInventory, 3));
        m_gInventoryManger.ResetInventoryStatus();
    }








    //--------------------------------------------------------------------------------------
    // FixedUpdate: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // is player allowed to move
        if (!m_bFreezePlayer)
        {
            // rotate player based on mouse postion.
            Rotate();

            // run the movement function to move player.
            Movement();
        } 
    }

    //--------------------------------------------------------------------------------------
    // LateUpdate: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void LateUpdate()
    {
        // move arm with mouse movement if the player is allowed to move
        if (!m_bFreezePlayer)
            MoveArm();
    }







    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void Update()
    {
        // Run the interaction function
        Interaction();

        //
        OpenCloseInventory();




        //
        if (m_gHotbarSelector.GetCurrentlySelectedItemStack().GetItem() != null)
        {
            //
            if (m_gHotbarSelector.GetCurrentlySelectedItemStack().GetItem().m_strTitle == "Pistol")
            {

                //
                m_gPistol.SetActive(true);

                //
                //CustomCursor.m_gInstance.SetCustomCursor(m_gPistol.GetComponent<Pistol>().m_tCursor);
            }

            else
            {
                //
                m_gPistol.SetActive(false);

                //
                //CustomCursor.m_gInstance.SetDefaultCursor();
            }
        }

        //
        else
        {
            //
            m_gPistol.SetActive(false);

            //
            CustomCursor.m_gInstance.SetDefaultCursor();
        }

        
    }







    //--------------------------------------------------------------------------------------
    // Movement:
    //--------------------------------------------------------------------------------------
    void Movement()
    {
        // Get the Horizontal and Vertical axis.
        Vector3 v2MovementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f).normalized;

        // Move the player
        m_rbRigidBody.MovePosition(transform.position + v2MovementDirection * m_fCurrentSpeed * Time.fixedDeltaTime);

        // if the players holds down left shift
        if (Input.GetKey(KeyCode.LeftShift) && !m_bExhausted)
        {
            // current player speed equals run speed
            m_fCurrentSpeed = m_fRunSpeed;

            // tick the current exhaust level up 
            m_fRunCurrentExhaust += Time.deltaTime;

            // if the current exhaust is above the max
            if (m_fRunCurrentExhaust > m_fRunExhaust)
            {
                // player is exhausted and speed is now walking.
                m_bExhausted = true;
                m_fCurrentSpeed = m_fWalkSpeed;
            }
        }

        // else if shift isnt down
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            // current speed is walking speed.
            m_fCurrentSpeed = m_fWalkSpeed;

            // tick the current exhaust level down 
            m_fRunCurrentExhaust -= Time.deltaTime;

            // if the current exhaust is below 0 keep at 0
            if (m_fRunCurrentExhaust < 0.0f)
                m_fRunCurrentExhaust = 0.0f;

            // if the current exhaust is below the max then exhausted false
            if (m_fRunCurrentExhaust < m_fRunExhaust)
                m_bExhausted = false;
        }
    }

    //--------------------------------------------------------------------------------------
    // Roatate: Rotate the player from the mouse movement.
    //--------------------------------------------------------------------------------------
    void Rotate()
    {
        // Get mouse inside camera 
        Vector3 v3Position = Camera.main.WorldToScreenPoint(transform.position);

        // Get the  mouse direction.
        Vector3 v3Direction = Input.mousePosition - v3Position;

        // Work out the angle.
        float fAngle = Mathf.Atan2(v3Direction.y, v3Direction.x) * Mathf.Rad2Deg;

        // Update the rotation.
        transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
    }

    //--------------------------------------------------------------------------------------
    // MoveArm:
    //--------------------------------------------------------------------------------------
    void MoveArm()
    {
        // Get mouse pos inside camera
        Vector3 v3Position = Camera.main.WorldToScreenPoint(m_gArm.transform.position);

        // update the distance arm has from mouse.
        m_fArmDistanceFromMouse = Vector3.Distance(v3Position, Input.mousePosition);

        // Check the distance between the mouse and arm.
        // if far enough away turn the mouse towards mouse.
        // else stop arm rotation.
        if (m_fArmDistanceFromMouse > 100)
        {
            // Get the  mouse direction.
            Vector3 v3Dir = Input.mousePosition - v3Position;

            // Work out the angle.
            float fAngle = Mathf.Atan2(v3Dir.y, v3Dir.x) * Mathf.Rad2Deg;

            // Update the rotation of the arm.
            m_gArm.transform.rotation = Quaternion.AngleAxis(fAngle, Vector3.forward);
        }
    }














    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void OpenCloseInventory()
    {
        // if the i key is down
        if (Input.GetKeyDown(KeyCode.I))
        {
            // if the inventory bool is false
            if (!m_gInventoryManger.IsInventoryOpen())
            {
                // Open the player inventory
                m_gInventoryManger.OpenContainer(new PlayerContainer(null, m_oInventory, 6));
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public bool GetFreezePlayer()
    {
        // get the player freeze bool
        return m_bFreezePlayer;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetFreezePlayer(bool bFreeze)
    {
        // set the player freeze bool
        m_bFreezePlayer = bFreeze;

        // make sure the player is forzen further by constricting ridgidbody
        if (bFreeze)
            m_rbRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        else if (!bFreeze)
            m_rbRigidBody.constraints = RigidbodyConstraints2D.None;

    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public Inventory GetInventory()
    {
        // return the player inventory
        return m_oInventory;
    }







    //--------------------------------------------------------------------------------------
    // Interaction: Function interacts on button press with interactables objects.
    //--------------------------------------------------------------------------------------
    public void Interaction()
    {
        // If the interaction button is pressed.
        if (Input.GetKeyUp(KeyCode.E) && InteractionCallback != null)
        {
            // Run interaction delegate.
            InteractionCallback();
        }
    }
}