using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public Block blockOne;
    public Block blockTwo;
    static float movedTime = 0f;
    static float autoMoveTime = 0f;
    public Spawner spawner;

    void Start()
    {
    }
        
    void Update()
    {
  
        if (Input.GetAxisRaw("Horizontal") > 0.5f) // Right
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.15f)
            {
                moveRight();
                movedTime = Time.time;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.5f) // Left
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.15f)
            {

                moveLeft();
                print(autoMoveTime);
                movedTime = Time.time;
            }
        }

        else if (Input.GetAxisRaw("Vertical") < -0.5f)// Down
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.15f)
            {
                bool movedDown = moveDown();
                if (!movedDown)
                {
                    enabled = false;
                    spawner.Spawn();

                }
                movedTime = Time.time;
            }
            
        }


        if (Mathf.Abs(autoMoveTime - Time.time) > 0.25f)
        {
            bool movedDown = moveDown();
            if (!movedDown)
            {
                enabled = false;
                spawner.Spawn();

            }
            autoMoveTime = Time.time;
        }

    }



    void updatePosition(GridSpace nextOne, GridSpace nextTwo)
    {
        GridSpace currentOneGS = blockOne.occupying;
        GridSpace currentTwoGS = blockTwo.occupying;

        blockOne.occupying = nextOne;
        blockTwo.occupying = nextTwo;
        nextOne.isOccupied = true;
        nextTwo.isOccupied = true;

        currentOneGS.isOccupied = false;
        currentTwoGS.isOccupied = false;

        blockOne.transform.position = nextOne.transform.position;
        blockTwo.transform.position = nextTwo.transform.position; 
    }

    bool moveRight()
    {
        GridSpace nextOne = blockOne.occupying.GetRightSpace();
        GridSpace nextTwo = blockTwo.occupying.GetRightSpace();

        // Not a good enough check but for dummy things right now
        // Will not work for rotations and straight :(
        if (nextTwo && !nextTwo.isOccupied)
        {
            updatePosition(nextOne, nextTwo);
            return true;
        }

        return false;
    }
    
    bool moveLeft()
    {
        GridSpace nextOne = blockOne.occupying.GetLeftSpace();
        GridSpace nextTwo = blockTwo.occupying.GetLeftSpace();
        if (nextOne && !nextOne.isOccupied)
        {
            updatePosition(nextOne, nextTwo);
            return true;
        }

        return false;
    }

    bool moveDown()
    {
        GridSpace nextOne = blockOne.occupying.GetDownSpace();
        GridSpace nextTwo = blockTwo.occupying.GetDownSpace();
        if ((nextOne && !nextOne.isOccupied) && (nextTwo && !nextTwo.isOccupied))
        {
            updatePosition(nextOne, nextTwo);
            return true;
        }

        return false;
    }
}