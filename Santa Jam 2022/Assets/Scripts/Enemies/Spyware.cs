using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyware : Enemy
{
    public GameObject spywarePrefab;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        speed = 2.0f;
        player = GameObject.FindGameObjectWithTag("RangePlayer");
    }


}

   
