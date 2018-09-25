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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameManager.Move(MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameManager.Move(MoveDirection.Left);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameManager.Move(MoveDirection.Up);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameManager.Move(MoveDirection.Down);

        }
    }
}
