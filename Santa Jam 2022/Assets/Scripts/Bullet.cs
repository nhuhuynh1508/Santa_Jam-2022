using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float speed = 20.0f;
    public int damage = 40;
    Rigidbody2D rb2D;
    GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb2D.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) 
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}

		Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);
	} 
}
