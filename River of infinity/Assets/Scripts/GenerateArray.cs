using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateArray : MonoBehaviour
{
    public const int Width = 44;
    public const int Height = 25;
    public static int[,] GenerateArrayMap()
    {
        
        
        int[,] map = new int[Width, Height];

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                if(x == Width/2 || x == Width/2 +1 || x == Width/2 - 1)
                {
                    map[x, y] = 0;
                }
                else
                { 
                    map[x, y] = 1; 
                }
                
            }
        }
        return map;
    }

    private void Start()
    {
       
        Sprite grass = Resources.Load("Grass1") as Sprite;
        Tilemap tilemap = GetComponent<Tilemap>();
       // Tile river = Resources.Load("") as Tile;
        AnimatedTile riverAnim = Resources.Load("River down") as AnimatedTile;
       // Debug.Log(tilemap.size);
       // Debug.Log(grass);
        Tile grassTile = ScriptableObject.CreateInstance<Tile>();
        grassTile.sprite = grass;
        // tilemap.SetTile(new Vector3Int(0,0,0), grassTile);
        // GenerateArrayMap(50, 24);
       
        RenderMap(GenerateArrayMap(), tilemap, grassTile, riverAnim);
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile, AnimatedTile animTile)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for(int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (map[x,y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x - Width/2, y - Height/2, 0), animTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x - Width/2, y - Height/2, 0), tile);
                }
                
            }
        }
    }
}
