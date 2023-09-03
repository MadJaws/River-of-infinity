using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreator : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ �������, ������� ����� ����������� �� ���� ������
    public float spawnInterval = 2f; // �������� ������� ����� ���������� ��������
    public float spawnRadius = 2f;   // ������ ��� �������� ������������ � ������������
   // public List<GameObject> objectsCreatorList = new List<GameObject>();
    private Camera mainCamera;

    private GameObject dam;
    private GameObject enemy;
    private bool damdamCompletelyBuilt;

    private float timer = 0f;
    private float startTimer = 2f;

    private SpriteRenderer sr;

    public bool objectRight;

    void Start()
    {
        mainCamera = Camera.main;

        
        // ��������� ��������, ������� ����� ��������� ������� �� �������
      //  StartCoroutine(SpawnObjectsWithInterval());
    }

    private void Update()
    {
        TimerSpawnObject();

    }

    private IEnumerator SpawnObjectsWithInterval()
    {

        while (true)
        {
            SpawnObjectOnEdge();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObjectOnEdge()
    {
        Vector2 spawnPosition = GetRandomPositionOnEdge();
        while (HasCollisionsNearby(spawnPosition))
        {
            spawnPosition = GetRandomPositionOnEdge();
        }

        // ������� ������ �� ������������ �������
      
        enemy = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        sr = enemy.GetComponent<SpriteRenderer>();
        if (enemy.transform.position.x < 0)
        {
            sr.flipX = true;
            objectRight = true;
        }
      //  objectsCreatorList.Add(createdPrefabObject);
    }

    private Vector2 GetRandomPositionOnEdge()
    {
        // ������� ������ � ������� �����������
        float screenX = mainCamera.orthographicSize * mainCamera.aspect;
        float screenY = mainCamera.orthographicSize;

        // ���������� ��������� ������� �� ���� ������
        float randomValue = Random.value;
        Vector2 spawnPosition = Vector2.zero;

      /*  if (randomValue < 0.25f) // ������� ����
        {
            spawnPosition = new Vector2(Random.Range(-screenX, screenX), screenY);
        }
        else if (randomValue < 0.5f) // ������ ����
        {
            spawnPosition = new Vector2(Random.Range(-screenX, screenX), -screenY);
        }*/
        if (randomValue < 0.5f) // ����� ����
        {
            spawnPosition = new Vector2(-screenX, Random.Range(-screenY, screenY));
        }
        else // ������ ����
        {
            spawnPosition = new Vector2(screenX, Random.Range(-screenY, screenY));
        }

        return spawnPosition;
    }

    private bool HasCollisionsNearby(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, spawnRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider != objectToSpawn.GetComponent<Collider2D>())
            {
                return true;
            }
        }
        return false;
    }

    private void TimerSpawnObject()
    {
        timer -= Time.deltaTime;
        dam = GameObject.FindWithTag("Dam");
        if (dam == null)
        {
            if (timer <= 0f)
            {
                timer = startTimer;

                SpawnObjectOnEdge();
            } 
        }
        else if (dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;
            if((!damdamCompletelyBuilt) && (timer <= 0))
            {
                timer = startTimer;

                SpawnObjectOnEdge();
            }
            else if(damdamCompletelyBuilt)
            {
                return;
            }
        }
    }
}
