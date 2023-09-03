using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour
{
    private RiverMapGenerator riverMapGenerator;
    public int[,] riverMap;
    int startX;
    int startY;

    //public GameObject garbagePrefab;

    private float timer = 0f;
    private float startTimer = 8f;

    private GameObject dam;
    private bool damdamCompletelyBuilt;

    public GameObject[] prefabOptions;
    void Start()
    {
        
    }
    void Update()
    {
       if(riverMap != null)
        {
            TimerSpawnGarbage();

        }
        else
        {
            riverMapGenerator = GetComponent<RiverMapGenerator>();
            riverMap = riverMapGenerator.riverMap;
            startX = riverMapGenerator.startX; 
            startY = riverMapGenerator.startY;
        }
        
    }

    private void TimerSpawnGarbage()
    {
        timer -= Time.deltaTime;
        dam = GameObject.FindWithTag("Dam");
        if (dam == null)
        {
            if (timer <= 0f)
            {
                timer = startTimer;

                Spawn();
            }
        }
        else if (dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;
            if ((!damdamCompletelyBuilt) && (timer <= 0))
            {
                timer = startTimer;

                Spawn();
            }
            else if (damdamCompletelyBuilt)
            {
                return;
            }
        }
    }

    public void Spawn()
    {
        GameObject randomPrefab = prefabOptions[Random.Range(0, prefabOptions.Length)];
        GameObject garbageObject = Instantiate(randomPrefab);
        garbageObject.transform.position = new Vector3(startX - 20.8f, startY - 11, 0);
        
        garbageObject.transform.gameObject.AddComponent<MovingGarbage>();
        garbageObject.transform.gameObject.AddComponent<BoxCollider2D>();
    }

  
}
