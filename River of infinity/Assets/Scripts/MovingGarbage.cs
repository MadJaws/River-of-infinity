using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading;
using UnityEngine.Playables;

public class MovingGarbage : MonoBehaviour
{
    private SpawnGarbage spawnGarbage;
   // int[,] riverMap;
    List <int> riverFlowMap;

    int checkPoint = 0;

    int speed = 4;

    bool screenBorderCrossed = false;

    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    public float lastXRiver;
    public float lastYRiver;
    public GameObject prefabDam;

    void Start()
    {
        GameObject river = GameObject.FindGameObjectWithTag("River");
       // spawnGarbage = river.GetComponent<SpawnGarbage>();
      //  riverMap = spawnGarbage.riverMap;
        riverFlowMap = river.GetComponent<RiverMapGenerator>().riverFlowMap;

        lastXRiver = river.GetComponent<RiverMapGenerator>().lastX;
        lastYRiver = river.GetComponent<RiverMapGenerator>().lastY;
        prefabDam = Resources.Load<GameObject>("prefab/PillarShort1");
    }

    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");

        if (dam == null)
        {
            if (checkPoint < riverFlowMap.Count)
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
                    Debug.Log("ставит плоитну соло");
                    Instantiate(prefabDam, new Vector3(lastXRiver - 19.8f, lastYRiver - 15.25f, 0), Quaternion.identity);
                }
            }
        }
        else if(dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;

            if (damdamCompletelyBuilt)
            {
                return;
            }
            else
            {
                if (checkPoint < riverFlowMap.Count)
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
                       // Debug.Log("ставит плоитну соло");
                       // Instantiate(prefabDam, new Vector3(lastXRiver - 19.8f, lastYRiver - 15.25f, 0), Quaternion.identity);
                    }
                }
            }
        }
    }


  /*  private IEnumerator StartTimeout()
    {
        for (int i = 0; i < riverFlowMap.Count; i += 2)
        {
            int x = riverFlowMap[i];
            int y = riverFlowMap[i + 1];
            yield return new WaitForSeconds(0f);
            
             Vector3 xyi = new Vector3(x - 21.8f, y - 12f, 0);
             transform.position = Vector3.MoveTowards(transform.position, xyi, Time.deltaTime * 2f);
        }
    }*/

    private void MovingCheck()
    {
        Vector3 targetPoint = new Vector3(riverFlowMap[checkPoint] - 21.8f, riverFlowMap[checkPoint + 1] - 12f, 0);

        if (transform.position == targetPoint)
        {
            checkPoint += 2;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);
        }
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
