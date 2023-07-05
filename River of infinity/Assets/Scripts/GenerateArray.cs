using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateArray : MonoBehaviour
{
    public const int Width = 44;
    public const int Height = 25;
                                                                                        
    public static int[,] GenerateArrayMap()                                             // 1 - Левый берег реки
    {                                                                                   // 2 - Река
        int[,] map = new int[Width, Height];                                            // 3 - Правый берег реки
                                                                                        //
        map[Random.Range(2,Width - 6), 0] = 1;                                          //
                                                                                        //
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
               if (map[x , y] == 1)
                {
                    map[x , y + 1 ] = 1;
                    map[x + 1 , y ] = 2;
                    map[x + 2 , y ] = 3;
                }
            }
        }
        return map;
    }

    private void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        Sprite grass = Resources.Load("Grass1") as Sprite;
        Tile grassTile = ScriptableObject.CreateInstance<Tile>();
        grassTile.sprite = grass;

        AnimatedTile riverAnim = Resources.Load("River down") as AnimatedTile;
        AnimatedTile RiverRightBank = Resources.Load("River down the right bank") as AnimatedTile;
        AnimatedTile RiverLeftBank = Resources.Load("River down the left bank") as AnimatedTile;

        RenderMap(GenerateArrayMap(), tilemap, grassTile, riverAnim);
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile, AnimatedTile animTile)
    {
        AnimatedTile riverAnim = Resources.Load("River down") as AnimatedTile;
        AnimatedTile RiverRightBank = Resources.Load("River down the right bank") as AnimatedTile;
        AnimatedTile RiverLeftBank = Resources.Load("River down the left bank") as AnimatedTile;
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x,y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x - Width/2, y - Height/2, 0), RiverLeftBank);
                } 
                else if (map[x,y] == 2)
                {
                    tilemap.SetTile(new Vector3Int(x - Width / 2, y - Height / 2, 0), riverAnim);
                }
                else if (map[x,y] == 3)
                {
                    tilemap.SetTile(new Vector3Int(x - Width / 2, y - Height / 2, 0), RiverRightBank);
                }
              //  else
              //  {
               //     tilemap.SetTile(new Vector3Int(x - Width/2, y - Height/2, 0), tile);
               // }
                
            }
        }
    }
}
