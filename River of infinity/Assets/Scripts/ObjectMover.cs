using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObjectMover : MonoBehaviour
{
    private Tilemap riverTilemap;
    Vector3 nearestRiverPoint;
    public List<GameObject> objectsThatCollided = new List<GameObject>();
    public bool isMoving = true; // Флаг для определения, двигается ли объект или остановлен
    private bool isRiverReached = false; // Флаг для определения, достиг ли объект реки

    public Vector2 size = new Vector2(3f, 3f);
    public float distance = 0f;
    public LayerMask layerMask;
    //  public UnityEngine.Color targetColor = UnityEngine.Color.blue;

    private GameObject dam;
    private bool damdamCompletelyBuilt = false;

    RiverMapGenerator riverMapGenerator;
    int[,] riverMapc;

    public Animator animator;
    private void Start()
    {
        // Находим объект Tilemap по тегу "River"
        GameObject river = GameObject.FindGameObjectWithTag("River");
        riverTilemap = river.GetComponent<Tilemap>();
        nearestRiverPoint = FindClosestRiverPoint();

        riverMapGenerator = river.GetComponent<RiverMapGenerator>();
       riverMapc = riverMapGenerator.riverMap;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FakeRiver"))
        {
            isMoving = false;
            isRiverReached = true;
            gameObject.tag = "ObjectTouchedRiver";
          //  animator.SetBool("Moving", false);
            //  Debug.Log("ТРИГЕР СРАБОТАЛ");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("StopMovableObject")) || (collision.gameObject.CompareTag("ObjectTouchedRiver")) && (gameObject.tag == "MovableObject"))
        {
          //  Debug.Log("пробка ёпта");
            isMoving = false;
            gameObject.tag = "StopMovableObject";

            ObjectMover collidedObject = collision.gameObject.GetComponent<ObjectMover>();
            if (collidedObject != null)
            {
                collidedObject.objectsThatCollided.Add(gameObject);
            }


        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("ObjectTouchedRiver") || (collision.gameObject.CompareTag("StopMovableObject") && (gameObject.tag == "MovableObject"))))
        {
           // gameObject.GetComponent<ObjectMover>().isMoving = false;
            isMoving = false;
            gameObject.tag = "StopMovableObject";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("MovableObject")) && (gameObject.CompareTag("StopMovableObject")))
        {
            //  Debug.Log("cнова поехали");
            gameObject.tag = "MovableObject";
            isMoving = true;
        }
    }

    private void Update()
    {
        dam = GameObject.FindWithTag("Dam");
        
        if (dam != null)
        {
            damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;
            if (damdamCompletelyBuilt == true)
            {
                animator.enabled = false;
            }
        }
       
        if (isMoving)
        {
            animator.SetBool("Moving", true);
        }
        else if (!isMoving)
        {
            animator.SetBool("Moving", false);
        }

        
        // frontObjectCheck();

        // Находим ближайшую точку соприкосновения с рекой


        // Если ближайшая точка найдена, двигаем объект к ней
        if (nearestRiverPoint != Vector3.zero)
        {
            if ((isMoving && !isRiverReached) && (dam == null))
            {
                transform.position = Vector3.MoveTowards(transform.position, nearestRiverPoint, Time.deltaTime * 2f);
            }
            else if((isMoving && !isRiverReached) && (dam != null) && (damdamCompletelyBuilt == false))
            {
                damdamCompletelyBuilt = dam.GetComponent<DamLogic>().damCompletelyBuilt;

                transform.position = Vector3.MoveTowards(transform.position, nearestRiverPoint, Time.deltaTime * 2f);
            }
            else if((dam != null) && (damdamCompletelyBuilt == true))
            {
                animator.SetBool("Moving", false);
                animator.enabled = false;
                return;
            }
        }
    }

    private Vector3 FindClosestRiverPoint()
    {
        Vector3 playerPosition = transform.position;
        Vector3 nearestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        if (riverTilemap != null)
        {
            BoundsInt bounds = riverTilemap.cellBounds;
            TileBase[] allTiles = riverTilemap.GetTilesBlock(bounds);
            System.Random random = new System.Random();
            while (true)
            {
                int randX = random.Next(bounds.xMin, bounds.xMax);
                int randY = random.Next(bounds.yMin, bounds.yMax);

                Vector3Int cellPosition = new Vector3Int(randX, randY, 0);
                TileBase tile = allTiles[(randX - bounds.xMin) + (randY - bounds.yMin) * bounds.size.x];

                if (tile != null)
                {

                     Vector3 cellCenter = riverTilemap.GetCellCenterWorld(cellPosition);
                     Vector3 cellXMinYMin = new Vector3(cellCenter.x - 2f, cellCenter.y - 2f, 0);
                     Vector3 cellXMinYMax = new Vector3(cellCenter.x - 2f, cellCenter.y + 2f, 0);
                     Vector3 cellXMaxYMin = new Vector3(cellCenter.x + 2f, cellCenter.y - 2f, 0);
                     Vector3 cellXMaxYMax = new Vector3(cellCenter.x + 2f, cellCenter.y + 2f, 0);
                     float distance = Vector3.Distance(playerPosition, cellXMaxYMax);
                     float distance1 = Vector3.Distance(playerPosition, cellXMaxYMin);
                     float distance2 = Vector3.Distance(playerPosition, cellXMinYMax);
                     float distance3 = Vector3.Distance(playerPosition, cellXMinYMin);

                     if (distance1 < closestDistance)
                     {
                         nearestPoint = cellXMaxYMin;
                         closestDistance = distance1;
                     }
                     if (distance2 < closestDistance)
                     {
                         nearestPoint = cellXMinYMax;
                         closestDistance = distance2;
                     }
                     if (distance3 < closestDistance)
                     {
                         nearestPoint = cellXMinYMin;
                         closestDistance = distance3;
                     }
                     if (distance < closestDistance)
                     {
                         nearestPoint = cellXMaxYMax;
                         closestDistance = distance;
                     }
                    break;
                }
            }
           /* int rand = random.Next(0, allTiles.Length - 1);
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);
                    TileBase tile = allTiles[(x - bounds.xMin) + (y - bounds.yMin) * bounds.size.x];
                    

                    if (tile != null)
                    {

                        Vector3 cellCenter = riverTilemap.GetCellCenterWorld(cellPosition);
                        Vector3 cellXMinYMin = new Vector3(cellCenter.x - 2f, cellCenter.y - 2f, 0);
                        Vector3 cellXMinYMax = new Vector3(cellCenter.x - 2f, cellCenter.y + 2f, 0);
                        Vector3 cellXMaxYMin = new Vector3(cellCenter.x + 2f, cellCenter.y - 2f, 0);
                        Vector3 cellXMaxYMax = new Vector3(cellCenter.x + 2f, cellCenter.y + 2f, 0);
                        float distance = Vector3.Distance(playerPosition, cellXMaxYMax);
                        float distance1 = Vector3.Distance(playerPosition, cellXMaxYMin);
                        float distance2 = Vector3.Distance(playerPosition, cellXMinYMax);
                        float distance3 = Vector3.Distance(playerPosition, cellXMinYMin);

                        if (distance1 < closestDistance)
                        {
                            nearestPoint = cellXMaxYMin;
                            closestDistance = distance1;
                        }
                        if (distance2 < closestDistance)
                        {
                            nearestPoint = cellXMinYMax;
                            closestDistance = distance2;
                        }
                        if (distance3 < closestDistance)
                        {
                            nearestPoint = cellXMinYMin;
                            closestDistance = distance3;
                        }
                        if (distance < closestDistance)
                        {
                            nearestPoint = cellXMaxYMax;
                            closestDistance = distance;
                        }
                    }
                }
            }*/
        }

        return nearestPoint;
    }

    public void frontObjectCheck()
    {
        Vector2 origin = transform.position + new Vector3(3, 0, 0);
        Vector2 direction = transform.forward;
           float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
      /*  Vector3 topLeft = origin + (Vector2)(rotation * new Vector2(-size.x / 2, size.y / 2)) + (Vector2)direction * distance;
        Vector3 topRight = origin + (Vector2)(rotation * new Vector2(size.x / 2, size.y / 2)) + (Vector2)direction * distance;
        Vector3 bottomLeft = origin + (Vector2)(rotation * new Vector2(-size.x / 2, -size.y / 2)) + (Vector2)direction * distance;
        Vector3 bottomRight = origin + (Vector2)(rotation * new Vector2(size.x / 2, -size.y / 2)) + (Vector2)direction * distance;

        // Учитываем расстояние для отрисовки прямоугольника
        topLeft += (Vector3)(direction * distance);
        topRight += (Vector3)(direction * distance);
        bottomLeft += (Vector3)(direction * distance);
        bottomRight += (Vector3)(direction * distance);

        Debug.DrawLine(topLeft, topRight, UnityEngine.Color.red);
        Debug.DrawLine(topRight, bottomRight, UnityEngine.Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, UnityEngine.Color.red);
        Debug.DrawLine(bottomLeft, topLeft, UnityEngine.Color.red);*/
        if (hit.collider.CompareTag("StopMovableObject"))
        {
            Debug.Log("СТОПМУВ");
            isMoving = false;
            gameObject.tag = "StopMovableObject";
        }
        else if (hit.collider.CompareTag("ObjectTouchedRiver"))
        {
            Debug.Log("ОБЪЕКТРЕКА");
            isMoving = false;
            gameObject.tag = "StopMovableObject";
        }
        else if (!(hit.collider.CompareTag("StopMovableObject")) || (!(hit.collider.CompareTag("ObjectTouchedRiver"))))
        {
            gameObject.tag = "MovableObject";
          //  Debug.Log("ГОГОГО");
            isMoving = true;
        }else if (isMoving == false)
        {
            gameObject.tag = "StopMovableObject";

        }
    }
  /*  void OnDrawGizmos()
    {
        Vector2 origin = transform.position + new Vector3(3, 0, 0);
        Vector2 direction = transform.forward;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Отрисовываем прямоугольник области действия BoxCast
        Vector3 topLeft = origin + (Vector2)(rotation * new Vector2(-size.x / 2, size.y / 2)) + (Vector2)direction * distance;
        Vector3 topRight = origin + (Vector2)(rotation * new Vector2(size.x / 2, size.y / 2)) + (Vector2)direction * distance;
        Vector3 bottomLeft = origin + (Vector2)(rotation * new Vector2(-size.x / 2, -size.y / 2)) + (Vector2)direction * distance;
        Vector3 bottomRight = origin + (Vector2)(rotation * new Vector2(size.x / 2, -size.y / 2)) + (Vector2)direction * distance;

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);


    }*/ 

}


