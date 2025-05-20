using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SlidingSweetness
{
    public static class LevelCalculator
    {


        public static List<BlockWillSpawn> GenBlockCalculator(DifficultLevelSetting setting)
        {
            var _blocks = GetBlocks(setting);
            return AddEmptySpace(_blocks);
        }


        static List<BlockWillSpawn> GetBlocks(DifficultLevelSetting setting)
        {
            var _spawnData = new List<BlockWillSpawn>();
            var _lvDifficult = setting.RandomDifficultLevel();
            var _totalSizeBlock = _lvDifficult.RandomTotalSizeBlock();

            BlockType _blockType = BlockType.Block1;
            while (_totalSizeBlock > 0)
            {
                _blockType = setting.RandomBlockType();
                var _blockSizeType = setting.RandomBlockSizeType();
                if (_blockSizeType.sizeInt > _totalSizeBlock)
                    _blockSizeType = setting.GetBlockSizeType(_totalSizeBlock);

                _totalSizeBlock -= _blockSizeType.sizeInt;
                _spawnData.Add(new(_blockType, _blockSizeType.sizeType, _blockSizeType.sizeInt));
            }
            return _spawnData;
        }

        static List<BlockWillSpawn> AddEmptySpace(List<BlockWillSpawn> blocks)
        {
            var _grid = Vector2Int.zero;
            var _totalSize = blocks.Sum(x => x.SizeInt);
            var _empty = Board.boardSize.x - _totalSize;

            var _xPos = 0;
            var _randSpace = 0;
            for (var i = 0; i < blocks.Count; i++)
            {
                _randSpace = _empty > 0 ? UnityEngine.Random.Range(0, _empty + 1) : 0;
                _empty = _empty - _randSpace > 0 ? _empty - _randSpace : 0;

                _xPos += _randSpace;

                blocks[i].GridPos = new(_xPos, 0);
                _xPos += blocks[i].SizeInt;
            }

            return blocks;
        }
    }

    [Serializable]
    public class BlockWillSpawn
    {
        public BlockType BlockType;
        public BlockSizeType BlockSizeType;
        public int SizeInt;
        public Vector2Int GridPos;

        public BlockWillSpawn(BlockType blockType, BlockSizeType blockSizeType, int sizeInt)
        {
            BlockType = blockType;
            BlockSizeType = blockSizeType;
            SizeInt = sizeInt;
        }
    }
}