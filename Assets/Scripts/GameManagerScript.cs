using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private TileScript[,] allTiles = new TileScript[4, 4]; // all tiles

    private List<TileScript[]> columns = new List<TileScript[]>(); // holds all cols details
    private List<TileScript[]> rows = new List<TileScript[]>(); // holds all rows details

    private List<TileScript> emptyTiles = new List<TileScript>();

    // Use this for initialization
    void Start()
    {
        TileScript[] allTilesOnDimention = FindObjectsOfType<TileScript>();
        foreach (TileScript tile in allTilesOnDimention)
        {
            tile.Number = 0;
            allTiles[tile.indexRow, tile.indexCol] = tile;
            emptyTiles.Add(tile);
        }

        AssignColumnAndRowLists();

    }

    private void AssignColumnAndRowLists()
    {
        columns.Add(new TileScript[] { allTiles[0, 0], allTiles[1, 0], allTiles[2, 0], allTiles[3, 0] });
        columns.Add(new TileScript[] { allTiles[0, 1], allTiles[1, 1], allTiles[2, 1], allTiles[3, 1] });
        columns.Add(new TileScript[] { allTiles[0, 2], allTiles[1, 2], allTiles[2, 2], allTiles[3, 2] });
        columns.Add(new TileScript[] { allTiles[0, 3], allTiles[1, 3], allTiles[2, 3], allTiles[3, 3] });


        rows.Add(new TileScript[] { allTiles[0, 0], allTiles[0, 1], allTiles[0, 2], allTiles[0, 3] });
        rows.Add(new TileScript[] { allTiles[1, 0], allTiles[1, 1], allTiles[1, 2], allTiles[1, 3] });
        rows.Add(new TileScript[] { allTiles[2, 0], allTiles[2, 1], allTiles[2, 2], allTiles[2, 3] });
        rows.Add(new TileScript[] { allTiles[3, 0], allTiles[3, 1], allTiles[3, 2], allTiles[3, 3] });
    }



    // ----------------------------------------- Generating New Tile With a number either 2 or 4
    private void GenerateTile()
    {
        if (emptyTiles.Count > 0)
        {
            // pick index for random number tile;
            int indexForNewNumber = Random.Range(0, emptyTiles.Count);
            int randomNumber = Random.Range(0, 10);
            if (randomNumber == 0)
                emptyTiles[indexForNewNumber].Number = 4; // assign 4 (only 10% chances)
            else
                emptyTiles[indexForNewNumber].Number = 2;
            emptyTiles.RemoveAt(indexForNewNumber);
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
