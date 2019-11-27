//--------------------------------------------------------------------------------------
// Purpose: Sets the cursor of the scene.
//
// Description: Script is used for easily setting the cursor of the current scene.
// Script takes in a public texture and set the hotpoint to center.
//
// Author: Thomas Wiltshire
//--------------------------------------------------------------------------------------

// Using, etc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------
// CustomCursor object. Inheriting from MonoBehaviour.
//--------------------------------------------------------------------------------------
public class CustomCursor : MonoBehaviour
{
    // Public texture for the cursor visuals.
    [LabelOverride("Cursor")] [Tooltip("The cursor object to replace the default unity cursor.")]
    public Texture2D m_tCursor;

    //
    public static CustomCursor m_gInstance;

    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // set instance
        m_gInstance = this;

        SetCustomCursor(m_tCursor);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetCustomCursor(Texture2D tCursor)
    {
        // Set the mouse click point.
        Vector2 v2CursorHotspot = new Vector2(tCursor.width / 2, tCursor.height / 2);

        // Set the cursor values.
        Cursor.SetCursor(tCursor, v2CursorHotspot, CursorMode.Auto);
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    public void SetDefaultCursor()
    {
        SetCustomCursor(m_tCursor);
    }
}