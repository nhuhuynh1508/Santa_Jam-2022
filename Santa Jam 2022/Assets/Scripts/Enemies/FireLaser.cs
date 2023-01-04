using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public Transform laserOrigin;
    public GameObject laserLine;
    private GameObject target;

    public GameObject laser;

    [SerializeField]
    private List<GameObject> potentialTargets;

    LineRenderer lr;

    public float laserDamage = 0.5f, laserDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lr = laserLine.GetComponent<LineRenderer>();

        StartCoroutine(SpawnLaser(laserDelay));

        // Ignore bullet collider
        Physics2D.IgnoreLayerCollision(9, 7);
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    void Update() 
    {
        if (potentialTargets.Count == 0)
        {
            lr.SetPosition(0, laserOrigin.position);
            lr.SetPosition(1, laserOrigin.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "RangePlayer")
        {
            potentialTargets.Add(collision.gameObject);;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision) 
    {
        potentialTargets.Remove(collision.gameObject);
    }

    IEnumerator SpawnLaser(float delay) 
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (potentialTargets.Count > 0)
                target = potentialTargets[Random.Range(0, potentialTargets.Count - 1)];
            else continue;

            lr.SetPosition(0, laserOrigin.position);
            lr.SetPosition(1, target.transform.position);

            if (target.gameObject.tag == "Enemy")
            {
                target.GetComponent<Enemy>().TakeDamage(laserDamage / 2);
            }
            else if (target.gameObject.tag == "RangePlayer")
            {
                target.GetComponent<Player>().TakeDamage(laserDamage);
            }
        }
    }
}
