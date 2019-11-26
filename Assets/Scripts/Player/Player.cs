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
    public Inventory m_oInventory = new Inventory(9);

    //
    private int m_nSelectedHotbarIndex = 0;

    //
    private int m_nPreviousSelectedHotbarIndex;

    //
    private KeyCode[] m_akHotbarControls = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };










    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Get the Rigidbody.
        m_rbRigidBody = GetComponent<Rigidbody2D>();

        // get the player arm object.
        m_gArm = transform.Find("Arm").gameObject;

        // set the current speed of the player to walk
        m_fCurrentSpeed = m_fWalkSpeed;

        // REMOVE // TEMP // REMOVE //
        // Set the parenting of pistol prefab.
        //m_gPistol = Instantiate(m_gWeaponPrefab);
        //m_gPistol.transform.parent = m_gArm.transform;
        // REMOVE // TEMP // REMOVE //
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

        // open player hotbar and make sure the inventory is not open
        InventoryManager.m_gInstance.OpenContainer(new PlayerHotbarContainer(null, m_oInventory, 3));
        InventoryManager.m_gInstance.ResetInventoryStatus();
    }








    //--------------------------------------------------------------------------------------
    // FixedUpdate: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // rotate player based on mouse postion.
        Rotate();

        // run the movement function to move player.
        Movement();      
    }

    //--------------------------------------------------------------------------------------
    // LateUpdate: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void LateUpdate()
    {
        // move arm with mouse movement
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
            if (!InventoryManager.m_gInstance.IsInventoryOpen())
            {
                // Open the player inventory
                InventoryManager.m_gInstance.OpenContainer(new PlayerContainer(null, m_oInventory, 6));

                //
                HideSelectedHotbar();
            }
        }

        //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //
            if (!InventoryManager.m_gInstance.IsInventoryOpen())
            {
                // close the player inventory and open hotbar
                InventoryManager.m_gInstance.OpenContainer(new PlayerHotbarContainer(null, m_oInventory, 3));

                //
                InventoryManager.m_gInstance.ResetInventoryStatus();

                //
                m_nSelectedHotbarIndex = m_nPreviousSelectedHotbarIndex;
            }
        }

        //
        MoveSelectedHotbarIndex(Input.GetAxis("Mouse ScrollWheel"));

        //
        for (int i = 0; i < m_akHotbarControls.Length; i++)
        {
            //
            if (Input.GetKeyDown(m_akHotbarControls[i]) && !InventoryManager.m_gInstance.IsInventoryOpen())
            {
                //
                m_nSelectedHotbarIndex = i;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    private void MoveSelectedHotbarIndex(float nDirection)
    {
        //
        if (!InventoryManager.m_gInstance.IsInventoryOpen())
        {
            //
            if (nDirection > 0)
            {
                //
                nDirection = 1;
            }

            //
            if (nDirection < 0)
            {
                //
                nDirection = -1;
            }

            //
            for (m_nSelectedHotbarIndex -= (int)nDirection; m_nSelectedHotbarIndex < 0; m_nSelectedHotbarIndex += 3) ;

            //
            while (m_nSelectedHotbarIndex >= 3)
            {
                //
                m_nSelectedHotbarIndex -= 3;
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void HideSelectedHotbar()
    {
        //
        m_nPreviousSelectedHotbarIndex = m_nSelectedHotbarIndex;

        //
        m_nSelectedHotbarIndex = 4;
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public int GetSelectedHotbarIndex()
    {
        //
        return m_nSelectedHotbarIndex;
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

            //
            HideSelectedHotbar();
        }
    }
}