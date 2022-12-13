using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float MaxHealth = 100.0f;
    public float currentHealth;
    
    protected Vector2 movement;
    protected Vector2 mousePos;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
    }

    private void Update() 
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void TakeDamage(float Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);

        if (currentHealth <= 0)
        {
            //move to game over
        }
    }

  
}
