using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* To discuss:
 
 
 If we move down from clear: if floating, how to move down?
 --> while no gap, everything below falls down...????
 
 */

public class Spawner : MonoBehaviour
{
    public Board board;
    // Used to spawn pawn fawn blocks!
    public GamePiece gamePiecePrefab;
    public Block blockPrefab;
    public GridSpace spawnSpace;
    public AudioClip recycleableClearClip;
    public AudioClip nuclearClearClip;
    
    // Note: this list of sprites must match up to Block.Type enum!
    public List<Sprite> sprites;

    public void Start()
    {


    }
    public Block.Type getRandomType()
    {
        int i = Random.Range(0, (int)Block.Type.Size);
        return (Block.Type) i;
    }


    // to do????
    public void fixFloatingBlocks()
    {
        List<List<GridSpace>> gameBoard = board.getBoard();
        for (int yIndex = 0; yIndex < board.gridHeight; yIndex++)
        {
            for (int xIndex = 0; xIndex < board.gridWidth; xIndex++)
            {
            }
        }
    }

    public bool CheckForExplosions()
    {
        List<List<GridSpace>> gameBoard = board.getBoard();
        for (int yIndex = 0; yIndex < board.gridHeight; yIndex++)
        {
            for (int xIndex = 0; xIndex < board.gridWidth; xIndex++)
            {
                GridSpace currentSpace = gameBoard[yIndex][xIndex];
                if (currentSpace.block != null && currentSpace.block.type == Block.Type.Nuclear)
                {
                    // should suffice to just check these?
                    GridSpace oneToRight = currentSpace.GetRightSpace();
                    GridSpace oneToBot = currentSpace.GetDownSpace();
                    if ((oneToRight != null && oneToRight.block != null && oneToRight.block.type == Block.Type.Nuclear) ||
                        (oneToBot != null && oneToBot.block != null && oneToBot.block.type == Block.Type.Nuclear))
                    {
                        print("GG you lose!!");
                        return true;
                    }
                }

       
                }
            }

        return false;

    }

    // check if three in a row ==> clear 
    public void CheckForClear()
    {
        HashSet<GridSpace> toClear = new HashSet<GridSpace>();
        List<List<GridSpace>> gameBoard = board.getBoard();
        bool clearedRecycleable = false;
        bool clearedNuclear = false;
        for (int yIndex = 0; yIndex < board.gridHeight; yIndex++)
        {
            for (int xIndex = 0; xIndex < board.gridWidth; xIndex++)
            {
                GridSpace currentSpace = gameBoard[yIndex][xIndex];
                if (xIndex + 2 < board.gridWidth)
                {
                    GridSpace oneToRight = gameBoard[yIndex][xIndex + 1];
                    GridSpace twoToRight = gameBoard[yIndex][xIndex + 2];
                    if (oneToRight != null && twoToRight != null && 
                        currentSpace.block != null &&
                        oneToRight.block != null &&
                        twoToRight.block != null &&
                        currentSpace.block.type == Block.Type.Recycleable
                        && oneToRight.block.type == Block.Type.Recycleable
                        && twoToRight.block.type == Block.Type.Recycleable)
                    {                        
                        toClear.Add(currentSpace);
                        toClear.Add(oneToRight);
                        toClear.Add(twoToRight);
                    }
                  

                }

                if (yIndex + 2 < board.gridHeight)
                {
                    GridSpace oneBelow = gameBoard[yIndex + 1][xIndex];
                    GridSpace twoBelow = gameBoard[yIndex + 2][xIndex];
                    if (oneBelow != null && twoBelow != null && 
                        currentSpace.block != null &&
                        oneBelow.block != null &&
                        twoBelow.block != null &&
                        currentSpace.block.type == Block.Type.Recycleable
                        && oneBelow.block.type == Block.Type.Recycleable
                        && twoBelow.block.type == Block.Type.Recycleable)
                    {
                        toClear.Add(currentSpace);
                        toClear.Add(oneBelow);
                        toClear.Add(twoBelow);
                        clearedRecycleable = true;
                    }
                }
                
                if (currentSpace.block != null && currentSpace.block.type == Block.Type.Nuclear)
                {
                    GridSpace left = null;
                    GridSpace right = null;
                    GridSpace up = null;
                    GridSpace down = null;
                    if (xIndex - 1 >= 0)
                    {
                        left = gameBoard[yIndex][xIndex - 1];
                    }
                    if (xIndex + 1 < board.gridWidth)
                    {
                        right = gameBoard[yIndex][xIndex + 1];
                    }
                    if (yIndex - 1 >= 0)
                    {
                        up = gameBoard[yIndex - 1][xIndex];
                    }
                    if (yIndex + 1 < board.gridHeight) {
                        down = gameBoard[yIndex + 1][xIndex];
                    }
                    float adjacentGarbage = 0;
                    if (left == null || (left.block != null && left.block.type == Block.Type.Garbage))
                    {
                        adjacentGarbage += 1;
                        if (left == null)
                        {
                            adjacentGarbage -= 0.5f;
                        }
                    }
                    if (right == null || (right.block != null && right.block.type == Block.Type.Garbage))
                    {
                        adjacentGarbage += 1;
                        if (right == null)
                        {
                            adjacentGarbage -= 0.5f;
                        }
                    }
                    if (up == null || (up.block != null && up.block.type == Block.Type.Garbage))
                    {
                        adjacentGarbage += 1;
                        if (up == null)
                        {
                            adjacentGarbage -= 0.5f;
                        }
                    }
                    if (down == null || (down.block != null && down.block.type == Block.Type.Garbage))
                    {
                        adjacentGarbage += 1;
                        if (down == null)
                        {
                            adjacentGarbage -= 0.5f;
                        }
                    }
                    if (adjacentGarbage >= 3)
                    {
                        if (left != null)
                        {
                            toClear.Add(left);
                        }
                        if (right != null)
                        {
                            toClear.Add(right);
                        }
                        if (up != null)
                        {
                            toClear.Add(up);
                        }
                        if (down != null)
                        {
                            toClear.Add(down);
                        }
                        if (xIndex - 1 >= 0 && yIndex -1 >= 0) // Top left
                        {
                            toClear.Add(gameBoard[yIndex - 1][xIndex - 1]);
                        }
                        if (xIndex + 1 < board.gridWidth && yIndex - 1 >= 0) // Top right
                        {
                            toClear.Add(gameBoard[yIndex - 1][xIndex + 1]);
                        }
                        if (xIndex - 1 >= 0 && yIndex + 1 < board.gridHeight) // Bottom left
                        {
                            toClear.Add(gameBoard[yIndex + 1][xIndex - 1]);
                        }
                        if (xIndex + 1 < board.gridWidth && yIndex + 1 < board.gridHeight) // Bottom right
                        {
                            toClear.Add(gameBoard[yIndex + 1][xIndex + 1]);
                        }
                        toClear.Add(currentSpace);
                        clearedNuclear = true;
                    }
                }

            }
        }

        foreach (GridSpace gs in toClear)
        {
            gs.isOccupied = false;
            // moveColumnDown(gs);
            if (gs.block != null)
            {
                if (gs.block.type == Block.Type.Nuclear)
                {
                    // activate particle system :)
                    board.explosion.transform.position = gs.block.transform.position;
                    board.explosion.GetComponent<ParticleSystem>().Emit (50);
                    //ParticleSystem.EmissionModule em = board.explosion.GetComponent<ParticleSystem>().emission;
                    //em.enabled = true;
                    board.score += 10;
                    
                }
                else if (gs.block.type == Block.Type.Recycleable)
                {
                    board.score += 1;

                }
                Destroy(gs.block.gameObject);
            }

            gs.block = null;
        }

        AudioSource audioSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        if (clearedNuclear)
        {
            audioSource.Stop();
            audioSource.clip = nuclearClearClip;
            audioSource.Play();
        }
        if (clearedRecycleable)
        {
            audioSource.Stop();
            audioSource.clip = recycleableClearClip;
            audioSource.Play();
        }

    }


    public void Spawn()
    {
        // Note: hardcoded to always spawn at place and one to right :o
        List<Block.Type[]> blockTypePairs = new List<Block.Type[]>();
        Block.Type[] twoGarbage = { Block.Type.Garbage, Block.Type.Garbage };
        Block.Type[] twoRecycleable = { Block.Type.Recycleable, Block.Type.Recycleable };
        Block.Type[] garbageRecycleable = { Block.Type.Garbage, Block.Type.Recycleable };
        Block.Type[] recycleableNuclear = { Block.Type.Recycleable, Block.Type.Nuclear };
        Block.Type[] garbageNuclear = { Block.Type.Garbage, Block.Type.Nuclear };
        blockTypePairs.Add(twoGarbage);
        blockTypePairs.Add(twoRecycleable);
        blockTypePairs.Add(garbageRecycleable);
        blockTypePairs.Add(recycleableNuclear);
        blockTypePairs.Add(garbageNuclear);
        float[] pairProbabilityCutoffs = {10/16f, 7/16f, 3/16f, 2/16f, 0/16f};

        // Check if it is valid to spawn:
        GridSpace spawnSpaceRight = spawnSpace.GetRightSpace();
        if ((spawnSpace && !spawnSpace.isOccupied) && (spawnSpaceRight && !spawnSpaceRight.isOccupied))
        {
            GamePiece newGamePiece =
                Instantiate(gamePiecePrefab, spawnSpace.transform.position, Quaternion.identity);
            Block blockOne =
                Instantiate(blockPrefab, spawnSpace.transform.position, Quaternion.identity);
            Block blockTwo =
                Instantiate(blockPrefab, spawnSpaceRight.transform.position, Quaternion.identity);

            // Set up blocks and game piece
            blockOne.transform.parent = newGamePiece.gameObject.transform;
            blockTwo.transform.parent = newGamePiece.gameObject.transform;
            blockOne.occupying = spawnSpace;
            blockTwo.occupying = spawnSpace.GetRightSpace();
            float rand = Random.Range(0f, 1f);
            int pairIndex = 0;
            for (int probIter = 0; probIter < pairProbabilityCutoffs.Length; probIter++)
            {
                if (rand > pairProbabilityCutoffs[probIter])
                {
                    pairIndex = probIter;
                    break;
                }
            }
            Block.Type[] blockTypePair = blockTypePairs[pairIndex];
            blockOne.type = blockTypePair[0];
            blockTwo.type = blockTypePair[1];
            blockOne.GetComponent<SpriteRenderer>().sprite = sprites[(int)blockOne.type];
            blockTwo.GetComponent<SpriteRenderer>().sprite = sprites[(int)blockTwo.type];
            blockOne.transform.localScale = new Vector3(0.021f, 0.021f, 1f);
            blockTwo.transform.localScale = new Vector3(0.021f, 0.021f, 1f);
            
            newGamePiece.blockOne = blockOne;
            newGamePiece.blockTwo = blockTwo;
   
            spawnSpace.block = blockOne;
            spawnSpaceRight.block = blockTwo;
            spawnSpace.isOccupied = true;
            spawnSpaceRight.isOccupied = true; 
            
        }

        else
        {
            
            // User lost!
        }

    }
    
}
