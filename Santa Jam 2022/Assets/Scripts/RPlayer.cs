using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPlayer : Player
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20.0f;
    public float attacksPerSecond = 4f;
    private float secondsToAttack = 0f;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        secondsToAttack -= Time.deltaTime;
        if (secondsToAttack <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                secondsToAttack = 1 / attacksPerSecond;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
        rb2D.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 10);
    }
}
