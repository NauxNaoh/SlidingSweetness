//using System;
//using System.Collections.Generic;
//using EffectSystem;

//namespace FoodSlide
//{
//    [Serializable]
//    public class ComboX 
//    {
//        public ComboXType comboXType;
//        public int Amount;
//    }

//    [Serializable]
//    public class BoosterUsed
//    {
//        public BoosterType boosterType;
//        public int Amount;
//    }

//    [Serializable]
//    public class DailyUserData 
//    {
//        public string DailyTime;
//        private DateTime dateTime = DateTime.UtcNow;
//        public int TotalGame = 0;
//        public int TotalWatchAd = 0;
//        public int TotalCustomer = 0;
//        public int TotalBoosterUsed = 0;
//        public int TotalSkillUsed = 0;
//        public int TotalCombo = 0;
//        public bool Shared = false;
//        public bool IsSwitchedRecipe = false;
//        public int TotalExchangeCurrency = 0;
//        public List<ComboX> ListComboX = new();
//        public List<BoosterUsed> ListBoosterUsed = new();

//        public void CheckDailyReset() 
//        {
//            dateTime = DateTime.UtcNow;
//            if (!string.IsNullOrEmpty(DailyTime) && DateTime.TryParse(DailyTime, out DateTime result))
//            {
//                dateTime = result;
//            }
//            if (dateTime.Day < DateTime.UtcNow.Day) 
//            {
//                DailyTime = DateTime.UtcNow.ToString();
//                TotalGame = 0;
//                TotalWatchAd = 0;
//                TotalCustomer = 0;
//                TotalBoosterUsed = 0;
//                ListComboX.Clear();
//                ListBoosterUsed.Clear();
//                Shared = false;
//                IsSwitchedRecipe = false;
//                TotalExchangeCurrency = 0;
//            }
//        }

//        public void AddTotalGame() => TotalGame++;
//        public void AddWatchAd() => TotalWatchAd++;
//        public void AddTotalCustomer() => TotalCustomer++;
//        public void AddTotalSkillUsed() => TotalSkillUsed++;
//        public void AddTotalExchangeCurrency() => TotalExchangeCurrency++;
//        public void ShareScore() => Shared = true;
//        public void SwitchRecipe() {
//            if (!IsSwitchedRecipe) 
//            {
//                IsSwitchedRecipe = true;
//            }
//        }
//        public void AddBoosterUsed(BoosterType boosterType) 
//        {
//            TotalBoosterUsed++;
//            int index = ListBoosterUsed.FindIndex(item => item.boosterType == boosterType);
//            if (index > -1) ListBoosterUsed[index].Amount++;
//            else ListBoosterUsed.Add(new BoosterUsed {
//                boosterType = boosterType,
//                Amount = 1
//            });
//        }

//        public void AddComboX(ComboXType comboXType) 
//        {
//            TotalCombo++;
//            int index = ListComboX.FindIndex(item => item.comboXType == comboXType);
//            if (index > -1) ListComboX[index].Amount++;
//            else ListComboX.Add(new ComboX {
//                comboXType = comboXType,
//                Amount = 1
//            });
//        }
//    }

//    [Serializable]
//    public class UserGameData
//    {
//        public string LoginTime;
//        public string ReLoginTime;
//        public int TotalGame = 0;
//        public int TotalWatchAd = 0;
//        public int TotalCustomer = 0;
//        public int TotalBoosterUsed = 0;
//        public int TotalCombo = 0;
//        public int TotalSkinPurchased = 0;
//        public int TotalSkillUsed = 0;
//        public int TotalCharacter = 1;
//        public int TotalLoginStreakDay = 1;
//        public int TotalRecipeUnlocked = 1;
//        public int TotalQuestCompleted = 0;
//        public List<ComboX> ListComboX = new();
//        public List<BoosterUsed> ListBoosterUsed = new();
//        public DailyUserData dailyUserData = new();
//        private DateTime LoginDate = DateTime.UtcNow;
//        private DateTime ReLoginDate = DateTime.UtcNow;

//        public UserGameData() 
//        {
//            LoginTime = DateTime.UtcNow.ToString();
//            ReLoginTime = LoginTime;
//        }

//        public void CheckDailyReset() 
//        {
//            dailyUserData?.CheckDailyReset();
//            ReLogin();
//        }

//        public int GetTotalComboX(ComboXType comboXType) 
//        {
//            int index = ListComboX.FindIndex(item => item.comboXType == comboXType);
//            if (index > -1) return ListComboX[index].Amount;
//            return 0;
//        }

//        public int GetDailyTotalComboX(ComboXType comboXType) 
//        {
//            int index = dailyUserData.ListComboX.FindIndex(item => item.comboXType == comboXType);
//            if (index > -1) return dailyUserData.ListComboX[index].Amount;
//            return 0;
//        }

//        public void AddTotalGame() 
//        {
//            TotalGame++;
//            dailyUserData.AddTotalGame();
//        }

//        public void AddWatchAd() 
//        {
//            TotalWatchAd++;
//            dailyUserData.AddWatchAd();
//        }

//        public void AddTotalCustomer() 
//        {
//            TotalCustomer++;
//            dailyUserData.AddTotalCustomer();
//        }

//        public void AddTotalSkillUsed() 
//        {
//            TotalSkillUsed++;
//            dailyUserData.AddTotalSkillUsed();
//        }

//        public void AddTotalSkinPurchased() => TotalSkinPurchased++;
//        public void AddTotalCharacterUnlocked() => TotalCharacter++;
//        public void AddTotalQuestCompleted() => TotalQuestCompleted++;
//        public void AddTotalExchangeCurrency() => dailyUserData.AddTotalExchangeCurrency();
//        public void AddTotalRecipeUnlocked() => TotalRecipeUnlocked++;
//        public void SwitchRecipe() => dailyUserData.SwitchRecipe();

//        public void AddBoosterUsed(BoosterType boosterType) 
//        {
//            TotalBoosterUsed++;
//            int index = ListBoosterUsed.FindIndex(item => item.boosterType == boosterType);
//            if (index > -1) ListBoosterUsed[index].Amount++;
//            else ListBoosterUsed.Add(new BoosterUsed {
//                boosterType = boosterType,
//                Amount = 1
//            });

//            dailyUserData.AddBoosterUsed(boosterType);
//        }

//        public void AddComboX(int value) 
//        {
//            ComboXType comboXType = (ComboXType)(value - 1);
//            AddComboX(comboXType);
//        }

//        public void AddComboX(ComboXType comboXType) 
//        {
//            TotalCombo++;
//            int index = ListComboX.FindIndex(item => item.comboXType == comboXType);
//            if (index > -1) ListComboX[index].Amount++;
//            else ListComboX.Add(new ComboX {
//                comboXType = comboXType,
//                Amount = 1
//            });

//            dailyUserData.AddComboX(comboXType);
//        }

//        public void ReLogin() 
//        {
//            ReLoginDate = DateTime.UtcNow;
//            LoginDate = DateTime.UtcNow;
//            if (!string.IsNullOrEmpty(ReLoginTime) && DateTime.TryParse(ReLoginTime, out DateTime result))
//            {
//                ReLoginDate = result;
//            }
//            if (!string.IsNullOrEmpty(LoginTime) && DateTime.TryParse(LoginTime, out DateTime loginDate))
//            {
//                LoginDate = loginDate;
//            }

//            if (ReLoginDate > LoginDate) 
//            {
//                if (DateTime.UtcNow.Day - ReLoginDate.Day == 1) 
//                {
//                    TotalLoginStreakDay++;
//                } else if (DateTime.UtcNow.Day - ReLoginDate.Day > 1) {
//                    TotalLoginStreakDay = 1;
//                }
//                ReLoginDate = DateTime.UtcNow;
//            } else if (ReLoginDate.Day == LoginDate.Day && DateTime.UtcNow > LoginDate)
//            {
//                ReLoginDate = DateTime.UtcNow;
//            }
//        }
//    }
//}