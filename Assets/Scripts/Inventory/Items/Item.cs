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
[CreateAssetMenu(fileName ="New Item")]
public class Item : ScriptableObject
{
    //
    public string m_strTitle;

    //
    public Sprite m_sIcon;

    //
    [Range(1, 20)]
    public int m_nMaxStackSize = 20;
}