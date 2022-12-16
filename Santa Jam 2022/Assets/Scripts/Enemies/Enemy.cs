using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10.0f;
    public float startSpeed;

    public GameObject player;
    private float distance;

    [SerializeField] public float maxHealth = 100.0f;
    public float currentHealth;

    //public Image healthBar;

    public float damage;

 

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = speed;
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("RangePlayer");
    }


  
    // Update is called once per frame
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

    void Die()
    {
        Debug.Log("Enemy die");
    }

    // enemies move toward the player every update
    public void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        // direction
        Vector2 direction = player.transform.position - transform.position;
        // move toward the player 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
