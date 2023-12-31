using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public const int Width = 43;
    public const int Height = 24;
    public const int MaxLenght = 1500;
    public const int MinLenght = 1500;

   public int[,] GenerateRiverMap()
    {
        System.Random random = new System.Random();
        // ������ ��������� ������
        int[,] riverMap = new int[Width + 10, Height + 10];

        // �������� ��������� ������� ����� ��� ������ ����
        // string[] sides = { "top", "bottom", "left", "right" };
        string startSide = "top"; 
           // sides[random.Next(sides.Length)];
        Debug.Log("����������� " + startSide);
        int startX = 0, startY = 0;
        if (startSide == "top")
        {
            startX = random.Next(8,Width - 9);
            startY = Height - 1;
        } 
        else if (startSide == "bottom")
        {
            startX = random.Next(8, Width - 9);
            startY = 0;
        }
        else if (startSide == "left") 
        {
            startX = 0;
            startY = random.Next(0, Height - 1);
        }
        else if(startSide == "right")
        {
            startX = Width - 1;
            startY = random.Next(0,Height - 1);
        } 

        int x = startX, y = startY;
        int length = 0; //������� ������ ����
        int targetLength = random.Next(MinLenght, MaxLenght + 1);

        string direction = "river 3.1";     

        

        while (true)
        {
            // ���������, ��������� �� ������� ������� ���� � �������� ����
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                length++;

                // ���������, �������� �� ���� ����� ������� ����� � ����� ����� ������������
                if (length >= targetLength && (x == 0 || x == Width - 1 || y == 0 || y == Height - 1))
                {
                    break;
                }

                /*  if ((direction == "clockWise 1.2") || (direction == "counterClockWise 2.4") || (direction == "river 3.2" ))      
                  {
                      if ((direction == "clockWise 1.2") && (x < 8))
                      {
                          string[] directions = { "counterClockWise 2.4", "river 3.2" };
                          direction = directions[random.Next(directions.Length)];
                      } 
                      else if ((direction == "counterClockWise 2.4") && (x < Width - 8))
                      {
                          string[] directions = { "clockWise 1.2", "river 3.2" };
                          direction = directions[random.Next(directions.Length)];
                      }  

                      riverBlock(direction,x,y);

                      riverMap[x, y] = 1;     // 1 - � ������ ������ ��������� ����

                      switch (direction)
                      {
                          case "clockWise 1.2":
                              if ((x >= 4) && (y >= 4))
                              {
                                  riverMap[x, y - 4] = 2;        
                                  riverMap[x - 4, y] = 2;
                              }
                              else if (x >= 4)
                              {
                                  riverMap[x - 4, y] = 2;
                              }
                              else if (y >= 4)
                              {
                                  riverMap[x, y - 4] = 2;
                              }
                              break;
                          case "counterClockWise 2.4":

                              if ((x <= Width - 5) && (y >= 4))
                              {
                                  riverMap[x, y - 4] = 2;
                                  riverMap[x + 4, y] = 2;
                              }
                              else if (x <= Width - 5)
                              {
                                  riverMap[x + 4, y] = 2;
                              }
                              else if (y >= 4)
                              {
                                  riverMap[x, y - 4] = 2;
                              }
                              break;
                          case "river 3.2":
                              if ((x <= Width - 5) && (x >= 4))
                              {
                                  riverMap[x + 4, y] = 2;
                                  riverMap[x - 4, y] = 2;
                              }
                              else if (x <= Width - 5)
                              {
                                  riverMap[x + 4, y] = 2;
                              }
                              else if (x >= 4)
                              {
                                  riverMap[x - 4, y] = 2;
                              }
                              break;
                      }

                      y += 4;         //��������� � ��������� ������ ��� ����������� ����

                      if ((riverMap[x - 4, y] == 2) && (riverMap[x + 4, y] == 2))             //��������� � ����� ������� ����� ���������� ����
                      {
                          direction = "river 3.2";                
                      }
                      else if ((riverMap[x + 4, y] == 2) && (riverMap[x, y + 4] == 2))
                      {
                          direction = "counterClockWise 2.1";
                      }
                      else if ((riverMap[x - 4, y] == 2) && (riverMap[x, y + 4] == 2))
                      {
                          direction = "clockWise 1.3";
                      }
                      else if (riverMap[x - 4, y] == 2)
                      {
                          string[] directions1 = { "clockWise 1.3", "river 3.2" };
                          direction = directions1[random.Next(directions1.Length)];
                      }
                      else if(riverMap[x, y + 4] == 2)
                      {
                          string[] directions1 = { "clockWise 1.3", "counterClockWise 2.1" };
                          direction = directions1[random.Next(directions1.Length)];
                      }
                      else if (riverMap[x + 4, y] == 2)
                      {
                          string[] directions1 = {"counterClockWise 2.1", "river 3.2" };
                          direction = directions1[random.Next(directions1.Length)];
                      }
                      else
                      {
                          string[] directions1 = { "clockWise 1.3", "counterClockWise 2.1", "river 3.2" };
                          direction = directions1[random.Next(directions1.Length)];
                      } 
           }   */
             if ((direction == "clockWise 1.1")  || (direction == "river 3.3"))                    //   || (direction == "counterClockWise 2.1") 
                {
                    if ((direction == "river 3.3") && (x < 8))
                    {
                        string[] directions2 = { "counterClockWise 2.2" };                              //"clockWise 1.2",
                        direction = directions2[random.Next(directions2.Length)];
                    }

                    riverBlock(direction, x, y);

                    riverMap[x, y] = 1;     // 1 - � ������ ������ ��������� ����

                    
  
                        switch (direction)
                        {
                            case "clockWise 1.1":
                                if ((x <= Width - 5) && (y >= 4))
                                {
                                    riverMap[x, y - 4] = 2;        // 2 - � ������ ������ ���� ���� �� �����
                                    riverMap[x + 4, y] = 2;
                                }
                                else if (x <= Width - 5)
                                {
                                    riverMap[x + 4, y] = 2;
                                }
                                else if (y >= 4)
                                {
                                    riverMap[y - 4, y] = 2;
                                }

                                break;
                          /*  case "counterClockWise 2.1":
                               
                                if ((y <= Height - 5) && (x <= Width - 5))
                                {
                                    riverMap[x, y + 4] = 2;
                                    riverMap[x + 4, y] = 2;
                                }
                                else if (x <= Width - 5)
                                {
                                    riverMap[x + 4, y] = 2;
                                }
                                else if (y <= Height - 5)
                                {
                                    riverMap[x, y + 4] = 2;
                                }
                                break;  */
                            case "river 3.3":
                               
                                if ((y <= Height - 5) && (y >= 4))
                                {
                                    riverMap[x, y + 4] = 2;
                                    riverMap[x, y - 4] = 2;
                                }
                                else if (y <= Height - 5)
                                {
                                    riverMap[x, y + 4] = 2;
                                }
                                else if (y >= 4)
                                {
                                    riverMap[x, y - 4] = 2;
                                }
                                break;
                        }
                    
                    x -= 4;

                   /* if ((riverMap[x - 4, y] == 2) && (riverMap[x, y - 4] == 2))             //��������� � ����� ������� ����� ���������� ����
                    {
                        direction = "clockWise 1.2";
                    } */
                    if ((riverMap[x - 4, y] == 2) && (riverMap[x, y + 4] == 2))
                    {
                        direction = "counterClockWise 2.2" ;
                    }
                    else if ((riverMap[x, y - 4] == 2) && (riverMap[x, y + 4] == 2))
                    {
                        direction = "clockWise 3.3";
                    }
                    else if (riverMap[x - 4, y] == 2)
                    {
                        string[] directions2 = { "counterClockWise 2.2"};                   // "clockWise 1.2", 
                        direction = directions2[random.Next(directions2.Length)];
                    }
                    else if (riverMap[x, y + 4] == 2)
                    {
                        string[] directions2 = { "counterClockWise 2.2", "river 3.3" };
                        direction = directions2[random.Next(directions2.Length)];
                    }
                    else if (riverMap[x, y - 4] == 2)
                    {
                        string[] directions2 = { "river 3.3" }; //"clockWise 1.2",
                        direction = directions2[random.Next(directions2.Length)];
                    }
                    else
                    {
                        string[] directions2 = { "counterClockWise 2.2", "river 3.3" }; //"clockWise 1.2",
                        direction = directions2[random.Next(directions2.Length)];
                    }
                }
                else if ( (direction == "counterClockWise 2.3") || (direction == "river 3.4")) //(direction == "clockWise 1.3") ||
                {
                    if ((direction == "river 3.4") && (x > Width - 8))
                    {
                        string[] directions3 = { "counterClockWise 2.3" }; //"clockWise 1.3",
                        direction = directions3[random.Next(directions3.Length)];
                    }

                    riverBlock(direction, x, y);

                    riverMap[x, y] = 1;     // 1 - � ������ ������ ��������� ����

                    switch (direction)
                    {
                       /* case "clockWise 1.3":
                          
                            if ((y <= Height - 5) && (x >= 4))
                            {
                                riverMap[x, y + 4] = 2;        
                                riverMap[x - 4, y] = 2;
                            }
                            else if (y <= Height - 5)
                            {
                                riverMap[x, y + 4] = 2;
                            }
                            else if (x >= 4)
                            {
                                riverMap[x - 4, y] = 2;
                            }
                            break; */
                        case "counterClockWise 2.3":
                          
                            if ((x >= 4) && (y >= 4))
                            {
                                riverMap[x, y - 4] = 2;
                                riverMap[x - 4, y] = 2;
                            }
                            else if (x >= 4)
                            {
                                riverMap[x - 4, y] = 2;
                            }
                            else if (y >= 4)
                            {
                                riverMap[x, y - 4] = 2;
                            }
                            break;
                        case "river 3.4":
                           
                            if ((y <= Height - 5) && (y >= 4))
                            {
                                riverMap[x, y + 4] = 2;
                                riverMap[x, y - 4] = 2;
                            }
                            else if (y <= Height - 5)
                            {
                                riverMap[x, y + 4] = 2;
                            }
                            else if (y >= 4)
                            {
                                riverMap[x, y - 4] = 2;
                            }
                            break;
                    }

                    x += 4;

                   /* if ((riverMap[x + 4, y] == 2) && (riverMap[x, y - 4] == 2))             //��������� � ����� ������� ����� ���������� ����
                    {
                        direction = "clockWise 2.4";
                    } */
                     if ((riverMap[x + 4, y] == 2) && (riverMap[x, y + 4] == 2))
                    {
                        direction = "counterClockWise 1.4";
                    }
                    else if ((riverMap[x, y - 4] == 2) && (riverMap[x, y + 4] == 2))
                    {
                        direction = "clockWise 3.4";
                    }
                    else if (riverMap[x + 4, y] == 2)
                    {
                        string[] directions3 = { "clockWise 1.4" }; //"counterClockWise 2.4"
                        direction = directions3[random.Next(directions3.Length)];
                    }
                    else if (riverMap[x, y + 4] == 2)
                    {
                        string[] directions3 = { "clockWise 1.4", "river 3.4" };
                        direction = directions3[random.Next(directions3.Length)];
                    }
                    else if (riverMap[x, y - 4] == 2)
                    {
                        string[] directions3 = {  "river 3.4" }; //"counterClockWise 2.4",
                        direction = directions3[random.Next(directions3.Length)];
                    }
                    else
                    {
                        string[] directions3 = { "clockWise 1.4",  "river 3.4" }; //"counterClockWise 2.4",
                        direction = directions3[random.Next(directions3.Length)];
                    }
                } 
                else if ((direction == "clockWise 1.4") || (direction == "counterClockWise 2.2") || (direction == "river 3.1"))               
                {
                    riverBlock(direction, x, y);

                    riverMap[x, y] = 1;     // 1 - � ������ ������ ��������� ����

                    switch (direction)
                    {
                        case "clockWise 1.4":
                            riverMap[x, y + 4] = 2;        // 2 - � ������ ������ ���� ���� �� �����
                            riverMap[x + 4, y] = 2;
                            if ((y <= Height - 5) && (x <= Width - 5))
                            {
                                riverMap[x, y + 4] = 2;        
                                riverMap[x + 4, y] = 2;
                            }
                            else if (y <= Height - 5)
                            {
                                riverMap[x, y + 4] = 2;
                            }
                            else if (x <= Width - 5)
                            {
                                riverMap[x + 4, y] = 2;
                            }
                            break;
                        case "counterClockWise 2.2":
                           
                            if ((y <= Height - 5) && (x >= 4))
                            {
                                riverMap[x, y + 4] = 2;
                                riverMap[x - 4, y] = 2;
                            }
                            else if (y <= Height - 5)
                            {
                                riverMap[x, y + 4] = 2;
                            }
                            else if (x >= 4)
                            {
                                riverMap[x - 4, y] = 2;
                            }
                            break;
                        case "river 3.1":
                            
                            if ((x <= Width - 5) && (x >= 4))
                            {
                                riverMap[x + 4, y] = 2;
                                riverMap[x - 4, y] = 2;
                            }
                            else if (x <= Width - 5)
                            {
                                riverMap[x + 4, y] = 2;
                            }
                            else if (x >= 4)
                            {
                                riverMap[x - 4, y] = 2;
                            }
                            break;
                    }

                    y -= 4;

                    if ((riverMap[x + 4, y] == 2) && (riverMap[x, y - 4] == 2))             //��������� � ����� ������� ����� ���������� ����
                    {
                        direction = "clockWise 1.1";
                    }
                    else if ((riverMap[x + 4, y] == 2) && (riverMap[x - 4, y] == 2))
                    {
                        direction = "counterClockWise 3.1";
                    }
                    else if ((riverMap[x, y - 4] == 2) && (riverMap[x - 4, y] == 2))
                    {
                        direction = "clockWise 2.3";
                    }
                    else if (riverMap[x + 4, y] == 2)
                    {
                        string[] directions4 = { "river 3.1", "clockWise 1.1" };
                        direction = directions4[random.Next(directions4.Length)];
                    }
                    else if (riverMap[x - 4, y] == 2)
                    {
                        string[] directions4 = { "river 3.1", "counterClockWise 2.3" };
                        direction = directions4[random.Next(directions4.Length)];
                    }
                    else if (riverMap[x, y - 4] == 2)
                    {
                        string[] directions4 = { "clockWise 1.1", "counterClockWise 2.3" };
                        direction = directions4[random.Next(directions4.Length)];
                    }
                    else
                    {
                        string[] directions4 = { "river 3.1", "clockWise 1.1", "counterClockWise 2.3" };
                        direction = directions4[random.Next(directions4.Length)];
                    }
                }
            }
            else
            {
                break;  // ���� ������� ������� ���� ������� �� ������� ����, ��������� ����
            }
        }

        return riverMap;

    }

    private void Start()
    {
        int[,] riverMap = GenerateRiverMap();

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
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverDownLeftBank);
                Debug.Log(block);
                break;
            case "river 3.2":
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverUp);                                      
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverUpLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverUp);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverUpRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverUpLeftBank);
                Debug.Log(block);
                break;
            case "river 3.3":
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverLLeftBank);
                Debug.Log(block);
                break;
            case "river 3.4":
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverRLeftBank);
                Debug.Log(block);
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
                Debug.Log(block);
                break;
            case "counterClockWise 2.2":
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), cCWLeftDownRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), cCWLeftDownRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), cCWLeftDownRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), cCWLeftDownRiverDownBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverLRightBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverLeft);
                Debug.Log(block);
                break;
            case "counterClockWise 2.3":
                tilemap.SetTile(new Vector3Int(x, y, 0), cCWDownRightRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cCWDownRightRiver);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cCWDownRightRiver);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cCWDownRightRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverDownLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverRRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                Debug.Log(block);
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
                Debug.Log(block);
                break;
            case "clockWise 1.1":
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), cWDownLeftRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), cWDownLeftRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), cWDownLeftRiver);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), cWDownLeftRiverDownBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverLLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverLeft);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), riverLeft);
                Debug.Log(block);
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
                Debug.Log(block);
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
                Debug.Log(block);
                break;
            case "clockWise 1.4":
                tilemap.SetTile(new Vector3Int(x + 3, y + 3, 0), cWRightDownRiverUpBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 2, 0), cWRightDownRiver);
                tilemap.SetTile(new Vector3Int(x + 1, y + 1, 0), cWRightDownRiver);
                tilemap.SetTile(new Vector3Int(x, y, 0), cWRightDownRiverDownBank);
                tilemap.SetTile(new Vector3Int(x, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 1, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x + 2, y + 3, 0), riverRLeftBank);
                tilemap.SetTile(new Vector3Int(x, y + 1, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 1, y + 2, 0), riverRight);
                tilemap.SetTile(new Vector3Int(x + 3, y, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 1, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 3, y + 2, 0), riverDownRightBank);
                tilemap.SetTile(new Vector3Int(x + 1, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y, 0), riverDown);
                tilemap.SetTile(new Vector3Int(x + 2, y + 1, 0), riverDown);
                Debug.Log(block);
                break;
        }
    }
}
