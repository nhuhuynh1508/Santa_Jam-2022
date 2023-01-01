using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminux : Enemy
{
    public GameObject bulletPrefab;
    public float bulletForce = 4f;

    public Animator animator;

    [Header("Shoot Phase")]
    public float secondsPerShootPhase = 10f;
    private float secondsToShootPhase = 10f;
    public int bulletsPerShot = 8;
    public int shotPerPhase = 10;
    private int shotLeft = 0;
    public float secondsPerShot = 0.25f;
    private float secondsToShoot = 0f;
    public float bulletOffsets;

    [Header("")]
    public Transform center;

    public Terminux() : base(1f, 300f, 100f, 99f)
    {

    }

    public override void _Start()
    {
        
    }

    public override void _Update()
    {
        // Shooting
        secondsToShootPhase -= Time.deltaTime;
        if (secondsToShootPhase <= 0)
        {
            secondsToShootPhase = secondsPerShootPhase;

            shotLeft = shotPerPhase;
            animator.SetBool("IsShooting", true);
        }

        if (shotLeft > 0)
        {
            secondsToShoot -= Time.deltaTime;
            if (secondsToShoot <= 0)
            {
                secondsToShoot = secondsPerShot;

                Shoot();

                shotLeft -= 1;
                if (shotLeft <= 0)
                {
                    animator.SetBool("IsShooting", false);
                }
            }
        }

        // Looking at the player
        Vector2 dirToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(new Vector3(dirToPlayer.x, dirToPlayer.y, 0), new Vector3(0, 1, 0));

        if (transform.position.x - player.transform.position.x < 0)
            angle = -angle;

        center.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Shoot()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            float angle = 360 * i / bulletsPerShot;

            Vector2 offsets = new Vector2(0, bulletOffsets);
            offsets = RotatedVector2(offsets, angle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position + (Vector3)offsets, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletForce);
            Destroy(bullet, 3.5f);
        }
    }

    Vector2 RotatedVector2(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        return new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player Bullet")
        {
            TakeDamage(1);
            collider.gameObject.tag = "Finish";
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "RangePlayer")
        {
            // Player take damage
        }
    }
}
