//using N.DB;
//using Newtonsoft.Json;
//using QuestSystem;
//using ScoreHistorySystem;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace FoodSlide
//{
//    [Serializable]
//    public class UserDB
//    {
//        [SerializeField] private string userID;
//        [SerializeField] private string userName;
//        [SerializeField] private int silveCoin;
//        [SerializeField] public int goldCoin;
//        [SerializeField] private RecipeMenuDB recipeMenu;
//        [SerializeField] private CharacterMenuDB characterMenu;
//        [SerializeField] private ScoreHistoryData scoreHistoryData;
//        [SerializeField] private BoosterMenuDB boosterMenu = new();
//        [SerializeField] private UserGameData userGameData = new();
//        [SerializeField] private TutorialDB tutorial = new();
//        [SerializeField] private GameQuestData gameQuestData; // set new manually


//        [SerializeField] public bool isCompletedFirstGame;



//        public int BestScore => scoreHistoryData != null ? scoreHistoryData.BestScore : 0;
//        public RecipeMenuDB RecipeMenu => recipeMenu;
//        public CharacterMenuDB CharacterMenu => characterMenu;
//        public ScoreHistoryData ScoreHistoryDB => scoreHistoryData;
//        public BoosterMenuDB BoosterMenu => boosterMenu;
//        public GameQuestData GameQuestDB { get => gameQuestData; set => gameQuestData = value; }
//        public UserGameData UserGameDB => userGameData;
//        public TutorialDB TutorialDB => tutorial;

//        [JsonConstructor]
//        public UserDB()
//        {
//            silveCoin = 50000;
//            goldCoin = 50000;
//            isCompletedFirstGame = false;


//            List<RecipeSkinModel> _lstRecipeMenu = new() { new(RecipeSkinType.RecipeSkin_1, true) };
//            recipeMenu = new RecipeMenuDB(RecipeSkinType.RecipeSkin_1, _lstRecipeMenu);

//            List<CharacterSkinModel> _lstCharacterSkins = new() { new(CharacterSkinType.Skin1, true) };
//            List<CharacterModel> _lstCharacterMenu = new() { new(CharacterType.Celeste, _lstCharacterSkins) };
//            characterMenu = new CharacterMenuDB(CharacterType.Celeste, _lstCharacterMenu);

//            scoreHistoryData = new(0);
//        }


//        //common
//        internal int GetCurrency(CurrencyType currencyType)
//        {
//            switch (currencyType)
//            {
//                case CurrencyType.SilveCoin:
//                    return silveCoin;
//                case CurrencyType.GoldCoin:
//                    return goldCoin;
//                default:
//                    return 0;
//            }
//        }
//        internal void AddCurrency(CurrencyType currencyType, int value)
//        {
//            if (value <= 0) return;
//            switch (currencyType)
//            {
//                case CurrencyType.SilveCoin:
//                    silveCoin += value;
//                    break;
//                case CurrencyType.GoldCoin:
//                    goldCoin += value;
//                    break;
//                default:
//                    return;
//            }

//            SaveItself();
//        }
//        internal void SpendCurrency(CurrencyType currencyType, int value)
//        {
//            if (value <= 0) return;
//            switch (currencyType)
//            {
//                case CurrencyType.SilveCoin:
//                    silveCoin = silveCoin - value > 0 ? silveCoin - value : 0;
//                    break;
//                case CurrencyType.GoldCoin:
//                    goldCoin = goldCoin - value > 0 ? goldCoin - value : 0;
//                    break;
//                default:
//                    return;
//            }

//            SaveItself();
//        }

//        internal void SetCompletedFirstGame()
//        {
//            isCompletedFirstGame = true;
//            SaveItself();
//        }


//        internal void UpdateScoreData(int score)
//        {
//            if (ScoreHistoryDB == null)
//            {
//                scoreHistoryData = new(score);
//            }
//            else
//            {
//                scoreHistoryData.UpdateScore(score);
//            }

//            SaveItself();
//        }

//        /// <summary>
//        /// Test only
//        /// </summary>
//        internal void UpdateScoreTestData(int year, int month, int day, int score)
//        {
//            if (ScoreHistoryDB == null)
//            {
//                scoreHistoryData = new(year, month, day, score);
//            }
//            else
//            {
//                scoreHistoryData.UpdateScore(year, month, day, score);
//            }

//            SaveItself();
//        }

//        internal void UpdateGameQuestData()
//        {
//            SaveItself();
//        }


//        //recipe
//        internal void UnlockRecipeSkin(RecipeSkinType type)
//        {
//            var _result = GetRecipeSkin(type);
//            _result.unlocked = true;
//            SaveItself();
//        }
//        internal bool SetSelectRecipeSkin(RecipeSkinType type)
//        {
//            if (recipeMenu.currentSkin == type) return false;
//            recipeMenu.currentSkin = type;
//            SaveItself();
//            return true;
//        }
//        internal RecipeSkinModel GetRecipeSkin(RecipeSkinType type)
//        {
//            var _result = recipeMenu.skinModels.Find(x => x.skinType == type);
//            if (_result != null) return _result;

//            _result = new RecipeSkinModel(type, false);
//            recipeMenu.skinModels.Add(_result);
//            SaveItself();
//            return _result;
//        }


//        //character
//        internal bool CheckUnlockDefault(CharacterType character)
//        {
//            var _result = GetCharacterSkin(character, CharacterSkinType.Skin1);
//            return _result.skinModel.unlocked;
//        }
//        internal void UnlockCharacterMenu(CharacterType character, CharacterSkinType skin)
//        {
//            var _result = GetCharacterSkin(character, skin);
//            _result.skinModel.unlocked = true;
//            SaveItself();
//        }
//        internal bool SetSelectCharacterSkin(CharacterType character, CharacterSkinType skin)
//        {
//            if (characterMenu.currentCharacter == character && characterMenu.currentSkin == skin)
//                return false;
//            characterMenu.currentCharacter = character;
//            characterMenu.currentSkin = skin;
//            SaveItself();
//            return true;
//        }
//        internal (CharacterModel charModel, CharacterSkinModel skinModel) GetCharacterSkin(CharacterType character, CharacterSkinType skin)
//        {
//            var _charModel = characterMenu.characterModels.Find(x => x.characterType == character);
//            if (_charModel == null)
//            {
//                var _skinModel = new CharacterSkinModel(skin, false);
//                _charModel = new CharacterModel(character, new() { _skinModel });
//                characterMenu.characterModels.Add(_charModel);
//                SaveItself();
//                return (_charModel, _skinModel);
//            }
//            else
//            {
//                var (isNew, skinModel) = _charModel.GetSkinModel(skin);
//                if (isNew) SaveItself();
//                return (_charModel, skinModel);
//            }
//        }


//        //booster
//        internal BoosterModel GetBooster(BoosterType type)
//        {
//            var _result = boosterMenu.boosterModels.Find(x => x.boosterType == type);
//            if (_result != null) return _result;

//            _result = new BoosterModel(type, 0);
//            boosterMenu.boosterModels.Add(_result);
//            SaveItself();
//            return _result;
//        }
//        internal void SpendBooster(BoosterType type, int value)
//        {
//            if (value <= 0) return;
//            var _booster = GetBooster(type);
//            _booster.stockAmount = _booster.stockAmount - value > 0 ? _booster.stockAmount - value : 0;
//            SaveItself();
//        }
//        internal void AddBooster(BoosterType type, int value)
//        {
//            if (value <= 0) return;
//            var _booster = GetBooster(type);
//            _booster.stockAmount = value > 0 ? _booster.stockAmount += value : _booster.stockAmount;
//            SaveItself();
//        }


//        //tutorial
//        internal TutorialModel GetTutorial(int tutorialId)
//        {
//            var _result = tutorial.tutorialModels.Find(x => x.tutorialId == tutorialId);
//            if (_result != null) return _result;

//            _result = new TutorialModel(tutorialId, false);
//            tutorial.tutorialModels.Add(_result);
//            SaveItself();
//            return _result;
//        }
//        internal TutorialModel GetCurrentTutorial()
//        {
//            return tutorial.tutorialModels.First(x => x.isCompleted == false);
//        }
//        internal void SetCompletedTutorial(int tutorialId)
//        {
//            var _tutorial = GetTutorial(tutorialId);
//            _tutorial.isCompleted = true;
//            SaveItself();
//        }
//        internal (bool isAllComplete, int curIdNotComplete) CheckAllTutorialCompleted()
//        {
//            for (int i = 0; i < tutorial.tutorialModels.Count; i++)
//            {
//                if (tutorial.tutorialModels[i].isCompleted == true) continue;

//                return (false, tutorial.tutorialModels[i].tutorialId);
//            }

//            return (true, -1);
//        }

//        internal bool IsTutorialCompleted
//        {
//            get {
//                for (int i = 0; i < tutorial.tutorialModels.Count; i++)
//                {
//                    if (!tutorial.tutorialModels[i].isCompleted) return false;
//                }
//                return true;
//            }
//        }


//        void SaveItself() => DBController.Instance.USER_DB = this;
//    }

//    #region Recipe Menu
//    [Serializable]
//    public class RecipeMenuDB
//    {
//        public RecipeSkinType currentSkin;
//        public List<RecipeSkinModel> skinModels;

//        public RecipeMenuDB(RecipeSkinType currentSkin, List<RecipeSkinModel> recipeSkinModels)
//        {
//            this.currentSkin = currentSkin;
//            skinModels = recipeSkinModels;
//        }
//    }

//    [Serializable]
//    public class RecipeSkinModel
//    {
//        public RecipeSkinType skinType;
//        public bool unlocked;

//        public RecipeSkinModel(RecipeSkinType skinType, bool unlocked)
//        {
//            this.skinType = skinType;
//            this.unlocked = unlocked;
//        }
//    }
//    #endregion

//    #region Character Menu
//    [Serializable]
//    public class CharacterMenuDB
//    {
//        public CharacterType currentCharacter;
//        public CharacterSkinType currentSkin;
//        public List<CharacterModel> characterModels;

//        public CharacterMenuDB(CharacterType currentCharacter, List<CharacterModel> characterModels)
//        {
//            this.currentCharacter = currentCharacter;
//            this.characterModels = characterModels;
//        }
//    }

//    [Serializable]
//    public class CharacterModel
//    {
//        public CharacterType characterType;
//        public List<CharacterSkinModel> skinModels;

//        public CharacterModel(CharacterType characterType, List<CharacterSkinModel> skinModels)
//        {
//            this.characterType = characterType;
//            this.skinModels = skinModels;
//        }

//        public (bool isNew, CharacterSkinModel skin) GetSkinModel(CharacterSkinType type)
//        {
//            var _result = skinModels.Find(x => x.skinType == type);
//            if (_result != null) return (false, _result);

//            _result = new CharacterSkinModel(type, false);
//            skinModels.Add(_result);
//            return (true, _result);
//        }
//    }

//    [Serializable]
//    public class CharacterSkinModel
//    {
//        public CharacterSkinType skinType;
//        public bool unlocked;

//        public CharacterSkinModel(CharacterSkinType skinType, bool unlocked)
//        {
//            this.skinType = skinType;
//            this.unlocked = unlocked;
//        }
//    }
//    #endregion

//    #region Booster Menu
//    [Serializable]
//    public class BoosterMenuDB
//    {
//        public List<BoosterModel> boosterModels;

//        public BoosterMenuDB()
//        {
//            boosterModels = new();
//        }
//    }

//    [Serializable]
//    public class BoosterModel
//    {
//        public BoosterType boosterType;
//        public int stockAmount;

//        public BoosterModel(BoosterType boosterType, int quantity)
//        {
//            this.boosterType = boosterType;
//            this.stockAmount = quantity;

//            this.stockAmount = 100;
//        }
//    }
//    #endregion

//    #region Tutorial
//    [Serializable]
//    public class TutorialDB
//    {
//        public List<TutorialModel> tutorialModels;

//        public TutorialDB()
//        {
//            this.tutorialModels = new();
//        }
//    }

//    [Serializable]
//    public class TutorialModel
//    {
//        public int tutorialId;
//        public bool isCompleted;

//        public TutorialModel(int tutorialId, bool isCompleted)
//        {
//            this.tutorialId = tutorialId;
//            this.isCompleted = isCompleted;
//        }
//    }
//    #endregion
//}