using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyUtils : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region field
    //enemy1.cs
    public float meleeRange = 0.5f;
    public float meleey = 0.1f;

    //ideal
    public float idealDuration = 0.1f;

    //walk
    public float walkDuration = 5f;
    public float locateAfter = 0.01f;

    //attack
    public float attaccCooldown = 0.5f;

    #endregion


    #region method
    public void setDifficulty(int x)
    {
        switch(x)
        {
            case 1:
                meleeRange = 0.4f;
                meleey = 0.12f;
                idealDuration = 1f;
                walkDuration = 7f;
                attaccCooldown = 0.9f;
                locateAfter = 0.9f;
                break;

            case 2:
                meleeRange = 0.5f;
                meleey = 0.11f;
                idealDuration = 0.5f;
                walkDuration = 5f;
                attaccCooldown = 0.5f;
                locateAfter = 0.5f;
                break;

            case 3:
                meleeRange = 0.5f;
                meleey = 0.1f;
                idealDuration = 0.1f;
                walkDuration = 5f;
                attaccCooldown = 0.25f;
                locateAfter = 0f;
                break;

            default:
                break;
        }
    }
    #endregion



}
