using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;
    
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.velocity = new Vector2(0, 0);

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        movement.Normalize();
        
        rb2D.velocity = movement * movementSpeed * Time.fixedDeltaTime;
    }
}
