using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("The Player")]
    [SerializeField]
    GameObject player;

    [Tooltip("The Space Fox Enemy to Spawn")]
    [SerializeField]
    GameObject spaceFox;

    [Tooltip("How long for initial spawn delay")]
    [SerializeField]
    float SpawnTime = 3f;

    [Tooltip("How far away from the player the enemies must be to spawn")]
    [SerializeField]
    float SpawnDistance = 3f;

    Vector2 TopRight, SpawnLocation;
    float halfWidth, halfHeight, actualSpawnDistance;
    bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        TopRight = new Vector2(1, 1);
        Vector2 SizeVector = Camera.main.ViewportToWorldPoint(TopRight);
        halfHeight = SizeVector.y * 0.9f;
        halfWidth = SizeVector.x * 0.9f;
     
        // Make sure the SpawnDistance translates between different screen sizes
        actualSpawnDistance = (Vector2.Distance(TopRight * SizeVector, Vector2.zero) / SizeVector.magnitude * SpawnDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            GenerateSpawnPoint();
            while (Vector2.Distance(SpawnLocation, player.transform.position) < actualSpawnDistance)
            {
                GenerateSpawnPoint();
            }
            StartCoroutine(SpawnCountDown());
            SpawnTime *= 0.9f;
            spawning = true;
        }
    }

    void GenerateSpawnPoint()
    {
        float x, y;
        x = Random.Range(-halfWidth, halfWidth);
        y = Random.Range(-halfHeight, halfHeight);
        SpawnLocation = new Vector2(x, y);
    }

    void SpawnEnemy()
    {
        Instantiate(spaceFox,SpawnLocation,Quaternion.identity);
    }

    IEnumerator SpawnCountDown()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(SpawnTime);
        spawning = false;
    }
}
