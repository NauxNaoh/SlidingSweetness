using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Block : MonoBehaviour
    {
        [Required]
        [SerializeField] private Transform tfmDeviation;
        [Required]
        [SerializeField] private SpriteRenderer sprBlock;

        private BoxCollider2D boxCollider2D;
        private BlockType blockType;
        private BlockSizeType blockSizeType;
        private int blockSize;
        private Vector2Int gridPlace;


        public Vector2Int GridPlace => gridPlace;
        public Vector3 Position => transform.position;
        public int Size => blockSize;

        [NonSerialized] public Sprite SpriteBlock;

        public void Initialize(BlockWillSpawn blockWillSpawn)
        {
            blockType = blockWillSpawn.BlockType;
            blockSizeType = blockWillSpawn.BlockSizeType;
            blockSize = blockWillSpawn.SizeInt;
            gridPlace = blockWillSpawn.GridPos;
            SpriteBlock = SOContainer.BlockSkin.GetBlockSprite(blockType, blockSizeType);

            SetBoxCollider2D();
            SetSpriteBlock(SpriteBlock);
            SetLocalDeviation(blockSize);
        }

        public float GetPositionDeviation() => GetPositionDeviation(blockSize);
        public float GetPositionDeviation(int size) => (size - 1) * Board.cellSize * 0.5f;
        public void SetSpriteBlock(Sprite spr) => sprBlock.sprite = spr;

        void SetBoxCollider2D()
        {
            if (boxCollider2D == null)
                boxCollider2D = GetComponent<BoxCollider2D>();

            boxCollider2D.size = new Vector2(Board.cellSize * blockSize, Board.cellSize);
            boxCollider2D.offset = new(GetPositionDeviation(), 0);
        }
        public void SetLocalDeviation(int size)
        {
            var _deviationX = GetPositionDeviation(size);
            var _position = Vector2.zero;
            _position.x += _deviationX;
            tfmDeviation.transform.localPosition = _position;
            //1 unit unity = 128 pixel png so need /1.28f
            sprBlock.transform.localScale = Board.ScaleFactor;
        }

        private void OnMouseDown()
        {
            //if (GameplayController.IsUserTurn())
            DragDropController.Instance.OnStartDragBlock(this);
        }

        private void OnMouseDrag()
        {
            DragDropController.Instance.OnDragBlock();
        }

        private void OnMouseUp()
        {
            DragDropController.Instance.OnEndDragBlock();
        }


    }
}