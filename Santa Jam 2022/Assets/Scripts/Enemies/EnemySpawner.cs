using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spyWarePrefab;
    [SerializeField]
    private GameObject spyWareContainer;

    [SerializeField]
    public int spawnCount = 4;
    private float spawnTime = 3f;
    private float spawnDelay = 1f;

    private int remainingEnemies = 0;

    public static EnemySpawner instance;
    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        WaitForSeconds wait = new(spawnDelay);

        // Initial wait
        yield return new WaitForSeconds(spawnTime);

        for (int count = spawnCount; count > 0; --count)
        {
            Vector2 postospawn = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.0f, 3.0f));
            GameObject clone = Instantiate(spyWarePrefab, postospawn, transform.rotation);
            clone.transform.parent = spyWareContainer.transform;

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


