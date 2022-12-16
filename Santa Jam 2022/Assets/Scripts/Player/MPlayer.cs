using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayer : Player
{
    public Transform attackPoint;
    private float secondstoAttack = 0f;

    public float attackRange = 1.5f;
    public LayerMask enemyLayers;

    public float damage = 20.0f;

    // Start is called before the first frame update
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        secondstoAttack -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

    }

    void Attack()
    {
        // detect enemy in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Player hit + enemy.name");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
