using System.Collections;
using UnityEngine;

public class SpawnerGarbageGameOverScene : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 2.0f;
    public Transform[] spawnPoints;
    public float moveSpeed = 2.0f;
    public float maxYOffset = 5.0f;
    public float destroyYThreshold = 10.0f; // Порог по Y, после которого объект уничтожается
    public int simultaneousSpawnCount = 5;

    public float rotationSpeed = 28f; // Скорость вращения
    

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            for (int i = 0; i < simultaneousSpawnCount; i++)
            {
                int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomSpawnIndex];
                StartCoroutine(SpawnObjectCoroutine(spawnPoint));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator SpawnObjectCoroutine(Transform spawnPoint)
    {
        GameObject randomPrefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        GameObject spawnedObject = Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
        spawnedObject.transform.SetParent(transform);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

        while (spawnedObject.transform.localPosition.y < maxYOffset)
        {
            rb.velocity = Vector2.up * moveSpeed;
            spawnedObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);


            // Уничтожение объекта при достижении пороговой координаты по Y
            if (spawnedObject.transform.localPosition.y >= destroyYThreshold)
            {
                Destroy(spawnedObject);
                yield break; // Выходим из корутины
            }

            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
