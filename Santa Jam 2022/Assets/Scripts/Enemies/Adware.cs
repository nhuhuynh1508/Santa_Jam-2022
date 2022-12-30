using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adware : Enemy
{
    public float rotateSpeed = 60f;

    // Update is called once per frame
    public Adware() : base(2f, 1f, 1f, 1f)
    {
        
    }

    public override void _Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public override void _Update()
    { 
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - rotateSpeed * Time.deltaTime);
    }
}
