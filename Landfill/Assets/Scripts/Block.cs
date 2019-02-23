using UnityEngine;


    public class Block : MonoBehaviour
    {
        public GridSpace occupying;
        public Sprite mySprite;
 

        public enum Type
        {
            Nuclear,
            Garbage,
            Recycleable,
            Size
        }

        public Type type;
        public int turnsUntilRecycleableChangesToNuclear;
        // Create a function s.t. 
        // Whenever a new block is placed, all current recycleable
        // next to nuclear will counter down.    

        void setType(Type setTypeTo)
        {
            type = setTypeTo;
            occupying.GetComponent<SpriteRenderer>().sprite = mySprite;

        }

    }
