using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private TileScript[,] allTiles = new TileScript[4, 4];

    // Use this for initialization
    void Start()
    {
        TileScript[] allTilesOnDimention = FindObjectsOfType<TileScript>();
        foreach (TileScript tile in allTilesOnDimention)
        {
            tile.Number = 0;
            allTiles[tile.indexRow, tile.indexCol] = tile;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(MoveDirection direction)
    {
        print(direction);
    }
}
