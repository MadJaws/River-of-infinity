using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RiverMapGenerator : MonoBehaviour
{
    public const int Width = 40;
    public const int Height = 24;
    public const int MaxLenght = 150;
    public const int MinLenght = 150;
    public int[,] riverMap;

    public List<int> riverFlowMap;

    public int startX = 0, startY = 0;
    public int lastX = 0, lastY = 0;

    public GameObject targetForEnemy;
    public GameObject squarePrefab;

    public Tilemap tilemapGrass; // Ссылка на компонент Tilemap, на котором вы хотите создать тайлы
    public Tilemap tilemapDecor;
    public TileBase tileToCreateGrass; // Тайл, который вы хотите создать
    public GameObject tileToCreateDecorFlowerPink;
    public GameObject tileToCreateDecorFlowerRed;
    public GameObject tileToCreateDecorFlowerWhite;
    public GameObject tileToCreateDecorTreeApples;
    public GameObject tileToCreateDecorTreeNoApples;
    public Transform parentObject;

    public void fillingMassifRiver(int x, int y)
    {
        int xCount = 0;
        int maxX = riverMap.GetLength(0); // Максимальный индекс по x
        int maxY = riverMap.GetLength(1); // Максимальный индекс по y


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 1 && j == 1)
                {
                    riverMap[x, y] = 2;
                    GameObject newTrgetForEnemy = Instantiate(targetForEnemy, new Vector3(x - 21.8f, y - 12f,0), Quaternion.identity);
                  //  GameObject targetsForEnemy = GameObject.FindWithTag("TargetsForEnemy");
                   // Transform newParentTransform = targetsForEnemy.GetComponent<Transform>();

                   // newTrgetForEnemy.transform.SetParent(newParentTransform);

                }
                else
                {
                    if (x >= 0 && x < maxX && y >= 0 && y < maxY)
                    {
                        riverMap[x, y] = 1;
                    }
                    
                }
                y--;
            }
            x++;
            y += 5;
            xCount++;
        }
    }

    public void buldingFakeRiver()
    {
        int decorCounter1 = 0;
        int decorCounter2 = 0;

        GameObject tileContainer = new GameObject("TileContainer");
        for (int x = 0; x < riverMap.GetLength(0); x++)
        {
            for (int y = 0; y < riverMap.GetLength(1); y++)
            {
                if (riverMap[x, y] == 1)                                                        //Создаём копию реки
                {

                    GameObject squareObject = Instantiate(squarePrefab);
                    squareObject.transform.position = new Vector3(x, y, 0);
                    BoxCollider2D collider = squareObject.AddComponent<BoxCollider2D>();
                    Rigidbody2D rigidbody = squareObject.AddComponent<Rigidbody2D>();
                    rigidbody.isKinematic = true;
                    collider.isTrigger = true;
                    squareObject.tag = "FakeRiver";
                    squareObject.transform.SetParent(tileContainer.transform);
                }
                else if (riverMap[x, y] == 0)                                                   //Создаём травы на другом тайлмапе
                {
                    tilemapGrass.SetTile(new Vector3Int(x, y + 3, 0), tileToCreateGrass);

                    // Создаем декор на другом тайлмапе
                    List<string> optionDecorFlowerList = new List<string> { "FlowerPink", "FlowerRed", "FlowerWhite","","" };
                    List<string> optionDecorTreeList = new List<string> { "TreeApples", "TreeNoApples", "", "" };

                    int randomFlowerIndex = UnityEngine.Random.Range(0, optionDecorFlowerList.Count);
                    string randomFlower = optionDecorFlowerList[randomFlowerIndex];

                    int randomTreeIndex = UnityEngine.Random.Range(0, optionDecorTreeList.Count);
                    string randomTree = optionDecorTreeList[randomTreeIndex];

                    if (decorCounter1 % 7 == 0)
                    {
                        switch (randomFlower)
                        {
                            case "FlowerPink":
                                GameObject newFlowerPink = Instantiate(tileToCreateDecorFlowerPink, new Vector3(x -21.8f , y - 11, 0), Quaternion.identity);
                                newFlowerPink.GetComponent<Renderer>().sortingOrder += 1;

                                break;
                            case "FlowerRed":
                                GameObject newFlowerRed = Instantiate(tileToCreateDecorFlowerRed, new Vector3(x - 21.8f, y - 11, 0), Quaternion.identity);
                                newFlowerRed.GetComponent<Renderer>().sortingOrder += 1;

                                break;

                            case "FlowerWhite":
                                GameObject newFlowerWhite = Instantiate(tileToCreateDecorFlowerWhite, new Vector3(x - 21.8f, y - 11, 0), Quaternion.identity);
                                newFlowerWhite.GetComponent<Renderer>().sortingOrder += 1;

                                break;
                        }
                    }
                   /* else if (decorCounter2 % 6 == 0)
                    {
                        switch (randomTree)
                        {
                            case "TreeApples":
                                GameObject newTree = Instantiate(tileToCreateDecorTreeApples, new Vector3(x - 21.8f, y - 12, 0), Quaternion.identity);

                                break;
                            case "TreeNoApples":
                                GameObject newTreeApple = Instantiate(tileToCreateDecorTreeNoApples, new Vector3(x - 21.8f, y - 12, 0), Quaternion.identity);
                                break;
                        } 
                    }*/
                    decorCounter1++;
                    decorCounter2++;
                }
            }
        }
        tileContainer.AddComponent<FakeRiverTransformPosition>();
    }
    public int[,] GenerateRiverMap(out List<int> riverFlowMap1)
    {
        riverFlowMap1 = new List<int>();

        System.Random random = new System.Random();
        // Создаём двумерный массив
        riverMap = new int[Width, Height];

        // Выбираем случайную сторону карты для начала реки
        // string[] sides = { "top", "bottom", "left", "right" };
        string startSide = "top";
        // sides[random.Next(sides.Length)];
        //  Debug.Log("направление " + startSide);

        if (startSide == "top")
        {
            List<int> numberList = new List<int> { 10, 15, 20, 25 };
            // List<int> numberList = new List<int> { 36 };
            // Генерация случайного индекса из диапазона списка
            int randomIndex = UnityEngine.Random.Range(0, numberList.Count);

            // Получение случайного числа из списка по случайному индексу
            int randomNumber = numberList[randomIndex];


            startX = randomNumber;
            startY = Height - 1;
        }
        /*  else if (startSide == "bottom")
          {
              startX = random.Next(8, Width - 9);
              startY = 0;
          }
          else if (startSide == "left")
          {
              startX = 0;
              startY = random.Next(0, Height - 1);
          }
          else if (startSide == "right")
          {
              startX = Width - 1;
              startY = random.Next(0, Height - 1);
          } */

        int x = startX, y = startY;
        int length = 0; //Счетчик длинны реки
        int targetLength = random.Next(MinLenght, MaxLenght + 1);

        string direction = "river 3.1";



        while (true)
        {

            // Проверяем, находится ли текущая позиция реки в пределах поля
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                length += 5;

                // Проверяем, достигла ли река своей целевой длины и конца карты одновременно
                if (length >= targetLength && (x == 0 || x == Width - 1 || y == 0 || y == Height - 1))
                {
                    break;
                }
                /* if (x <= 7)
                 {
                     switch (direction)
                     {
                      //   case "clockWise 1.1":
                      //       string[] directions = { "counterClockWise 2.3", "river 3.1" };
                      //       direction = directions[random.Next(directions.Length)];
                      //       break;
                         case "river 3.3":
                             direction = "counterClockWise 2.2";
                             break;
                         case "river 1.1":
                             direction = "counterClockWise 2.2";
                             break;
                     }
                 }
                 else if (x >= Width - 7)
                 {
                     switch(direction)
                     {
                        // case "counterClockWise 2.3":
                        //     string[] directions = { "counterClockWise 1.1", "river 3.1" };
                        //     direction = directions[random.Next(directions.Length)];
                        //     break;
                         case "river 3.4":
                             direction = "counterClockWise 1.4";
                             break;
                         case "river 2.3":
                             direction = "counterClockWise 1.4";
                             break;
                     }
                 }*/

                if ((direction == "clockWise 1.1") || (direction == "river 3.3"))
                {


                    riverBlock(direction, x, y);

                    fillingMassifRiver(x, y);

                    riverFlowMap1.Add(x + 1);
                    riverFlowMap1.Add(y - 1);

                    lastX = x;
                    lastY = y;

                    x -= 5;

                   
                    if (x <= 7)
                    {
                        direction = "counterClockWise 2.2";
                    }
                    else
                    {
                        string[] directions = { "counterClockWise 2.2", "river 3.3" };
                        direction = directions[random.Next(directions.Length)];
                    }
                }
                else if ((direction == "counterClockWise 2.3") || (direction == "river 3.4"))
                {
                    riverBlock(direction, x, y);

                    fillingMassifRiver(x, y);

                    riverFlowMap1.Add(x + 1);
                    riverFlowMap1.Add(y - 1);

                    lastX = x;
                    lastY = y;

                    x += 5;

                    if (x >= Width - 7)
                    {
                        direction = "clockWise 1.4";
                    }
                    else
                    {
                        string[] directions = { "clockWise 1.4", "river 3.4" };
                        direction = directions[random.Next(directions.Length)];
                    }
                }
                else if ((direction == "clockWise 1.4") || (direction == "counterClockWise 2.2") || (direction == "river 3.1"))
                {
                    riverBlock(direction, x, y);

                    fillingMassifRiver(x, y);

                    riverFlowMap1.Add(x + 1);
                    riverFlowMap1.Add(y - 1);

                    lastX = x;
                    lastY = y;

                    y -= 5;

                    if (x >= Width - 7)
                    {
                        string[] directions = { "river 3.1", "clockWise 1.1" };
                        direction = directions[random.Next(directions.Length)];
                    }
                    else if (x <= 7)
                    {
                        string[] directions = { "river 3.1", "counterClockWise 2.3" };
                        direction = directions[random.Next(directions.Length)];
                    }
                    else
                    {
                        string[] directions = { "river 3.1", "clockWise 1.1", "counterClockWise 2.3" };
                        direction = directions[random.Next(directions.Length)];
                    }
                }
            else
            {
                break;  // Если текущая позиция реки выходит за пределы поля, завершаем цикл
            }
        } 
            else 
            {
                break; 
            }
        }
        return riverMap;
    }

    private void Start()
    {
        

        int[,] riverMap = GenerateRiverMap(out riverFlowMap);

        buldingFakeRiver();

      /*  for (int i = 0; i < riverMap.GetLength(1); i++)
        {
            string row = "";
            for (int j = 0; j < riverMap.GetLength(0); j++)
            {
                row += riverMap[j, i] + " ";
            }
            Debug.Log("Row " + i + ": " + row);
        } */
    }


    
    public void riverBlock(string block, int x, int y)
    {
        Tilemap tilemap = GetComponent<Tilemap>();


        AnimatedTile riverDown = Resources.Load("River down") as AnimatedTile;
        AnimatedTile riverDownRightBank = Resources.Load("River down the right bank") as AnimatedTile;
        AnimatedTile riverDownLeftBank = Resources.Load("River down the left bank") as AnimatedTile;
        

        AnimatedTile riverUp = Resources.Load("River up") as AnimatedTile;
        AnimatedTile riverUpRightBank = Resources.Load("River up the right bank") as AnimatedTile;
        AnimatedTile riverUpLeftBank = Resources.Load("River up the left bank") as AnimatedTile;

        AnimatedTile riverRight = Resources.Load("River right") as AnimatedTile;
        AnimatedTile riverRRightBank = Resources.Load("River right the right bank") as AnimatedTile;
        AnimatedTile riverRLeftBank = Resources.Load("River right the left bank") as AnimatedTile;

        AnimatedTile riverLeft = Resources.Load("River left") as AnimatedTile;
        AnimatedTile riverLRightBank = Resources.Load("River left the right bank") as AnimatedTile;
        AnimatedTile riverLLeftBank = Resources.Load("River left the left bank") as AnimatedTile;


        AnimatedTile cCWUpLeftRiverUpBank = Resources.Load("counterClockWiseUpLeftRiverUpBank") as AnimatedTile;
        AnimatedTile cCWUpLeftRiver = Resources.Load("counterClockWiseUpLeftRiver") as AnimatedTile;
        AnimatedTile cCWUpLeftRiverDownBank = Resources.Load("counterClockWiseUpLeftRiverDownBank") as AnimatedTile;

        AnimatedTile cCWLeftDownRiverUpBank = Resources.Load("counterClockWiseLeftDownRiverUpBank") as AnimatedTile;
        AnimatedTile cCWLeftDownRiver = Resources.Load("counterClockWiseLeftDownRiver") as AnimatedTile;
        AnimatedTile cCWLeftDownRiverDownBank = Resources.Load("counterClockWiseLeftDownRiverDownBank") as AnimatedTile;

        AnimatedTile cCWDownRightRiverUpBank = Resources.Load("counterClockWiseDownRightRiverUpBank") as AnimatedTile;
        AnimatedTile cCWDownRightRiver = Resources.Load("counterClockWiseDownRightRiver") as AnimatedTile;
        AnimatedTile cCWDownRightRiverDownBank = Resources.Load("counterClockWiseDownRightRiverDownBank") as AnimatedTile;

        AnimatedTile cCWRightUpRiverUpBank = Resources.Load("counterClockWiseRightUpRiverUpBank") as AnimatedTile;
        AnimatedTile cCWRightUpRiver = Resources.Load("counterClockWiseRightUpRiver") as AnimatedTile;
        AnimatedTile cCWRightUpRiverDownBank = Resources.Load("counterClockWiseRightUpRiverDownBank") as AnimatedTile;


        AnimatedTile cWDownLeftRiverUpBank = Resources.Load("ClockWiseDownLeftRiverUpBank") as AnimatedTile;
        AnimatedTile cWDownLeftRiver = Resources.Load("ClockWiseDownLeftRiver") as AnimatedTile;
        AnimatedTile cWDownLeftRiverDownBank = Resources.Load("ClockWiseDownLeftRiverDownBank") as AnimatedTile;

        AnimatedTile cWLeftUpRiverUpBank = Resources.Load("ClockWiseLeftUpRiverUpBank") as AnimatedTile;
        AnimatedTile cWLeftUpRiver = Resources.Load("ClockWiseLeftUpRiver") as AnimatedTile;
        AnimatedTile cWLeftUpRiverDownBank = Resources.Load("ClockWiseLeftUpRiverDownBank") as AnimatedTile;

        AnimatedTile cWUpRightRiverUpBank = Resources.Load("ClockWiseUpRightRiverUpBank") as AnimatedTile;
        AnimatedTile cWUpRightRiver = Resources.Load("ClockWiseUpRightRiver") as AnimatedTile;
        AnimatedTile cWUpRightRiverDownBank = Resources.Load("ClockWiseUpRightRiverDownBank") as AnimatedTile;

        AnimatedTile cWRightDownRiverUpBank = Resources.Load("ClockWiseRightDownRiverUpBank") as AnimatedTile;
        AnimatedTile cWRightDownRiver = Resources.Load("ClockWiseRightDownRiver") as AnimatedTile;
        AnimatedTile cWRightDownRiverDownBank = Resources.Load("ClockWiseRightDownRiverDownBank") as AnimatedTile;


        switch (block)
        {
            case "river 3.1":
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverDownLeftBank);
              //  Debug.Log(block);
                break;
            case "river 3.2":
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverUpLeftBank);
             //   Debug.Log(block);
                break;
            case "river 3.3":
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverLLeftBank);
             //   Debug.Log(block);
                break;
            case "river 3.4":
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverRight); 
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), riverRLeftBank);
             //   Debug.Log(block);
                break;
            case "counterClockWise 2.1":
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cCWUpLeftRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cCWUpLeftRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cCWUpLeftRiver);
                tilemap.SetTile(new Vector3Int(x, y, 0), cCWUpLeftRiverDownBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverLeft);
             //   Debug.Log(block);
                break;
            case "counterClockWise 2.2":
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), cCWLeftDownRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), cCWLeftDownRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cCWLeftDownRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), cCWLeftDownRiver);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), cCWLeftDownRiverDownBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverLeft);
                //  Debug.Log(block);
                break;
            case "counterClockWise 2.3":
                tilemap.SetTile(new Vector3Int(x, y, 0), cCWDownRightRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cCWDownRightRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cCWDownRightRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cCWDownRightRiver);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), cCWDownRightRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverDown);
                //  Debug.Log(block);
                break;
            case "counterClockWise 2.4":
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), cCWRightUpRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), cCWRightUpRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), cCWRightUpRiver);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), cCWRightUpRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverUp);
              //  Debug.Log(block);
                break;
            case "clockWise 1.1":
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), cWDownLeftRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), cWDownLeftRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cWDownLeftRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), cWDownLeftRiver);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), cWDownLeftRiverDownBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverLeft);
                //   Debug.Log(block);
                break;
            case "clockWise 1.2":
                tilemap.SetTile(new Vector3Int(x, y, 0), cWLeftUpRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cWLeftUpRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cWLeftUpRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cWLeftUpRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverLeft);
              //  Debug.Log(block);
                break;
            case "clockWise 1.3":
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), cWUpRightRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), cWUpRightRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), cWUpRightRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), cWUpRightRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverRight);
              //  Debug.Log(block);
                break;
            case "clockWise 1.4":
                tilemap.SetTile(new Vector3Int(x + 4, y + 4, 0), cWRightDownRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cWRightDownRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cWRightDownRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cWRightDownRiver);
                tilemap.SetTile(new Vector3Int(x, y, 0), cWRightDownRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 4, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 4, y, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 4, y + 3, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDown);
                // Debug.Log(block);
                break;

        }
    }
}


