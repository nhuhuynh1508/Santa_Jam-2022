using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan : Enemy
{
    public GameObject bulletPrefab;
    public float bulletForce = 4f;

    public Trojan() : base(1.5f, 4f, 1f, 5f)
    {

    }

    void Start()
    {
        Die();
    }

    public override void Die()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = 360 * i / 8;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletForce);
        }
    }
}
