using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    [CreateAssetMenu(fileName = nameof(DifficultLevelSetting), menuName = "SO/LevelSetting/" + nameof(DifficultLevelSetting))]
    public class DifficultLevelSetting : ScriptableObject
    {
        [TableList]
        public DifficultLevel[] DifficultLevels;


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

            return DifficultLevels.Length > 0 ? DifficultLevels[0] : new(rate: 0, LvType.Easy, minSize: 3, maxSize: 7);
        }

        float GetTotalPercent() => DifficultLevels.Sum<DifficultLevel>(x => x.Rate);
    }

    [Serializable]
    public class DifficultLevel
    {
        [Range(0f, 100f, order = 0)]
        public float Rate;
        public LvType LevelType;
        [Range(3, 7)]
        public int MinSize;
        [Range(3, 7)]
        public int MaxSize;

        public DifficultLevel(float rate, LvType levelType, int minSize, int maxSize)
        {
            Rate = rate;
            LevelType = levelType;
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public int RandomTotalSizeBlock() => UnityEngine.Random.Range(MinSize, MaxSize + 1);
    }

    public enum LvType { Easy, Medium, Hard }

}