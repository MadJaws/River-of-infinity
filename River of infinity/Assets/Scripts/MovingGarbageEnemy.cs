using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading;
using Unity.Mathematics;

public class MovingGarbageEnemy : MonoBehaviour
{
  //  private SpawnGarbage spawnGarbage;
    // int[,] riverMap;
    List<int> riverFlowMap;

    public float lastXRiver;
    public float lastYRiver;
    public GameObject prefabDam;

    private GameObject _closestObject;
    public GameObject riverMapGenerator;
    public GameObject ClosestObject { get { return _closestObject; } }

    private int _closestObjectIndex;
    public int ClosestObjectIndex { get { return _closestObjectIndex; } }
    int checkPoint = 0;
    int startCheckPoint;

    int speed = 4;
    float deviationFactor = 0;

    bool screenBorderCrossed = false;

    public float epsilon = 0.1f;

    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    public float rotationSpeed = 280f; // Скорость вращения
    void Start()
    {
        SetClosestObject(ClosestObject);
        SetClosestObjectIndex(ClosestObjectIndex);
        // creationGarbage = GetComponentInParent<CreationGarbage>();
        startCheckPoint = _closestObjectIndex;
        checkPoint = startCheckPoint * 2;

        GameObject river = GameObject.FindGameObjectWithTag("River");
        // spawnGarbage = river.GetComponent<SpawnGarbage>();
        //  riverMap = spawnGarbage.riverMap;
        riverFlowMap = river.GetComponent<RiverMapGenerator>().riverFlowMap;

        deviationFactor = UnityEngine.Random.Range(-1f, 1.7f);

        prefabDam = Resources.Load<GameObject>("prefab/PillarShort1");     
        lastXRiver = river.GetComponent<RiverMapGenerator>().lastX;
        lastYRiver = river.GetComponent<RiverMapGenerator>().lastY;
        
         
       
    }

    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        if (_closestObject != null)
        {
            if (dam == null)
            {
                if (checkPoint + 2 < riverFlowMap.Count)
                {
                    MovingCheck();
                }
                else
                {
                    ScreenBorderCrossingCheck();

                    if (screenBorderCrossed == false)
                    {
                        NearestScreenBorder();
                    }
                    else
                    {
                        GarbageDamageController garbageDamageController = gameObject.GetComponent<GarbageDamageController>();
                        garbageDamageController.Die();

                       // Debug.Log("ставит плоитну мусор");
                       // Instantiate(prefabDam, new Vector3(lastXRiver - 19.8f, lastYRiver - 15.25f, 0), Quaternion.identity);
                    }
                }
            } 
            else if(dam != null)
            {
                damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;

                if (damdamCompletelyBuilt == true)
                {
                    return;
                }
                else
                {
                    if (checkPoint + 2 < riverFlowMap.Count)
                    {
                        MovingCheck();
                    }
                    else
                    {
                        ScreenBorderCrossingCheck();

                        if (screenBorderCrossed == false)
                        {
                            NearestScreenBorder();
                        }
                        else
                        {
                            GarbageDamageController garbageDamageController = gameObject.GetComponent<GarbageDamageController>();
                            garbageDamageController.Die();

                           // Debug.Log("ставит плоитну");
                           // Instantiate(prefabDam, new Vector3(lastXRiver - 19.8f, lastYRiver - 15.25f, 0), Quaternion.identity);
                        }
                    }
                }
            }
          //  }
        }
      
    }
    private void MovingCheck()
    {
        Vector3 targetPoint = new Vector3(riverFlowMap[checkPoint + 2] - 21.8f + deviationFactor , riverFlowMap[checkPoint + 3] - 12f + deviationFactor, 0);
       // Debug.Log(targetPoint);
       // Debug.Log(transform.position);
        if(Vector3.Distance(transform.position,targetPoint) < 0.1)
        {
            checkPoint += 2;
        }
        else
        {
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);
        }
    }
    public void SetClosestObject(GameObject closestObject)
    {
        _closestObject = closestObject;
    }

    public void SetClosestObjectIndex(int closestObjectIndex)
    {
        _closestObjectIndex = closestObjectIndex;
    }

    bool AreVectorsAlmostEqual(Vector3 v1, Vector3 v2, float epsilon)
    {
        return Vector3.Distance(v1, v2) < epsilon;
    }

    public void NearestScreenBorder()
    {
        Vector3 objectPosition = transform.position;

        //float distanceToTop = Mathf.Abs(Camera.main.orthographicSize - objectPosition.y);
        float distanceToBottom = Mathf.Abs(-Camera.main.orthographicSize - objectPosition.y);
        float distanceToLeft = Mathf.Abs(-Camera.main.orthographicSize * Camera.main.aspect - objectPosition.x);
        float distanceToRight = Mathf.Abs(Camera.main.orthographicSize * Camera.main.aspect - objectPosition.x);

        float minDistance = Mathf.Min(distanceToBottom, distanceToLeft, distanceToRight);

       // if (minDistance == distanceToTop)
       // {
       //     Debug.Log("Closest wall: Top");
       // }
        if (minDistance == distanceToBottom)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else if (minDistance == distanceToLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    public void ScreenBorderCrossingCheck()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            screenBorderCrossed = true;
        }
    }
}
