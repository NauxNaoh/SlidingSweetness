using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    [CreateAssetMenu(fileName = nameof(DifficultLevelSetting), menuName = "SO/LevelSetting/" + nameof(DifficultLevelSetting))]
    public class DifficultLevelSetting : ScriptableObject
    {
        public DifficultLevel[] DifficultLevels;

        public DifficultLevelSetting()
        {
            DifficultLevels = new DifficultLevel[3]
            {
                new(45, DifficultType.Easy, 3,4),
                new(40, DifficultType.Easy, 3,6),
                new(15, DifficultType.Easy, 4,7),
            };
        }

        public static BlockType[] cachedBlocks;
        public static BlockSizeType[] cachedBlockSizes;

        public BlockType RandomBlockType()
        {
            cachedBlocks ??= (BlockType[])Enum.GetValues(typeof(BlockType));
            return (BlockType)cachedBlocks.GetValue(UnityEngine.Random.Range(0, cachedBlocks.Length));
        }

        public (BlockSizeType sizeType, int sizeInt) RandomBlockSizeType()
        {
            cachedBlockSizes ??= (BlockSizeType[])Enum.GetValues(typeof(BlockSizeType));
            var _sizeType = (BlockSizeType)cachedBlockSizes.GetValue(UnityEngine.Random.Range(0, cachedBlockSizes.Length));
            return (_sizeType, (int)_sizeType + 1);
        }

        public (BlockSizeType sizeType, int sizeInt) GetBlockSizeType(int sizeInt)
        {
            var _sizeType = (BlockSizeType)cachedBlockSizes.GetValue(sizeInt - 1);
            return (_sizeType, sizeInt);
        }

        public DifficultLevel RandomDifficultLevel()
        {
            var _totalPercent = GetTotalPercent();
            var _randomPer = UnityEngine.Random.Range(0, _totalPercent);
            var _cumulative = 0f;

            foreach (var _level in DifficultLevels)
            {
                _cumulative += _level.Rate;
                if (_randomPer < _cumulative)
                    return _level;
            }

            return DifficultLevels.Length > 0 ? DifficultLevels[0] : new(rate: 0, DifficultType.Easy, minSize: 3, maxSize: 7);
        }

        float GetTotalPercent() => DifficultLevels.Sum<DifficultLevel>(x => x.Rate);
    }

    [Serializable]
    public class DifficultLevel
    {
        [TabGroup("Level")]
        [Range(0f, 100f)]
        public float Rate;

        [TabGroup("Level")]
        public DifficultType LevelType;

        [TabGroup("Level")]
        [Range(1, 7)]
        public int MinSize;

        [TabGroup("Level")]
        [Range(1, 7)]
        public int MaxSize;


        [TabGroup("Size")]
        public DifficultSize[] difficultSizes;


        public DifficultLevel(float rate, DifficultType levelType, int minSize, int maxSize)
        {
            Rate = rate;
            LevelType = levelType;
            MinSize = minSize;
            MaxSize = maxSize;

            difficultSizes = new DifficultSize[4]
            {
                new(BlockSizeType.Size1, 30f),
                new(BlockSizeType.Size2, 30f),
                new(BlockSizeType.Size3, 30f),
                new(BlockSizeType.Size4, 10f),
            };
        }

        public int RandomTotalSizeBlock() => UnityEngine.Random.Range(MinSize, MaxSize + 1);
    }

    [Serializable]
    public class DifficultSize
    {
        public BlockSizeType SizeType;

        [Range(0f, 100f)]
        public float Rate;

        public DifficultSize(BlockSizeType sizeType, float rate)
        {
            SizeType = sizeType;
            Rate = rate;
        }
    }

    public enum DifficultType { Easy, Medium, Hard }

}