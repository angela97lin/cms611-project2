using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public int gridWidth = 7;
    public int gridHeight = 14;
    public float spriteSize = 1f;
    public Transform boardCenterTransform;
    public GridSpace gridSpacePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard(gridWidth, gridHeight);
    }

    void CreateBoard(int width, int height)
    {
        // Initialize empty board
        List<List<GridSpace>> board = new List<List<GridSpace>>();
        for (int i = 0; i < height; i++)
        {
            board.Add(new List<GridSpace>());
        }

        // Create board spaces
        for (int yIndex = 0; yIndex < height; yIndex++)
        {
            for (int xIndex = 0; xIndex < width; xIndex++)
            {
                board[yIndex].Add(Instantiate(gridSpacePrefab, GridSpacePosition(xIndex, yIndex, width, height), Quaternion.identity)); // TODO: Instantiate in proper location
            }
        }

        for (int yIndex = 0; yIndex < height; yIndex++)
        {
            for (int xIndex = 0; xIndex < width; xIndex++)
            {
                GridSpace currentSpace = board[yIndex][xIndex];
                if (yIndex + 1 < height)
                {
                    currentSpace.SetDownSpace(board[yIndex + 1][xIndex]);
                }
                if (xIndex + 1 < width)
                {
                    currentSpace.SetRightSpace(board[yIndex][xIndex + 1]);
                }
            }
        }
        board[0][0].SetActive();
    }

    Vector2 GridSpacePosition(int xIndex, int yIndex, int width, int height)
    {
        return new Vector2(xIndex - (width / 2) + 0.5f, -1 * (yIndex - (height / 2) + 0.5f)) * spriteSize;
    }
}
