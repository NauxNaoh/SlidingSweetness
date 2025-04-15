using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    public class Block : MonoBehaviour
    {
        private BlockType blockType;

        [Required]
        [SerializeField] private Transform tfmDeviation;
        [Required]
        [SerializeField] private SpriteRenderer sprBlock;


        public void SetSpriteBlock(Sprite spr)
        {
            sprBlock.sprite = spr;
        }

        public void SetLocalDeviation(int size)
        {
            var _deviationX = (size - 1) * Board.cellSize / 2f;
            var _position = Vector2.zero;
            _position.x += _deviationX;
            tfmDeviation.transform.localPosition = _position;

            sprBlock.transform.localScale = Vector3.one * (Board.cellSize / 1.28f); //1 unit unity = 128 pixel png
        }

    }
}