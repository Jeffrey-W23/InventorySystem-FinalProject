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
    private Player m_gPlayer;

    //
    public List<GameObject> m_agSelectors = new List<GameObject>();

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    void Awake()
    {
        //
        m_gPlayer = FindObjectOfType<Player>();

        //
        for (int i = 0; i < m_agSelectors.Count; i++)
        {
            //
            m_agSelectors[i].GetComponent<Image>().enabled = false;
        }
    }

    //--------------------------------------------------------------------------------------
    // f
    //--------------------------------------------------------------------------------------
    void Update()
    {
        //
        if (m_gPlayer != null)
        {
            //
            for (int i = 0; i < m_agSelectors.Count; i++)
            {
                //
                if (i == m_gPlayer.GetSelectedHotbarIndex())
                {
                    //
                    m_agSelectors[m_gPlayer.GetSelectedHotbarIndex()].GetComponent<Image>().enabled = true;
                }

                //
                else if (i != m_gPlayer.GetSelectedHotbarIndex())
                {
                    m_agSelectors[i].GetComponent<Image>().enabled = false;
                }

                //
                else
                {
                    //
                    m_agSelectors[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
