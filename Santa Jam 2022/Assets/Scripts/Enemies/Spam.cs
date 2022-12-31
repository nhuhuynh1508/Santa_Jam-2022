using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spam : Enemy
{
    public GameObject sprites;
    public float shakeInterval;
    public float shakeDist;
    private float secondsToShake = 0f;

    public Spam() : base(5f, 1f, 1f, 1f)
    {

    }

    public override void _Update()
    {
        Vector2 dir = player.transform.position - transform.position;
        transform.up = dir;

        secondsToShake -= Time.deltaTime;
        if (secondsToShake <= 0)
        {
            secondsToShake = shakeInterval;
            sprites.transform.localPosition = new Vector2(Random.Range(-shakeDist, shakeDist), Random.Range(-shakeDist, shakeDist));
        }
    }
}
