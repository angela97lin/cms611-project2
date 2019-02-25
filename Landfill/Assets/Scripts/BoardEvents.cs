using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Note:
 
 Not sure if there is a better way to keep track of what to clear rather than having to loop through for each? :(
 
 */

    public class BoardEvents : MonoBehaviour
    {
        public Board board;

        public bool CheckForNuclearExplosions()
        {
            List<List<GridSpace>> gameBoard = board.getBoard();
            
            return false;
        }

      

        public void UpdateNuclearDegradation()
        {
            
        }
        
        
        public bool CheckGarbageSurroundingNuclear()
        {
            return false;
        }
        
        public void CheckAndUpdate()
        {
            
             
            
        }
        
        
        
    }
