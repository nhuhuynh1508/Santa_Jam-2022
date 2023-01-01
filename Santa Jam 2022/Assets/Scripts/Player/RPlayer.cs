/*
    Player.cs' derivation.
    Ranged Player.
    Handle Shooting logic.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPlayer : Player
{
    public Transform firePoint;

    [Header("Default")]
    public GameObject bulletPrefab;
    public float bulletForce = 20.0f;
    public float attacksPerSecond = 4f;
    private float secondsToAttack = 0f;

    [Header("Skill")]
    public GameObject skillBulletPrefab;
    public float skillBulletForce = 20f;
    public float skillCooldown = 10f;
    private float secondsToHaveSkill = 0f;
    public int numberOfBullets = 8;
    public float spread = 45f;

    public override void _Update()
    {
        secondsToAttack -= Time.deltaTime;
        if (secondsToAttack <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                float asModifier = 1 + ASPerStack * modifiers[Powerup.AttackSpeed];
                secondsToAttack = 1 / (attacksPerSecond * asModifier);

                
                float doubleShotChance = DSCPerStack * modifiers[Powerup.DoubleShotChance];
                if (Random.Range(0f, 1f) < doubleShotChance)
                {
                    Shoot(2);
                }
                else
                {
                    Shoot(1);
                }
            }
        }

        secondsToHaveSkill -= Time.deltaTime;
        if (secondsToHaveSkill <= 0)
        {
            if (Input.GetMouseButton(1))
            {
                float scModifier = Mathf.Pow(1 - SCPerStack, modifiers[Powerup.SkillCooldown]);
                secondsToHaveSkill = skillCooldown * scModifier;

                UseSkill();
                float doubleShotChance = DSCPerStack * modifiers[Powerup.DoubleShotChance];
                if (Random.Range(0f, 1f) < doubleShotChance)
                {
                    Invoke("UseSkill", 0.18f);
                }
            }
        }

        // Regen health here using RSPerStack and modifiers[Powerup.RegenSpeed]
    }

    void Shoot(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Vector3 pos = firePoint.position + firePoint.right * 0.6f * (-(n-1) * 0.5f + i);
            GameObject bullet = Instantiate(bulletPrefab, pos, firePoint.rotation);
            Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
            // add force by multiplied the fire pointed up vector with the bullet force
            // up is the axis where the player is facing
            rb2D.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 6);
        }
    }

    void UseSkill()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = spread * (i - (numberOfBullets-1) / 2) / numberOfBullets;
            GameObject bullet = Instantiate(skillBulletPrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.z + angle));
            Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
            rb2D.AddForce(RotatedVector2(firePoint.up, angle) * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 4);
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
}
