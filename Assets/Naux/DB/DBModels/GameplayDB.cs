//using N.DB;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace FoodSlide
//{
//    [Serializable]

//    public class GameplayDB
//    {
//        [SerializeField] private bool endGame;
//        [SerializeField] private bool wasRevive;
//        [SerializeField] private int currentScore;
//        [SerializeField] private int overheatCanUse;
//        [SerializeField] private int godSlicewCanUse;
//        [SerializeField] private int frozenCanUse;
//        [SerializeField] private List<IngredientBoardDB> boardDatas = new();


//        public bool IsEndGame => endGame;
//        public bool WasRevive => wasRevive;
//        public int CurrentScore => currentScore;
//        public int OverheatCanUse => overheatCanUse > 0 ? overheatCanUse : 9999999;
//        public int GodSlicewCanUse => godSlicewCanUse > 0 ? godSlicewCanUse : 9999999;
//        public int FrozenCanUse => frozenCanUse > 0 ? frozenCanUse : 9999999;
//        public List<IngredientBoardDB> BoardDatas => boardDatas;

//        public GameplayDB()
//        {
//            endGame = true;
//        }

//        public void ResetForNewGameplay()
//        {
//            endGame = false;
//            wasRevive = false;
//            currentScore = 0;
//            overheatCanUse = DBController.Instance.FIREBASE_CONFIG_DB.LimitBoosterUsed;
//            godSlicewCanUse = DBController.Instance.FIREBASE_CONFIG_DB.LimitBoosterUsed;
//            frozenCanUse = DBController.Instance.FIREBASE_CONFIG_DB.LimitBoosterUsed;
//            boardDatas.Clear();
//            SaveItself();
//        }
//        internal void SetEndgame()
//        {
//            endGame = true;
//            SaveItself();
//        }
//        internal void SetRevived()
//        {
//            wasRevive = true;
//            SaveItself();
//        }

//        internal void AddCurrentScore(int score)
//        {
//            currentScore += score;
//            SaveItself();
//        }

//        public void SpenUseBooster(BoosterType type)
//        {
//            switch (type)
//            {
//                case BoosterType.Overheat:
//                    overheatCanUse--;
//                    break;
//                case BoosterType.GodSlice:
//                    godSlicewCanUse--;
//                    break;
//                case BoosterType.Frozen:
//                    frozenCanUse--;
//                    break;
//                default: return;
//            }
//            SaveItself();
//        }

//        public void SaveBoard(List<Ingredient> lstIngredient)
//        {
//            boardDatas.Clear();

//            for (int i = 0, _count = lstIngredient.Count; i < _count; i++)
//            {
//                IngredientBoardDB _ingreDB = null;
//                var _ingre = lstIngredient[i];
//                if (_ingre.BlockType == BlockType.Booster)
//                {
//                    _ingreDB = new(_ingre.BlockType, _ingre.BoosterType, _ingre.GridPos, _ingre.Size);
//                }
//                else
//                {
//                    _ingreDB = new(_ingre.BlockType, _ingre.IngredientType, _ingre.GridPos, _ingre.Size);
//                }
//                if (_ingreDB != null)
//                    boardDatas.Add(_ingreDB);
//            }
//            SaveItself();
//        }

//        void SaveItself() => DBController.Instance.GAMEPLAY_DB = this;
//    }

//    [Serializable]
//    public class IngredientBoardDB
//    {
//        public BlockType blockType;
//        public IngredientType ingredientType;
//        public BoosterType boosterType;
//        public Vector2Int gridPos;
//        public int size;

//        public IngredientBoardDB(BlockType blockType, IngredientType ingredientType, Vector2Int gridPos, int size)
//        {
//            this.blockType = blockType;
//            this.ingredientType = ingredientType;
//            this.gridPos = gridPos;
//            this.size = size;
//        }

//        public IngredientBoardDB(BlockType blockType, BoosterType boosterType, Vector2Int gridPos, int size)
//        {
//            this.blockType = blockType;
//            this.boosterType = boosterType;
//            this.gridPos = gridPos;
//            this.size = size;
//        }
//    }
//}