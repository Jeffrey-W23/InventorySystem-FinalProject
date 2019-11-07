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
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    protected void Update()
    {
        // If the mouse is pressed.
        if (Input.GetMouseButtonDown(0))
        {
            // Allocate a bullet to the pool.
            GameObject gBullet = Allocate();

            // if a valid bullet and not null.
            if (gBullet)
            {
                // Update the postion, rotation and set direction of the bullet.
                gBullet.transform.position = transform.position;
                gBullet.transform.rotation = transform.rotation;
                gBullet.GetComponent<Bullet>().SetDirection(transform.right);
            }
        }
    }

    //--------------------------------------------------------------------------------------
    // Allocate: Allocate bullets to the pool.
    //
    // Return:
    //      GameObject: Current gameobject in the pool.
    //--------------------------------------------------------------------------------------
    GameObject Allocate()
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
}