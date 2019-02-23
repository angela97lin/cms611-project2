using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public Block blockOne;
    public Block blockTwo;
    static float movedTime = 0f;

    void Start()
    {
    }
        
        
    void Update()
    {
        //todo: if valid
        if (Mathf.Abs(movedTime - Time.time) > 0.2f)
        {
            
            if (Input.GetAxisRaw("Horizontal") > 0.5f) // Right
            {


            }
            else if (Input.GetAxisRaw("Horizontal") < -0.5f) // Left
            {
                moveLeft();
                movedTime = Time.time;
            }
            else if (Input.GetAxisRaw("Vertical") > 0.5f)
            {
            }
            else if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                moveDown();
                movedTime = Time.time;
            }
        }
    }

    void moveLeft()
    {
        GridSpace currentOneGS = blockOne.occupying;
        GridSpace currentTwoGS = blockTwo.occupying;

        GridSpace nextOne = currentOneGS.GetLeftSpace();
        GridSpace nextTwo = currentTwoGS.GetLeftSpace();

        blockOne.occupying = nextOne;
        blockTwo.occupying = nextTwo;

        currentOneGS.isOccupied = false;
        currentTwoGS.isOccupied = false;

        blockOne.transform.position = nextOne.transform.position;
        blockTwo.transform.position = nextTwo.transform.position;
    }

    void moveDown()
    {
        
        // add null checkers!
        GridSpace currentOneGS = blockOne.occupying;
        GridSpace currentTwoGS = blockTwo.occupying;

        GridSpace nextOne = currentOneGS.GetDownSpace();
        GridSpace nextTwo = currentTwoGS.GetDownSpace();

        blockOne.occupying = nextOne;
        blockTwo.occupying = nextTwo;

        currentOneGS.isOccupied = false;
        currentTwoGS.isOccupied = false;

        blockOne.transform.position = nextOne.transform.position;
        blockTwo.transform.position = nextTwo.transform.position;

    }
}