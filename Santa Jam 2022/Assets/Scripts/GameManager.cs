using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playerHealth = 0f;
    public float playerExp = 0f;
    public int[] expMilestone;
    private int currentMilestone = -1;

    public Canvas powerupPicker;

    private Player player;
    private Rigidbody2D playerRB;
    //private float pushBackForce = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 7);
        Physics2D.IgnoreLayerCollision(7, 7);

        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(6, 8);
        Physics2D.IgnoreLayerCollision(8, 8);

        Physics2D.IgnoreLayerCollision(11, 0);
        Physics2D.IgnoreLayerCollision(11, 1);
        Physics2D.IgnoreLayerCollision(11, 2);
        Physics2D.IgnoreLayerCollision(11, 3);
        Physics2D.IgnoreLayerCollision(11, 4);
        Physics2D.IgnoreLayerCollision(11, 5);
        Physics2D.IgnoreLayerCollision(11, 6);
        Physics2D.IgnoreLayerCollision(11, 7);
        Physics2D.IgnoreLayerCollision(11, 8);
        Physics2D.IgnoreLayerCollision(11, 9);


        player = Player.instance;
        playerRB = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //BoundChecking();
    }

    public void OnEnemyDie(Enemy enemy)
    {
        playerExp += enemy.exp;

        if (playerExp >= expMilestone[currentMilestone + 1])
        {
            currentMilestone += 1;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        powerupPicker.GetComponent<PowerupPicker>().OnLevelUp();
    }

    //private void BoundChecking()
    //{
        //Vector2 playerPos = player.transform.position;
        //int dir = 1;

        //if (Mathf.Abs(playerPos.x) >= BOUNDS_X_POSITION)
        //{
        //    if (playerPos.x >= 0)
        //        dir = -1;
        //    else 
        //        dir = 1;
        //    playerRB.AddForce(transform.right * dir * pushBackForce);
        //}

        //if (Mathf.Abs(playerPos.y) >= BOUNDS_Y_POSITION)
        //{
        //    if (playerPos.y >= 0)
        //        dir = -1;
        //    else 
        //        dir = 1;
        //    playerRB.AddForce(transform.up * dir * pushBackForce);
        //}
    //}



    public int GetCurrentMilestone()
    {
        return currentMilestone;
    }
}
