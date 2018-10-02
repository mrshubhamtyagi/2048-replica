using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverPanel;

    private TileScript[,] allTiles = new TileScript[4, 4]; // all tiles

    private List<TileScript[]> columns = new List<TileScript[]>(); // holds all cols details
    private List<TileScript[]> rows = new List<TileScript[]>(); // holds all rows details

    private List<TileScript> emptyTiles = new List<TileScript>(); // list of empty tiles

    // Use this for initialization
    void Start()
    {
        gameOverPanel.SetActive(false);

        // set all tiles number's to 0 and add them to empty tiles list
        TileScript[] allTilesOnDimention = FindObjectsOfType<TileScript>();
        foreach (TileScript tile in allTilesOnDimention)
        {
            tile.Number = 0;
            allTiles[tile.indexRow, tile.indexCol] = tile;
            emptyTiles.Add(tile);
        }

        AssignColumnAndRowLists();
        GenerateTile();
        GenerateTile();
    }

    private void AssignColumnAndRowLists()
    {
        //          col1    col2    col3    col4
        // row1     0,0     0,1     0,2     0,3
        // row2     1,0     1,1     1,2     1,3
        // row3     2,0     2,1     2,2     2,3
        // row4     3,0     3,1     3,2     3,3

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
    private bool GenerateTile()
    {
        if (emptyTiles.Count > 0)
        {
            // pick index for random number tile;
            int indexForNewNumber = Random.Range(0, emptyTiles.Count);
            int randomNumber = Random.Range(0, 10);
            if (randomNumber > 8) // around 20% chances
                emptyTiles[indexForNewNumber].Number = 4;
            else
                emptyTiles[indexForNewNumber].Number = 2;
            emptyTiles.RemoveAt(indexForNewNumber);
            return true;
        }
        return false;
    }

    private void UpdateEmptyTiles()
    {
        if (emptyTiles.Count == 0)
            gameOverPanel.SetActive(true);

        emptyTiles.Clear();
        foreach (TileScript tile in allTiles)
        {
            if (tile.Number == 0)
                emptyTiles.Add(tile);
        }
    }

    private void ResetMergeTags()
    {
        foreach (TileScript tile in allTiles)
            tile.hasMergedAlready = false;
    }

    #region MOVE  AND MERGE MECHANISM -----------------------------------------------------
    public void MoveAndMerge(MoveDirection _direction)
    {
        bool moveMade = false;
        ResetMergeTags();
        for (int i = 0; i < rows.Count; i++)
        {
            switch (_direction)
            {
                case MoveDirection.Down:
                    while (MakeOneMoveUpIndex(columns[i])) { moveMade = true; }
                    break;

                case MoveDirection.Right:
                    while (MakeOneMoveUpIndex(rows[i])) { moveMade = true; }
                    break;

                case MoveDirection.Left:
                    while (MakeOneMoveDownIndex(rows[i])) { moveMade = true; }
                    break;

                case MoveDirection.Up:
                    while (MakeOneMoveDownIndex(columns[i])) { moveMade = true; }
                    break;
            }
        }
        if (moveMade)
        {
            UpdateEmptyTiles();
            GenerateTile();
        }
    }


    bool MakeOneMoveDownIndex(TileScript[] _lineOfTiles)
    {
        // This method is called on Right or Down Arrow/Swipe
        // search for move if available make a single move and return true else return false
        for (int i = 0; i < _lineOfTiles.Length - 1; i++)
        {
            // MOVE BLOCK
            if (_lineOfTiles[i].Number == 0 && _lineOfTiles[i + 1].Number != 0) // check if move is available
            {
                _lineOfTiles[i].Number = _lineOfTiles[i + 1].Number; // i is the available empty tile
                _lineOfTiles[i + 1].Number = 0;
                return true;
            }

            // MERGE BLOCK
            if (_lineOfTiles[i].Number != 0 && _lineOfTiles[i + 1].Number != 0
                && _lineOfTiles[i].Number == _lineOfTiles[i + 1].Number
                && !_lineOfTiles[i].hasMergedAlready
                && !_lineOfTiles[i + 1].hasMergedAlready)
            {
                _lineOfTiles[i].Number *= 2;
                _lineOfTiles[i + 1].Number = 0;
                _lineOfTiles[i].hasMergedAlready = true;
                ScoreManager.INSTANCE.UpdateCurrentScore(_lineOfTiles[i].Number); //  Update Score
                return true;
            }
        }
        return false;
    }

    bool MakeOneMoveUpIndex(TileScript[] _lineOfTiles)
    {
        // This method is called on Left or Up Arrow/Swipe
        // search for move if available make a single move and return true else return false
        for (int i = _lineOfTiles.Length - 1; i > 0; i--)
        {
            // Move Block
            if (_lineOfTiles[i].Number == 0 && _lineOfTiles[i - 1].Number != 0)
            {
                _lineOfTiles[i].Number = _lineOfTiles[i - 1].Number;
                _lineOfTiles[i - 1].Number = 0;
                return true;
            }

            // MERGE BLOCK
            if (_lineOfTiles[i].Number != 0 && _lineOfTiles[i - 1].Number != 0
                && _lineOfTiles[i].Number == _lineOfTiles[i - 1].Number
                && !_lineOfTiles[i].hasMergedAlready
                && !_lineOfTiles[i - 1].hasMergedAlready)
            {
                _lineOfTiles[i].Number *= 2;
                _lineOfTiles[i - 1].Number = 0;
                _lineOfTiles[i].hasMergedAlready = true;
                ScoreManager.INSTANCE.UpdateCurrentScore(_lineOfTiles[i].Number); //  Update Score
                return true;
            }
        }
        return false;
    }
    #endregion


    public void NewGame_Btn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
