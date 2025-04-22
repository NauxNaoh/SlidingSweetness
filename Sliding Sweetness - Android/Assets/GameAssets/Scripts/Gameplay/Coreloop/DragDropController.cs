using System.Threading;
using Cysharp.Threading.Tasks;
using N.Patterns;
using N.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    public class DragDropController : Singleton<DragDropController>
    {
        [Required]
        [SerializeField] private SpriteRenderer sprFadeOfBlock;

        private bool initialized;
        private Block blockHandling;
        private Vector2 rangeDragX;
        private Vector3 mouseStartPosition;
        private Vector3 blockStartPosition;
        private bool isDragging;


        public async UniTask Initialize(CancellationToken token)
        {
            initialized = false;
            SetActiveObject(sprFadeOfBlock.gameObject, false);
            sprFadeOfBlock.SetAlpha(0.3f);
            sprFadeOfBlock.transform.localScale = Board.ScaleFactor;
            await UniTask.Yield(token);
            initialized = true;
        }

        void SetPosition(Transform tfm, Vector3 position) => tfm.position = position;
        void SetActiveObject(GameObject gobj, bool status) => CommonExtensions.SetActiveObject(gobj, status);
        void SetSpriteSprRenderer(SpriteRenderer spr, Sprite sprite) => spr.sprite = sprite;

        public void OnStartDragBlock(Block block)
        {
            if (blockHandling != null || blockHandling == block) return;
            isDragging = true;
            blockHandling = block;

            rangeDragX = Board.GetRangeXCanDrag(blockHandling);
            mouseStartPosition = CommonExtensions.GetMousePosition();
            blockStartPosition = blockHandling.Position;

            Vector3 _fadePos = blockStartPosition;
            _fadePos.x += blockHandling.GetPositionDeviation();
            SetPosition(sprFadeOfBlock.transform, _fadePos);
            SetSpriteSprRenderer(sprFadeOfBlock, blockHandling.SpriteBlock);
            SetActiveObject(sprFadeOfBlock.gameObject, true);
        }

        public void OnDragBlock()
        {
            if (!isDragging) return;
            var _mousePos = blockStartPosition + (CommonExtensions.GetMousePosition() - mouseStartPosition);
            var _newPos = new Vector3(Mathf.Clamp(_mousePos.x, rangeDragX.x, rangeDragX.y), blockStartPosition.y, blockStartPosition.z);

            blockHandling.transform.position = _newPos;
            //BoardController.Instance.SnapVirtualPosition(ingredientPick, _newPos);
        }
        public void OnEndDragBlock()
        {
            isDragging = false;
            //if (ingredientPick != null)
            //{
            //    BoardController.Instance.SnapToNewPosition(ingredientPick, ingredientPick.Position);
            //}

            blockHandling = null;
            SetActiveObject(sprFadeOfBlock.gameObject, false);
            //BoardController.Instance.ClearAllVirtualPosition();
        }

    }
}