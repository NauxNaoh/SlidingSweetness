using UnityEngine;

namespace SlidingSweetness
{
    public class Cell : MonoBehaviour
    {
        private Block block;


        public bool HasBlock => block != null;
        public Block Block => block;



        public void SetBlockInCell(Block block) => this.block = block;

    }
}