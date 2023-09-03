using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CreationGarbage : MonoBehaviour
{
    private float timer = 0f;
    private float startTimer = 3f;

    private float timerAnim = 0f;
    private float startTimerAnim = 0.5f;

    public float x = 0f;
    public float y = 0f;
    public int closestObjectIndex;

    public float closestDistance;
    public float triggerDistance = 2f;

    
    public GameObject closestObject;
    public List<GameObject> targetsForEnemy;
    
    public GameObject[] prefabOptions;

    public GameObject garbageObject;

    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    public Animator animator;
    

    private Vector3 pointSpawnGarbage;
    private SpriteRenderer sr;
    
    // public GameObject closestObjectIndex;

    private void Start()
    {
        FillingTargetList();

        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        SearchNearestTarget();

        if (closestDistance <= triggerDistance)
         {
            TimerSpawnGarbage();
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
         if (sr.flipX == false)
        {
            pointSpawnGarbage = new Vector3(-2.8f, 2f, 0);
        }
        else
        {
            pointSpawnGarbage = new Vector3(2.8f, 2f, 0);
        }
       // animator.ResetTrigger("Attack");
         StartCoroutine(StopAnim());

        GameObject randomPrefab = prefabOptions[Random.Range(0, prefabOptions.Length)];
        garbageObject = Instantiate(randomPrefab);
        garbageObject.transform.position = transform.position + pointSpawnGarbage;
        
        // garbageObject.transform.parent = transform;
        // garbageObject.transform.position = gameObject.transform.position + new Vector3(x, y, 0);
        // garbageObject.AddComponent<MovingGarbage>();
        // garbageObject.GetComponent<MovingGarbageEnemy>().SetClosestObject(closestObject);
         garbageObject.AddComponent<MovementObjectTowardsTarget>();
         garbageObject.GetComponent<MovementObjectTowardsTarget>().SetClosestObject(closestObject);
        garbageObject.AddComponent<MovingGarbageEnemy>();
        garbageObject.GetComponent<MovingGarbageEnemy>().SetClosestObjectIndex(closestObjectIndex);
        
    }

    public void FillingTargetList()
    {
        GameObject[] objectsWithSameTag = GameObject.FindGameObjectsWithTag("TargetForEnemy");

        // Конвертируем массив в список и сохраняем в objectsList
        targetsForEnemy.AddRange(objectsWithSameTag);
    }

    public void SearchNearestTarget()
    {
       // if (targetsForEnemy.Count == 0)
       // {
        //    closestObject = null;
        //    closestDistance = Mathf.Infinity;
        //    return;
      //  }

        closestObjectIndex = 0;
        closestObject = targetsForEnemy[0];
        closestDistance = Vector3.Distance(gameObject.transform.position, closestObject.transform.position);

        // Проходимся по всем объектам из списка и находим ближайший
        for (int i = 0; i < targetsForEnemy.Count; i++)
        {
            GameObject targetObject = targetsForEnemy[i];
            float distance = Vector3.Distance(gameObject.transform.position, targetObject.transform.position);
            if (distance < closestDistance)
            {
                closestObjectIndex = i;
                closestObject = targetObject;
                closestDistance = distance;
            }
        }
       // Debug.Log(closestObject.transform.position);
    }

    private IEnumerator StopAnim()
    {
        animator.SetBool("Moving", false);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
       
        animator.ResetTrigger("Attack");
        animator.SetBool("Moving", true);
    }
}
