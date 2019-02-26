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
    
    public float autoMoveDownTime = 0.4f;
    // To deal with: lose as soon as top hits (not just when middle)
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
                    GetComponent<AudioSource>().Play();
                    spawner.Spawn();
                    spawner.CheckForClear();
                }

                movedTime = Time.time;
            }
        }

        else if (Input.GetKeyDown(KeyCode.X)) // 
                 {
                     if (Mathf.Abs(movedTime - Time.time) > 0.10f)
                     {
                         bool rotated = rotateClockwise();
                         if (!rotated)
                         {
                             //enabled = false;
                             //GetComponent<AudioSource>().Play();
                             //spawner.Spawn();
                         }
         
                         movedTime = Time.time;
                     }
                 }
        
        else if (Input.GetKeyDown(KeyCode.Z)) // 
        {
            if (Mathf.Abs(movedTime - Time.time) > 0.10f)
            {
                bool rotated = rotateCounterclockwise();
                if (!rotated)
                {
                    //enabled = false;
                    //GetComponent<AudioSource>().Play();
                    //spawner.Spawn();
                }

                movedTime = Time.time;
            }
        }

        if (Mathf.Abs(autoMoveTime - Time.time) > autoMoveDownTime)
        {
            bool movedDown = moveDown();
            if (!movedDown)
            {
                
                enabled = false;
                GetComponent<AudioSource>().Play();
                spawner.Spawn();
                spawner.CheckForClear();
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
        currentOneGS.block = null;
        currentTwoGS.block = null;

        nextOne.block = blockOne;
        nextTwo.block = blockTwo;
        
        nextOne.isOccupied = true;
        nextTwo.isOccupied = true;
        blockOne.transform.position = nextOne.transform.position;
        blockTwo.transform.position = nextTwo.transform.position;
    }

    bool moveRight()
    {
        // in actuality, this might not be good enough with rotations...
        GridSpace nextOne = blockOne.occupying.GetRightSpace();
        GridSpace nextTwo = blockTwo.occupying.GetRightSpace();
        if  ((nextOne != null && (!nextOne.isOccupied || currentlyOccupying(nextOne))) &&
             (nextTwo != null && (!nextTwo.isOccupied || currentlyOccupying(nextTwo))))
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
        if  ((nextOne != null && (!nextOne.isOccupied || currentlyOccupying(nextOne))) &&
             (nextTwo != null && (!nextTwo.isOccupied || currentlyOccupying(nextTwo))))
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
        return false;
    }

    bool currentlyOccupying(GridSpace nextOne)
    {
        if (nextOne == null)
            return false;
        bool nextOneEquals = (nextOne.transform.gameObject == blockOne.occupying.gameObject) || 
                             (nextOne.transform.gameObject == blockTwo.occupying.gameObject);
        return nextOneEquals;
    }

    
    // ------> +X
    // down -> +Y
    // Keep one of pieces steady, rotate the other one.
    // will clean uppppppppp
    bool rotateCounterclockwise()
    {
        GridSpace centerGS = blockOne.occupying; // Keep still.
        GridSpace legGS = blockTwo.occupying; 
        GridSpace newLegGS = null; 
        
  
        if (centerGS.x - legGS.x > 0) // to the left of center
        {
            newLegGS = centerGS.GetDownSpace();

        }
        
        else if (centerGS.x - legGS.x < 0) // to the right of center
        {
            newLegGS = centerGS.GetUpSpace();

        }
        
        else if (centerGS.y - legGS.y > 0) // above center
        {
            newLegGS = centerGS.GetLeftSpace();
   
        }
        
        else if (centerGS.y - legGS.y < 0)
        {
            newLegGS = centerGS.GetRightSpace();
   
        }
        
        if (newLegGS && (!newLegGS.isOccupied || currentlyOccupying(newLegGS)))
        {
            updatePosition(centerGS, newLegGS);
            return true;
        }
        
        return false;
    }


    bool rotateClockwise()
    {
        GridSpace centerGS = blockOne.occupying; // Keep still.
        GridSpace legGS = blockTwo.occupying;
        GridSpace newLegGS = null;
        // 
        if (centerGS.x - legGS.x > 0) // to the left of center
        {
            newLegGS = centerGS.GetUpSpace();
        }
        
        else if (centerGS.x - legGS.x < 0) // to the right of center
        {
            newLegGS = centerGS.GetDownSpace();
        }
        
        else if (centerGS.y - legGS.y > 0) // above center
        {
            newLegGS = centerGS.GetRightSpace(); 
        }
        
        else if (centerGS.y - legGS.y < 0)
        {
            newLegGS = centerGS.GetLeftSpace();

        }
        
        if (newLegGS && (!newLegGS.isOccupied || currentlyOccupying(newLegGS)))
        {
            updatePosition(centerGS, newLegGS);
            return true;
        }   
        
        return false;
    }
}