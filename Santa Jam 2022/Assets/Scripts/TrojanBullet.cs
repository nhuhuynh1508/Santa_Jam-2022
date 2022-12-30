using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanBullet : MonoBehaviour
{
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
}
