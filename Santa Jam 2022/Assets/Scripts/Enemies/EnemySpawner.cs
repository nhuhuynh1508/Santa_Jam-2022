using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    private float difficulty = 0f;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject adwarePrefab;
    [SerializeField]
    private GameObject spyWarePrefab;
    [SerializeField]
    private GameObject spamPrefab;
    [SerializeField]
    private GameObject trojanPrefab;

    public GameObject terminuxPrefab;

    [Header("What To Spawn")]
    public GroupInfo[] pre10;
    public GroupInfo[] pre20;
    public GroupInfo[] pre40;
    public GroupInfo[] pre80;
    public GroupInfo[] pre160;
    public GroupInfo[] pre240;
    public GroupInfo[] post240;

    [Header("Where To Spawn")]
    public Transform[] spawnPositions;
    private int currentBiasedPositionIndex = 0;
    [Range(0, 1)]
    public float biasedChance;
    public float spread = 2f;

    [Header("When To Spawn")]
    public int uselessNumber;
    private float secondsPerWave
    {
        get
        {
            return 4 + Mathf.Min(difficulty / 13, 16f);
        }
    }
    private float secondsToNextWave = 0f;
    private int groupsPerWave
    {
        get
        {
            return 3 + Mathf.FloorToInt(Mathf.Pow(difficulty / 24, 1.2f));
        }
    }
    private int groupsLeft = 0;
    private float groupSpawnInterval
    {
        get
        {
            return 0.55f - Mathf.Max(difficulty / 500, 0.3f);
        }
    }
    private float secondsToNextGroup = 0f;



    [Header("Others")]
    [SerializeField]
    private GameObject enemiesContainer;

    private bool bossSpawned = false;


    private void Update()
    {
        if (!bossSpawned)
        {
            difficulty += Time.deltaTime;
        }

        secondsToNextWave -= Time.deltaTime;
        if (secondsToNextWave <= 0)
        {
            secondsToNextWave = secondsPerWave;

            if (difficulty < 10)
            {
                groupsLeft += groupsPerWave;
            }
            else if (!bossSpawned)
            {
                SpawnBoss();
                bossSpawned = true;
            }

            currentBiasedPositionIndex += 1;
            if (currentBiasedPositionIndex >= spawnPositions.Length)
            {
                currentBiasedPositionIndex = 0;
            }
        }

        if (groupsLeft > 0)
        {
            secondsToNextGroup -= Time.deltaTime;
            if (secondsToNextGroup <= 0)
            {
                groupsLeft -= 1;
                Spawn(difficulty);
            }
        }
    }

    private void SpawnBoss()
    {
        Instantiate(terminuxPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private void Spawn(float difficulty)
    {
        GroupInfo group = GetRandomGroup(difficulty);

        GameObject enemyPrefab;
        switch (group.enemyID)
        {
            case 0:
                enemyPrefab = adwarePrefab;
                break;
            case 1:
                enemyPrefab = spyWarePrefab;
                break;
            case 2:
                enemyPrefab = spamPrefab;
                break;
            case 3:
                enemyPrefab = trojanPrefab;
                break;
            default:
                enemyPrefab = adwarePrefab;
                break;
        }

        for (int i = 0; i < group.quantity; i++)
        {
            GameObject clone = Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
            clone.transform.parent = enemiesContainer.transform;
        }
    }

    private GroupInfo GetRandomGroup(float difficulty)
    {
        GroupInfo[] groups;
        if (difficulty < 10)
        {
            groups = pre10;
        }
        else if (difficulty < 20)
        {
            groups = pre20;
        }
        else if (difficulty < 40)
        {
            groups = pre40;
        }
        else if (difficulty < 80)
        {
            groups = pre80;
        }
        else if (difficulty < 160)
        {
            groups = pre160;
        }
        else if (difficulty < 240)
        {
            groups = pre240;
        }
        else
        {
            groups = post240;
        }
        return groups[UnityEngine.Random.Range(0, groups.Length - 1)];
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 offsets = new Vector2(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread));
        return (Vector2)spawnPositions[currentBiasedPositionIndex].position + offsets;
    }

    [Serializable]
    public struct GroupInfo
    {
        public int enemyID;
        public int quantity;
    }
}





