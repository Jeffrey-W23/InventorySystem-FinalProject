﻿//--------------------------------------------------------------------------------------
// Purpose: Sets an object for the camera to follow.
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
// Follow object. Inheriting from MonoBehaviour.
//--------------------------------------------------------------------------------------
public class Follow : MonoBehaviour
{
    // PUBLIC VALUES //
    //--------------------------------------------------------------------------------------
    // Public Gameobject for the target the camera is to follow.
    [LabelOverride("Target")] [Tooltip("The object you want the camera to follow.")]
    public GameObject m_gTarget;
    //--------------------------------------------------------------------------------------

    // PRIVATE VALUES //
    //--------------------------------------------------------------------------------------
    // private vector3 for the offset for the camera
    private Vector3 m_v3Offset;
    //--------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // set the offset value.
        m_v3Offset = transform.position - m_gTarget.transform.position;
    }

    //--------------------------------------------------------------------------------------
    // Update: Function that calls each frame to update game objects.
    //--------------------------------------------------------------------------------------
    void Update()
    {
        // Calc the new x and y position of the camera
        float fNewXPosition = m_gTarget.transform.position.x - m_v3Offset.x;
        float fNewYPosition = m_gTarget.transform.position.y - m_v3Offset.y;

        // Update the postion of the camera.
        transform.position = new Vector3(fNewXPosition, fNewYPosition, transform.position.z);
    }
}