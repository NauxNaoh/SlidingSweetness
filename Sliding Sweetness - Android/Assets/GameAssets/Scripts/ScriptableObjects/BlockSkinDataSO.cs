using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    [CreateAssetMenu(fileName = nameof(BlockSkinDataSO), menuName = "SO/GameSkin/" + nameof(BlockSkinDataSO))]
    public class BlockSkinDataSO : ScriptableObject
    {
        public List<BlockData> Blocks;

        public BlockSkinDataSO()
        {
            Blocks = new()
            {
                new(BlockType.Block1),
                new(BlockType.Block2),
                new(BlockType.Block3),
                new(BlockType.Block4),
                new(BlockType.Block5),
            };
        }
    }

    [Serializable]
    public class BlockData
    {
        [LabelWidth(100)]
        public BlockType BlockType;

        [TableList(AlwaysExpanded = true, ShowIndexLabels = false, DrawScrollView = false)]
        public List<SkinData> Skins;

        public BlockData(BlockType blockType)
        {
            BlockType = blockType;
            Skins = new()
            {
                new (BlockSizeType.Size1),
                new (BlockSizeType.Size2),
                new (BlockSizeType.Size3),
                new (BlockSizeType.Size4),
            };
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