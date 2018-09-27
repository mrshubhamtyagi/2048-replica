using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private TileScript[,] allTiles = new TileScript[4, 4];
    private List<TileScript> tileList = new List<TileScript>();

    // Use this for initialization
    void Start()
    {
        TileScript[] allTilesOnDimention = FindObjectsOfType<TileScript>();
        foreach (TileScript tile in allTilesOnDimention)
        {
            tile.Number = 0;
            allTiles[tile.indexRow, tile.indexCol] = tile;
            tileList.Add(tile);
        }
    }

    // ----------------------------------------- Generating New Tile With a number either 2 or 4
    private void GenerateTile()
    {
        if (tileList.Count > 0)
        {
            // pick index for random number tile;
            int indexForNewNumber = Random.Range(0, tileList.Count);
            int randomNumber = Random.Range(0, 10);
            if (randomNumber == 0)
                tileList[indexForNewNumber].Number = 4; // assign 4 (only 10% chances)
            else
                tileList[indexForNewNumber].Number = 2;
            tileList.RemoveAt(indexForNewNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateTile();
    }

    public void Move(MoveDirection direction)
    {
        print(direction);
    }
}
