using System;
using System.Collections.Generic;

namespace SlidingSweetness
{
    public static class LevelCalculator
    {
        public static List<BlockWillSpawn> TestGene(DifficultLevelSetting setting)
        {
            var _lvDifficult = setting.RandomDifficultLevel();
            var _totalSizeBlock = _lvDifficult.RandomTotalSizeBlock();


            var _spawnData = new List<BlockWillSpawn>();

            while (_totalSizeBlock > 0)
            {
                var _blockType = setting.RandomBlockType();
                var _blockSizeType = setting.RandomBlockSizeType();
                if (_blockSizeType.sizeInt > _totalSizeBlock)
                    _blockSizeType = setting.GetBlockSizeType(_totalSizeBlock);

                _totalSizeBlock -= _blockSizeType.sizeInt;
                _spawnData.Add(new(_blockType, _blockSizeType.sizeType, _blockSizeType.sizeInt));
            }

            return _spawnData;
        }

    }

    [Serializable]
    public class BlockWillSpawn
    {
        public BlockType BlockType;
        public BlockSizeType BlockSizeType;
        public int SizeInt;

        public BlockWillSpawn(BlockType blockType, BlockSizeType blockSizeType, int sizeInt)
        {
            BlockType = blockType;
            BlockSizeType = blockSizeType;
            SizeInt = sizeInt;
        }
    }
}