//--------------------------------------------------------------------------------------
// Purpose: Sets the cursor of the scene.
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
// CustomCursor object. Inheriting from MonoBehaviour.
//--------------------------------------------------------------------------------------
public class CustomCursor : MonoBehaviour
{
    // Public texture for the cursor visuals.
    [LabelOverride("Cursor")] [Tooltip("The cursor object to replace the default unity cursor.")]
    public Texture2D m_tCursor;

    //--------------------------------------------------------------------------------------
    // initialization
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        // Set the mouse click point.
        Vector2 v2CursorHotspot = new Vector2(m_tCursor.width / 2, m_tCursor.height / 2);

        // Set the cursor values.
        Cursor.SetCursor(m_tCursor, v2CursorHotspot, CursorMode.Auto);
    }
}