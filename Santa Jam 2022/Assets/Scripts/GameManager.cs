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

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 7);
        Physics2D.IgnoreLayerCollision(7, 7);

        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(6, 8);
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
