using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyware : Enemy
{
    public float rotateSpeed;
    private Transform clockwise;
    private Transform counterClockwise;

    //CONSTRUCTOR
    public Spyware() : base(1.5f, 3f, 1.0f, 3f)
    {
        
    }

    public override void _Start()
    {
        clockwise = transform.GetChild(0).GetChild(0);
        clockwise.rotation = Quaternion.Euler(0, 0, clockwise.rotation.eulerAngles.z + Random.Range(0, 360));

        counterClockwise = transform.GetChild(0).GetChild(1);
        counterClockwise.rotation = Quaternion.Euler(0, 0, counterClockwise.rotation.eulerAngles.z + Random.Range(0, 360));
    }

    public override void _Update()
    {
        clockwise.rotation = Quaternion.Euler(0, 0, clockwise.rotation.eulerAngles.z - rotateSpeed * Time.deltaTime);
        counterClockwise.rotation = Quaternion.Euler(0, 0, counterClockwise.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime);
    }
}

   
