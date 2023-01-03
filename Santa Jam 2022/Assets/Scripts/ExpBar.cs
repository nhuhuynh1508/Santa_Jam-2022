/*
    ProgressBar.cs' derivation.
    Handle Player Experience progress bar logic.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : ProgressBar
{
    GameManager gameManager;

    private void Start() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        CalculateExp();
    }



    void CalculateExp()
    {
        int denominator;
        float numerator;
        int mileStone = gameManager.GetCurrentMilestone();
        
        if (mileStone == -1)
        {
            numerator = gameManager.playerExp;
            denominator = gameManager.expMilestone[0];
        }
        else
        {
            numerator = gameManager.playerExp - gameManager.expMilestone[mileStone];
            denominator = gameManager.expMilestone[mileStone + 1] - gameManager.expMilestone[mileStone];
        }

        _slider.value = numerator / denominator;
    }
}
