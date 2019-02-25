using UnityEngine;


    public class Block : MonoBehaviour
    {
        public GridSpace occupying; 

        public enum Type
        {
            Garbage,
            Nuclear,
            Recycleable,
            Size
        }

        public Type type;
        public int turnsUntilRecycleableChangesToNuclear;
        // Create a function s.t. 
        // Whenever a new block is placed, all current recycleable
        // next to nuclear will counter down.    

        void setType(Type setTypeTo, Sprite setSpriteTo)
        {
            type = setTypeTo;
            occupying.GetComponent<SpriteRenderer>().sprite = setSpriteTo;
        }

    }
