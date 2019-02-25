using System.Collections;
using System.Collections.Generic;
using UnityEditor.StyleSheets;
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
        
        // Note: this list of sprites must match up to Block.Type enum!
        public List<Sprite> sprites;


        public Block.Type getRandomType()
        {
            int i = Random.Range(0, (int)Block.Type.Size);
            return (Block.Type) i;
        }
        
        

        // check if three in a row ==> clear 
        public void CheckForClear()
        {
            HashSet<GridSpace> toClear = new HashSet<GridSpace>();
            List<List<GridSpace>> gameBoard = board.getBoard();
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
                            print("Horizontal");
                            
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
                            print(oneBelow.block);
                            print("vertical");
                            print(currentSpace.x + "," + currentSpace.y);
                            toClear.Add(currentSpace);
                            toClear.Add(oneBelow);
                            toClear.Add(twoBelow);
                        }
                    }
                    
                    

                }
            }

            foreach (GridSpace gs in toClear)
            {
                gs.isOccupied = false;
                // moveColumnDown(gs);                
                Destroy(gs.block.gameObject);

                gs.block = null;
            }
            
        }


        public void moveColumnDown(GridSpace source)
        {
            // move down connected components after a clear
            GridSpace current = source;
            GridSpace currentUp = current.GetUpSpace();
            while (currentUp.isOccupied)
            {
                current.block = currentUp.block;
                current.isOccupied = true;
                currentUp.isOccupied = false;
                current = currentUp;
                currentUp = current.GetUpSpace();
            }
            
  

        }
        
        public void Spawn()
        {
            // Note: hardcoded to always spawn at place and one to right :o
            
            // Check if it is valid to spawn:
            GridSpace spawnSpaceRight = spawnSpace.GetRightSpace();
            if ((spawnSpace && !spawnSpace.isOccupied) && (spawnSpaceRight && !spawnSpaceRight.isOccupied))
            {
                GamePiece newGamePiece =
                    Instantiate(gamePiecePrefab, spawnSpace.transform.position, Quaternion.identity);
                Block blockOne =
                    Instantiate(blockPrefab, spawnSpace.transform.position + new Vector3(0.15f, 0.0f, 0.0f), Quaternion.identity);
                Block blockTwo =
                    Instantiate(blockPrefab, spawnSpace.transform.position, Quaternion.identity);

                // Set up blocks and game piece
                blockOne.transform.parent = newGamePiece.gameObject.transform;
                blockTwo.transform.parent = newGamePiece.gameObject.transform;
                blockOne.occupying = spawnSpace;
                blockTwo.occupying = spawnSpace.GetRightSpace();
                blockOne.type = getRandomType();
                blockTwo.type = getRandomType();
                // to do: make sure trash and nuclear are not together
                blockOne.GetComponent<SpriteRenderer>().sprite = sprites[(int)blockOne.type];
                blockTwo.GetComponent<SpriteRenderer>().sprite = sprites[(int)blockTwo.type];
                blockOne.transform.localScale = new Vector3(0.026f, 0.026f, 1f);
                blockTwo.transform.localScale = new Vector3(0.026f, 0.026f, 1f);
                
                newGamePiece.blockOne = blockOne;
                newGamePiece.blockTwo = blockTwo;
        
                spawnSpace.isOccupied = true;
                spawnSpace.block = blockOne;
                spawnSpaceRight.block = blockTwo;
                spawnSpace.GetRightSpace().isOccupied = true; 
                
            }

            else
            {
                
                // User lost!
            }

        }
        
    }
