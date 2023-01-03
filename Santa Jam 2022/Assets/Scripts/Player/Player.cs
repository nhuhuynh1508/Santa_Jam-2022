using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Powerup
{
    MovementSpeed,
    AttackSpeed,
    DoubleShotChance,
    SkillCooldown,
    RegenSpeed
}

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public float maxHealth = 100.0f;
    public float currentHealth;
    
    protected Vector2 movement;
    protected Vector2 mousePos;

    protected Rigidbody2D rb2D;

    protected BoxCollider2D playerColliderBox;

    protected Dictionary<Powerup, int> modifiers;
    public static float MSPerStack = 0.12f;
    public static float ASPerStack = 0.15f;
    public static float DSCPerStack = 0.1f;
    public static float SCPerStack = 0.2f;
    public static float RSPerStack = 0.25f;

    public static Player instance;
    private void Awake() 
    {
        instance = this;
    }


    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerColliderBox = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;

        modifiers = new Dictionary<Powerup, int>();
        modifiers.Add(Powerup.MovementSpeed, 0);
        modifiers.Add(Powerup.AttackSpeed, 0);
        modifiers.Add(Powerup.DoubleShotChance, 10);
        modifiers.Add(Powerup.SkillCooldown, 0);
        modifiers.Add(Powerup.RegenSpeed, 0);
    }

    private void Update()
    {
        _Update();
    }

    public virtual void _Update()
    {

    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float speedModifier = 1 + MSPerStack * modifiers[Powerup.MovementSpeed];
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * speedModifier * Time.fixedDeltaTime);
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
        if (collision.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
        }
        if (currentHealth <= 0)
        {
            //move to game over
        }
    }

    public virtual void Upgrade(Powerup powerup)
    {
        modifiers[powerup] += 1;
    }
}
