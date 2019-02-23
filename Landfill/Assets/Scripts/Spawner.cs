using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        // Used to spawn pawn fawn blocks!
        public GamePiece gamePiecePrefab;
        public Block blockPrefab;
        public GridSpace spawnSpace;
        
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

                blockOne.transform.parent = newGamePiece.gameObject.transform;
                blockTwo.transform.parent = newGamePiece.gameObject.transform;

                newGamePiece.blockOne = blockOne;
                newGamePiece.blockTwo = blockTwo;
        
                blockOne.occupying = spawnSpace;
                blockTwo.occupying = spawnSpace.GetRightSpace();
                spawnSpace.isOccupied = true;
                spawnSpace.GetRightSpace().isOccupied = true; 
            }

            else
            {
                
                // User lost!
            }

        }
        
    }
