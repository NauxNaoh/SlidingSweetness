using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    [CreateAssetMenu(fileName = nameof(BlockSkinSetting), menuName = "SO/GameSkin/" + nameof(BlockSkinSetting))]
    public class BlockSkinSetting : ScriptableObject
    {
        public BlockData[] Blocks;

        public BlockSkinSetting()
        {
            Blocks = new BlockData[5]
            {
                new(BlockType.Block1),
                new(BlockType.Block2),
                new(BlockType.Block3),
                new(BlockType.Block4),
                new(BlockType.Block5),
            };
        }

        public Sprite GetBlockSprite(BlockType blockType, BlockSizeType blockSizeType)
        {
            var _block = Blocks.FirstOrDefault(x => x.BlockType == blockType);
            return _block.GetSpriteSkinSize(blockSizeType);
        }
    }

    [Serializable]
    public class BlockData
    {
        [LabelWidth(100)]
        public BlockType BlockType;

        [TableList(AlwaysExpanded = true, ShowIndexLabels = false, DrawScrollView = false)]
        public SkinData[] Skins;

        public BlockData(BlockType blockType)
        {
            BlockType = blockType;
            Skins = new SkinData[4]
            {
                new (BlockSizeType.Size1),
                new (BlockSizeType.Size2),
                new (BlockSizeType.Size3),
                new (BlockSizeType.Size4),
            };
        }

        public SkinData GetSkinSize(BlockSizeType blockSizeType)
        {
            return Skins.FirstOrDefault(x => x.SizeType == blockSizeType);
        }

        public Sprite GetSpriteSkinSize(BlockSizeType blockSizeType)
        {
            return Skins.FirstOrDefault(x => x.SizeType == blockSizeType).BlockSprite;
        }
    }

    [Serializable]
    public class SkinData
    {
        [VerticalGroup("Data/Info", Order = 1)]
        [LabelWidth(80)]
        public BlockSizeType SizeType;

        [HorizontalGroup("Data", 100, Order = 0)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        public Sprite BlockSprite;

        public SkinData(BlockSizeType sizeType)
        {
            SizeType = sizeType;
        }
    }
}