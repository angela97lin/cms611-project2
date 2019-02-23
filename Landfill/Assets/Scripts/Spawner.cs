using System.Collections;
using System.Collections.Generic;
using UnityEditor.StyleSheets;
using UnityEngine;

    public class Spawner : MonoBehaviour
    {
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
                
                newGamePiece.blockOne = blockOne;
                newGamePiece.blockTwo = blockTwo;
        

                spawnSpace.isOccupied = true;
                spawnSpace.GetRightSpace().isOccupied = true; 
                
                
                
                
            }

            else
            {
                
                // User lost!
            }

        }
        
    }
