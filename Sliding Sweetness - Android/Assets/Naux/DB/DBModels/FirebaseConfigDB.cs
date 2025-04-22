//using System;
//using System.Collections.Generic;
//using FirebaseSystem;
//using FoodSlide;
//using N.DB;
//using Newtonsoft.Json;
//using UnityEngine;

//namespace N.DB
//{
//    public enum AdBreakType { None = 0, Time, TotalGame }
//    public enum FirebaseGameConfigType { None, Config_1, Config_2, Config_3 }

//    [Serializable]
//    public class DailyEvent 
//    {
//        public string Key;
//        public int Value;
//        public int Day;
//    }

//    [Serializable]
//    public class FirebaseConfigDB 
//    {
//        [Serializable]
//        public class AdBreakConfig 
//        {
//            public AdBreakType adBreakType = AdBreakType.Time;
//            public int FirstAdBreakTime = 30;
//            public int AdBreakTime = 60;
//            public CurrencyType currencyCostType = CurrencyType.GoldCoin;
//            public int SkipCost = 60;
//            public CurrencyType currencyBonusType = CurrencyType.SilveCoin;
//            public int AdBreakBonus = 50;
//            public int CountDownTime = 10;
//            public int BannerAdTime = 30;

//            public bool isActive => adBreakType != AdBreakType.None;
//        }

//        [Serializable]
//        public class AdSessionTimeStore 
//        {
//            public float BannerAdSessionTime {
//                get 
//                {
//                    BannerSessionDate = DateTime.MinValue;
//                    if (!string.IsNullOrEmpty(BannerSessionDateTime) && DateTime.TryParse(BannerSessionDateTime, out var result)) 
//                    {
//                        BannerSessionDate = result;
//                    }
//                    return BannerSessionDate != DateTime.MinValue ? Mathf.Min(0, (float)(DateTime.UtcNow - BannerSessionDate).TotalSeconds) : 0;
//                }
//            }
            
//            public float InterstialAdSessionTime 
//            {
//                get 
//                {
//                    InterstialSessionDate = DateTime.MinValue;
//                    if (!string.IsNullOrEmpty(InterstialSessionDateTime) && DateTime.TryParse(InterstialSessionDateTime, out var result)) 
//                    {
//                        InterstialSessionDate = result;
//                    }
//                    return InterstialSessionDate != DateTime.MinValue ? Mathf.Min(0, (float)(DateTime.UtcNow - InterstialSessionDate).TotalSeconds) : 0;
//                }
//            }

//            public string InterstialSessionDateTime;
//            public string BannerSessionDateTime;
//            private DateTime InterstialSessionDate = DateTime.MinValue;
//            private DateTime BannerSessionDate = DateTime.MinValue;

//            public void SetInterstialAdSessionTime(DateTime dateTime) 
//            {
//                InterstialSessionDate = dateTime;
//                InterstialSessionDateTime = dateTime.ToString();
//            }

//            public void SetBannerAdSessionTime(DateTime dateTime) 
//            {
//                BannerSessionDate = dateTime;
//                BannerSessionDateTime = dateTime.ToString();
//            }
//        }

//        [Serializable]
//        public class ActiveGameConfig 
//        {
//            public FirebaseGameConfigType firebaseGameConfigType;
//        }

//        [Serializable]
//        public class FirebaseGameConfig
//        {
//            public List<int> ListScore;
//        }

//        [Serializable]
//        public class BoosterLimitConfig
//        {
//            public int LimitBoosterUsed;
//        }

//        [SerializeField] public List<string> ListKeyOnce = new();
//        [SerializeField] public List<DailyEvent> ListDailyEvent = new();
//        [SerializeField] public AdSessionTimeStore adSessionTimeStore = new();
//        [SerializeField] public AdBreakConfig adBreakConfig = new();
//        [SerializeField] public ActiveGameConfig activeGameConfig = new();
//        [SerializeField] public FirebaseGameConfig firebaseGameConfig = new();
//        [SerializeField] public string InfiniteTimerDate;
//        [SerializeField] public string LoginDate;
//        [SerializeField] public int LoginDay;
//        [SerializeField] public int ReLoginTimes;
//        [SerializeField] public int AdTotalGame;
//        [SerializeField] public int TotalAdBreak = 0;
//        [SerializeField] public int LimitBoosterUsed = 3;
        
//        public object GetFirebaseGameConfig() 
//        {
//            if (activeGameConfig.firebaseGameConfigType != FirebaseGameConfigType.None) 
//            {
//                if (activeGameConfig.firebaseGameConfigType == FirebaseGameConfigType.Config_1) return firebaseGameConfig.ListScore;
//            }
//            return null;
//        }

//        public FirebaseConfigDB() 
//        {
//            ListKeyOnce = new();
//            ListDailyEvent = new();
//            adSessionTimeStore = new();
//            adBreakConfig = new();
//            LimitBoosterUsed = 3;
//            LoginDay = 0;
//            ReLoginTimes = 0;
//        }

//        public void SetLoginDate() 
//        {
//            LoginDate = DateTime.Now.ToString();
//            LoginDay = DateTime.Now.Day;
//        }

//        public void SaveDailyTotalGameForAdBreak() 
//        {
//            if (adBreakConfig.adBreakType == AdBreakType.Time) 
//            {
//                AdTotalGame = 0;
//                return;
//            }
//            AdTotalGame++;
//            if (adBreakConfig.adBreakType == AdBreakType.TotalGame && AdTotalGame > adBreakConfig.AdBreakTime) 
//            {
//                AdTotalGame = 0;
//            }
//        }

//        public void SaveActiveGameConfig(string data) 
//        {
//            var config = GetConfig<ActiveGameConfig>(data);
//            if (config != null) activeGameConfig = config;
//            Save();
//        }

//        public void SaveLimitBoosterConfig(string data) 
//        {
//            var config = GetConfig<BoosterLimitConfig>(data);
//            if (config != null) LimitBoosterUsed = config.LimitBoosterUsed;
//            Save();
//        }

//        public void SaveFirebaseGameConfig(string data) 
//        {
//            var config = GetConfig<FirebaseGameConfig>(data);
//            if (config != null) firebaseGameConfig = config;
//            Save();
//        }

//        public T GetConfig<T>(string data) where T : class
//        {
//            try
//            {
//                var config = JsonConvert.DeserializeObject<T>(data);
//                if (config != null) return config;
//            }
//            catch (System.Exception)
//            {
//                return null;
//            }
//            return null;
//        }

//        public void SaveAdBreakSettingConfig(string data) 
//        {
//            var config = GetConfig<AdBreakConfig>(data);
//            if (config != null) adBreakConfig = config;
//            Save();
//        }

//        public void SaveBannerAdTimeSessionStore(DateTime dateTime) 
//        {
//            adSessionTimeStore.SetBannerAdSessionTime(dateTime);
//            Save();
//        }

//        public void SaveInterstialAdSessionTime(DateTime dateTime) 
//        {
//            adSessionTimeStore.SetInterstialAdSessionTime(dateTime);
//            Save();
//        }

//        public void SaveInfiniteEnergyTimer(float Time) 
//        {
//            if (!string.IsNullOrEmpty(InfiniteTimerDate) && DateTime.TryParse(InfiniteTimerDate, out DateTime result)) {
//                if ((result - DateTime.Now).TotalSeconds > 0) 
//                {
//                    DateTime date = result.AddSeconds(Time);
//                    InfiniteTimerDate = date.ToString();
//                } else {
//                    InfiniteTimerDate = DateTime.Now.AddSeconds(Time).ToString();
//                }
//            } else {
//                InfiniteTimerDate = DateTime.Now.AddSeconds(Time).ToString();
//            }
//            Save();
//        }

//        public bool HasInfiniteEnergyTimer() 
//        {
//            if (!string.IsNullOrEmpty(InfiniteTimerDate) && DateTime.TryParse(InfiniteTimerDate, out DateTime result)) {
//                return (result - DateTime.Now).TotalSeconds > 0; 
//            }
//            return false;
//        }

//        public DateTime GetInfiniteEnergyTimer() 
//        {
//            if (!string.IsNullOrEmpty(InfiniteTimerDate) && DateTime.TryParse(InfiniteTimerDate, out DateTime result)) {
//                return result;
//            }
//            return DateTime.Now;
//        }

//        public bool AddKeyOnce(string key) 
//        {
//            if (ListKeyOnce.Contains(key)) return false;
//            ListKeyOnce.Add(key);
//            Save();
//            return true;
//        }

//        public bool AddDailyEventOnce(string key) 
//        {
//            var item = ListDailyEvent.Find(item => item.Key == key);
//            if (item == null) 
//            {
//                ListDailyEvent.Add(new() {
//                    Key = key,
//                    Value = 1,
//                    Day = DateTime.UtcNow.Day
//                });
//                return true;
//            }
//            return false;
//        }

//        public void ResetDailyEvent() 
//        {
//            LoginDay = DateTime.UtcNow.Day;
//            ReLoginTimes = 0;
//            ListDailyEvent.Clear();
//        }
//        public void AddDailyReLoginTimes() => AddDailyEvent(FirebaseDataKey.ReLoginTimes);
//        public void AddDailyRetentionDay() => AddDailyEvent(FirebaseDataKey.RetentionDay);
//        public void AddDailyReplayTimes() => AddDailyEvent(FirebaseDataKey.ReplayTimes);

//        public int DailyReplayTimes() 
//        {
//            var index = ListDailyEvent.FindIndex(item => item.Key == FirebaseDataKey.ReplayTimes);
//            if (index > -1) 
//            {
//                return ListDailyEvent[index].Value;
//            }
//            return 0;
//        }
        
//        public void AddDailyEvent(string key) 
//        {
//            var index = ListDailyEvent.FindIndex(item => item.Key == key);
//            if (index > -1) 
//            {
//                ListDailyEvent[index].Value++;
//            }
//            else 
//            {
//                ListDailyEvent.Add(new() {
//                    Key = key,
//                    Value = 1,
//                    Day = DateTime.UtcNow.Day
//                });
//            }
//        }

//        public void RemoveInfiniteEnergyTimer() => InfiniteTimerDate = "";
//        public void Save() => DBController.Instance.FIREBASE_CONFIG_DB = this;
//    }
//}