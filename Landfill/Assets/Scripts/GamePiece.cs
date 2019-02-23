using System.Collections;
using System.Collections.Generic;
using UnityEditor.StyleSheets;
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

    
    // To deal with: lose as soon as top hits?
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
                movedTime = Time.time;
            }
        }

        else if (Input.GetAxisRaw("Vertical") < -0.5f) // Down
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.5f)
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

        else if (Input.GetKeyDown(KeyCode.X)) // 
                 {
                     if (Mathf.Abs(movedTime - Time.time) > 0.15f)
                     {
                         bool rotated = rotateClockwise();
                         if (!rotated)
                         {
                             //enabled = false;
                             //spawner.Spawn();
         
                         }
         
                         movedTime = Time.time;
                     }
                 }
        
        else if (Input.GetKeyDown(KeyCode.Z)) // 
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.15f)
            {
                bool rotated = rotateCounterclockwise();
                if (!rotated)
                {
                    //enabled = false;
                    //spawner.Spawn();

                }

                movedTime = Time.time;
            }
        }

        if (Mathf.Abs(autoMoveTime - Time.time) > 0.2f)
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
        currentOneGS.isOccupied = false;
        currentTwoGS.isOccupied = false;

        nextOne.isOccupied = true;
        nextTwo.isOccupied = true;


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

        if  ((nextOne != null && (!nextOne.isOccupied || currentlyOccupying(nextOne))) &&
             (nextTwo != null && (!nextTwo.isOccupied || currentlyOccupying(nextTwo))))

        {
            updatePosition(nextOne, nextTwo);
            return true;
        }

        // print("false");

        return false;
    }

    bool currentlyOccupying(GridSpace nextOne)
    {
        if (nextOne == null)
            return false;
        bool nextOneEquals = (nextOne.transform.gameObject == blockOne.occupying.gameObject) || 
                             (nextOne.transform.gameObject == blockTwo.occupying.gameObject);
        print("currently occupying: " + nextOneEquals);
        return nextOneEquals;
    }

    
    // ------> +X
    // down -> +Y
    // Keep one of pieces steady, rotate the other one.
    bool rotateCounterclockwise()
    {
        GridSpace centerGS = blockOne.occupying; // Keep still.
        GridSpace legGS = blockTwo.occupying; 
  
        // 
        if (centerGS.x - legGS.x > 0) // to the left of center
        {
            legGS = centerGS.GetDownSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }
        }
        
        else if (centerGS.x - legGS.x < 0) // to the right of center
        {
            legGS = centerGS.GetUpSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        else if (centerGS.y - legGS.y > 0) // above center
        {
            legGS = centerGS.GetLeftSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        else if (centerGS.y - legGS.y < 0)
        {
            legGS = centerGS.GetRightSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        return false;
    }


    bool rotateClockwise()
    {
        GridSpace centerGS = blockOne.occupying; // Keep still.
        GridSpace legGS = blockTwo.occupying; 
  
        // 
        if (centerGS.x - legGS.x > 0) // to the left of center
        {
            legGS = centerGS.GetUpSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }
        }
        
        else if (centerGS.x - legGS.x < 0) // to the right of center
        {
            legGS = centerGS.GetDownSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        else if (centerGS.y - legGS.y > 0) // above center
        {
            legGS = centerGS.GetRightSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        else if (centerGS.y - legGS.y < 0)
        {
            legGS = centerGS.GetLeftSpace();
            if (legGS && !legGS.isOccupied)
            {
                updatePosition(centerGS, legGS);
                return true;
            }    
        }
        
        return false;
    }
}