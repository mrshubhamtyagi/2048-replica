using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}

public class InputManagerScript : MonoBehaviour
{
    private GameManagerScript gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameManager.MoveAndMerge(MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameManager.MoveAndMerge(MoveDirection.Left);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameManager.MoveAndMerge(MoveDirection.Up);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameManager.MoveAndMerge(MoveDirection.Down);

        }
    }
}
