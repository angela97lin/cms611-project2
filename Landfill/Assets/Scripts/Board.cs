using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Board : MonoBehaviour
{
    public int gridWidth = 7;
    public int gridHeight = 14;
    private float spriteSize = .15f;
    public Transform boardCenterTransform;
    public GridSpace gridSpacePrefab;
    public GamePiece gamePiecePrefab;
    public Block blockPrefab;
    public GameObject psPrefab;
    public GameObject explosion;
    public List<List<GridSpace>> board;
    public Spawner spawner;
    public int score;
    public Canvas canvas;
    public TextMeshPro scoreText;


    // Start is called before the first frame update
    void Start()
    {
        CreateBoard(gridWidth, gridHeight);
        explosion = Instantiate(psPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Stop();
        score = 0;
    }

    void Update()
    {
        canvas.GetComponentInChildren<TextMeshProUGUI>().SetText("Score: " + score);
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
                GridSpace gridSpace = Instantiate(gridSpacePrefab, GridSpacePosition(xIndex, yIndex, width, height),
                    Quaternion.identity);
                board[yIndex].Add(gridSpace);
                gridSpace.x = xIndex;
                gridSpace.y = yIndex;
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

        spawner.spawnSpace = board[0][gridWidth / 2];
        spawner.board = this;
        spawner.Spawn();
    }


    Vector2 GridSpacePosition(int xIndex, int yIndex, int width, int height)
    {
        return new Vector2(boardCenterTransform.position.x, boardCenterTransform.position.y) +
               new Vector2(xIndex - (width / 2) + 0.5f, -1 * (yIndex - (height / 2) + 0.5f)) * spriteSize;
    }


    public List<List<GridSpace>> getBoard()
    {
        return board;
    }
}