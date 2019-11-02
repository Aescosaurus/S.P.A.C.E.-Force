using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("The Space Fox Enemy to Spawn")]
    [SerializeField]
    GameObject spaceFox;

    Vector2 TopRight, SpawnLocation;
    float halfWidth, halfHeight;
    float SpawnTime = 7f;
    bool spawning;
    
    // Start is called before the first frame update
    void Start()
    {
        TopRight = new Vector2(1, 1);
        Vector2 SizeVector = Camera.main.ViewportToWorldPoint(TopRight);
        halfHeight = SizeVector.y * 0.9f;
        halfWidth = SizeVector.x * 0.9f;
        Debug.Log("Width: " + halfWidth + " - Width: " + halfHeight);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            float x, y;
            x = Random.Range(-halfWidth, halfWidth);
            y = Random.Range(-halfHeight, halfHeight);
            SpawnLocation = new Vector2(x, y);
            StartCoroutine(SpawnCountDown());
            SpawnTime *= 0.9f;
            spawning = true;
        }
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
