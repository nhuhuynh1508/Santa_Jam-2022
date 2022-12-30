using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float speed;

    private float maxHealth;
    public float currentHealth;

    private float exp = 1f;

    public float damage;

    private GameObject player;


    // CONSTRUCTOR
    public Enemy(float speed, float maxHealth, float damage, float exp)
    {
        this.speed = speed;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.exp = exp;
    }



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("RangePlayer");
    }

    // Update is called once per frame
    public void Update()
    {
        Movement();
    }



    // Movement: use MoveTowards; distance & direction not yet used
    private void Movement()
    {
        // distance
        float distance = Vector2.Distance(transform.position, player.transform.position);
        // direction
        Vector2 direction = player.transform.position - transform.position;

        // move toward the player 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    // Damge system
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //healthBar.fillAmount = currentHealth / maxHealth;
        
        if (currentHealth <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    public virtual void Die()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("A lo?");
        if (collider.gameObject.tag == "Player Bullet")
        {
            TakeDamage(1);
            Destroy(collider.gameObject);
        }
    }

}
