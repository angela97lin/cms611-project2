using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public int gridWidth = 7;
    public int gridHeight = 14;
    private float spriteSize = .15f;
    public Transform boardCenterTransform;
    public GridSpace gridSpacePrefab;
    public GamePiece gamePiecePrefab;
    public Block blockPrefab;

    private List<List<GridSpace>> board;
    // Start is called before the first frame update
    void Start()
    {
        CreateBoard(gridWidth, gridHeight);
    }
    

    void CreateBoard(int width, int height)
    {
        // Initialize empty board
        board = new List<List<GridSpace>>();
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
        Spawn();
    }


    void Spawn()
    {
        GamePiece newGamePiece =
            Instantiate(gamePiecePrefab, board[0][gridWidth / 2].transform.position, Quaternion.identity);
        Block blockOne =
            Instantiate(blockPrefab, board[0][gridWidth / 2].transform.position + new Vector3(0.15f, 0.0f, 0.0f), Quaternion.identity);
        Block blockTwo =
            Instantiate(blockPrefab, board[0][gridWidth / 2].transform.position, Quaternion.identity);

        newGamePiece.blockOne = blockOne;
        newGamePiece.blockTwo = blockTwo;
        
        blockOne.occupying = board[0][gridWidth / 2];
        blockTwo.occupying = board[0][gridWidth / 2 + 1];
        board[0][gridWidth / 2].isOccupied = true;
        board[0][gridWidth / 2 + 1].isOccupied = true;
    }


    Vector2 GridSpacePosition(int xIndex, int yIndex, int width, int height)
    {
        return new Vector2(boardCenterTransform.position.x, boardCenterTransform.position.y) + new Vector2(xIndex - (width / 2) + 0.5f, -1 * (yIndex - (height / 2) + 0.5f)) * spriteSize;
    }
}
