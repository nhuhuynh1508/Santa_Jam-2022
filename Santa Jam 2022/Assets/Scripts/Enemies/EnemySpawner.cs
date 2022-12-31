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
    /*
    [SerializeField]
    public int spawnCount = 4;
    private float spawnTime = 3f;
    private float spawnDelay = 1f;

    private int remainingEnemies = 0;

    public static EnemySpawner instance;
    */
    private bool isTerminTime = false;


    private void Update()
    {
        difficulty += Time.deltaTime;

        secondsToNextWave -= Time.deltaTime;
        if (secondsToNextWave <= 0)
        {
            secondsToNextWave = secondsPerWave;

            if (difficulty < 300)
            {
                groupsLeft += groupsPerWave;
            }
            else
            {
                isTerminTime = true;
            }

            currentBiasedPositionIndex += 1;
            if (currentBiasedPositionIndex >= spawnPositions.Length)
            {
                currentBiasedPositionIndex = 0;
            }
        }

        if (isTerminTime)
        {
            SpawnBoss();
        }
        else if (groupsLeft > 0)
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
            Instantiate(enemyPrefab, GetSpawnPosition(), Quaternion.identity);
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

    /*
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnObjects());
    

    private IEnumerator SpawnObjects()
    {
        WaitForSeconds wait = new(spawnDelay);

        // Initial wait
        yield return new WaitForSeconds(spawnTime);

        for (int count = spawnCount; count > 0; --count)
        {
            Vector2 postospawn = new Vector2(UnityEngine.Random.Range(-5.5f, 5.5f), UnityEngine.Random.Range(-3.0f, 3.0f));
            GameObject clone = Instantiate(spyWarePrefab, postospawn, transform.rotation);
            clone.transform.parent = enemiesContainer.transform;

            // Detect when an enemy gets destroyed
            DestroyEventEmitter destroyEventEmitter = clone.AddComponent<DestroyEventEmitter>();
            destroyEventEmitter.OnObjectDestroyedEvent += OnGameObjectDestroyed;
            remainingEnemies++;

            // Wait before next spawn
            yield return wait;
        }

        Debug.Log("All the enemies have been instantiated!");
    }

    private void OnGameObjectDestroyed(DestroyEventEmitter emitter)
    {
        remainingEnemies--;
        emitter.OnObjectDestroyedEvent -= OnGameObjectDestroyed;

        Debug.Log("Remaining enemies: " + remainingEnemies);

        if (remainingEnemies == 0)
        {
            Debug.Log("All enemies have been destroyed");
        }
    }


    // EVENT CLASS
    public class DestroyEventEmitter : MonoBehaviour
    {
        public delegate void OnObjectDestroyedEventHandler(DestroyEventEmitter emitter);
        public event OnObjectDestroyedEventHandler OnObjectDestroyedEvent;
        private void OnDestroy()
        {
            OnObjectDestroyedEvent?.Invoke(this);
        }
    }
    */

    [Serializable]
    public struct GroupInfo
    {
        public int enemyID;
        public int quantity;
    }
}


/*public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyprefab;

    [SerializeField]
    private float spawntime;
    private float spawndelay;

    void Start()
    {
        StartCoroutine(spawnenemy(spawninterval, enemyprefab));
    }

    private ienumerator spawnenemy(float interval, gameobject enemy)
    {
        waitforseconds wait = new waitforseconds(spawndelay);

        yield return new waitforseconds(interval);
        gameobject newenemy = instantiate(enemy, new vector3(random.range(-5f, 5), random.range(-6f, 6f), 0), quaternion.identity);
        startcoroutine(spawnenemy(interval, enemy));

        //timer += time.deltatime;

        //if (timer >= interval)
        //{
        //    timer = 0;
        //    instantiate(enemyprefab, transform.position, transform.rotation);
        //}
    }
}*/


