using UnityEngine;


    public class Block : MonoBehaviour
    {
        public GridSpace occupying; 
        static float autoMoveTime = 0.0f;

        public enum Type
        {
            Garbage,
            Nuclear,
            Recycleable,
            Size
        }

        public Type type;

        void setType(Type setTypeTo, Sprite setSpriteTo)
        {
            type = setTypeTo;
            occupying.GetComponent<SpriteRenderer>().sprite = setSpriteTo;
        }

        
        void updatePosition(GridSpace nextOne)
        {
            occupying = nextOne;
        
            occupying.isOccupied = false;
            occupying.block = null;

            nextOne.block = this;        
            nextOne.isOccupied = true;
            this.transform.position = nextOne.transform.position;
        }


        public void Update()
        {
            if (Mathf.Abs(autoMoveTime - Time.time) > 0.6f)
            {
                bool movedDown = moveBlockDown();
                if (!movedDown)
                {
                    enabled = false;
                }
 
            }
            autoMoveTime = Time.time;

        }

        
        bool moveBlockDown()
        {
            GridSpace nextOne = occupying.GetDownSpace();

            while  ((nextOne != null && (!nextOne.isOccupied)))            
            {
                updatePosition(nextOne);
                return true;
            }
            return false;
        }
    }
