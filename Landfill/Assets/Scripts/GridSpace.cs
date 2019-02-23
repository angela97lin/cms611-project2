using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    GridSpace leftSpace = null;
    GridSpace rightSpace = null;
    GridSpace upSpace = null;
    GridSpace downSpace = null;
    bool isActive = false;
    static float movedTime = 0f;
    public bool isOccupied = false;
    
    
    public void Update()
    {
        // NOTE: This is only for testing grid connectivity
        //if (isActive)
        //{
        //    transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        //    if (Mathf.Abs(movedTime - Time.time) > 0.0001f)
        //    {
        //        if (Input.GetAxisRaw("Horizontal") > 0.5f && rightSpace != null)
        //        {
        //            rightSpace.isActive = true;
        //            isActive = false;
        //            movedTime = Time.time;
        //        }
        //        else if (Input.GetAxisRaw("Horizontal") < -0.5f && leftSpace != null)
        //        {
        //            leftSpace.isActive = true;
        //            isActive = false;
        //            movedTime = Time.time;
        //        }
        //        else if (Input.GetAxisRaw("Vertical") > 0.5f && upSpace != null)
        //        {
        //            upSpace.isActive = true;
        //            isActive = false;
        //            movedTime = Time.time;
        //        }
        //        else if (Input.GetAxisRaw("Vertical") < -0.5f && downSpace != null)
        //        {
        //            downSpace.isActive = true;
        //            isActive = false;
        //            movedTime = Time.time;
        //        }
        //    }
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1,1,1);
        //}
    }

    public void SetActive()
    {
        isActive = true;
    }

    public GridSpace GetLeftSpace()
    {
        return leftSpace;
    }

    public GridSpace GetRightSpace()
    {
        return rightSpace;
    }

    public GridSpace GetUpSpace()
    {
        return upSpace;
    }

    public GridSpace GetDownSpace()
    {
        return downSpace;
    }

    public void SetLeftSpace(GridSpace space)
    {
        leftSpace = space;
        space.rightSpace = this;
    }

    public void SetRightSpace(GridSpace space)
    {
        rightSpace = space;
        space.leftSpace = this;
    }

    public void SetUpSpace(GridSpace space)
    {
        upSpace = space;
        space.downSpace = this;
    }

    public void SetDownSpace(GridSpace space)
    {
        downSpace = space;
        space.upSpace = this;
    }
}
