using System;
using N.Patterns;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace N.DB
{
    public class DBController : Singleton<DBController>
    {
        #region Default
        public static bool IsInitialized = false;

        protected override void Awake()
        {
            base.Awake();
            Initializing();
            IsInitialized = true;
        }
        void CheckDependency(string key, UnityAction<string> onComplete)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                onComplete?.Invoke(key);
            }
        }
        void Save<T>(string key, T values)
        {
            if (typeof(T) == typeof(int) ||
                typeof(T) == typeof(bool) ||
                typeof(T) == typeof(string) ||
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(Quaternion) ||
                typeof(T) == typeof(Vector2) ||
                typeof(T) == typeof(Vector3) ||
                typeof(T) == typeof(Vector2Int) ||
                typeof(T) == typeof(Vector3Int))
            {
                PlayerPrefs.SetString(key, $"{values}");
            }
            else
            {
                try
                {
                    string json = JsonUtility.ToJson(values);
                    PlayerPrefs.SetString(key, json);
                }
                catch (UnityException e)
                {
                    throw new UnityException(e.Message);
                }
            }
        }
        T LoadDataByKey<T>(string key)
        {
            if (typeof(T) == typeof(int) ||
                typeof(T) == typeof(bool) ||
                typeof(T) == typeof(string) ||
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(Quaternion) ||
                typeof(T) == typeof(Vector2) ||
                typeof(T) == typeof(Vector3) ||
                typeof(T) == typeof(Vector2Int) ||
                typeof(T) == typeof(Vector3Int))
            {
                var value = PlayerPrefs.GetString(key);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                var json = PlayerPrefs.GetString(key, null);
                if (!string.IsNullOrEmpty(json))
                {
                    var setting = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    json = JsonConvert.DeserializeObject(json, setting).ToString();
                }
                return JsonUtility.FromJson<T>(json);
            }
        }
        #endregion

        #region Declare
        //private FirebaseConfigDB firebaseConfigDB;
        //internal FirebaseConfigDB FIREBASE_CONFIG_DB
        //{
        //    get => firebaseConfigDB;
        //    set
        //    {
        //        firebaseConfigDB = value;
        //        Save(DBKey.FIREBASE_CONFIG_DB, value);
        //    }
        //}

        //private ShopDB shopDB;
        //internal ShopDB SHOP_DB
        //{
        //    get => shopDB;
        //    set
        //    {
        //        shopDB = value;
        //        Save(DBKey.SHOP_DB, value);
        //    }
        //}

        //private GameSettingDB gameSettingDB;
        //internal GameSettingDB GAME_SETTING_DB
        //{
        //    get => gameSettingDB;
        //    set
        //    {
        //        gameSettingDB = value;
        //        Save(DBKey.GAME_SETTING_DB, value);
        //    }
        //}

        //private UserDB userDB;
        //internal UserDB USER_DB
        //{
        //    get => userDB;
        //    set
        //    {
        //        userDB = value;
        //        Save(DBKey.USER_DB, value);
        //    }
        //}

        //private GameplayDB gameplayDB;
        //internal GameplayDB GAMEPLAY_DB
        //{
        //    get => gameplayDB;
        //    set
        //    {
        //        gameplayDB = value;
        //        Save(DBKey.GAMEPLAY_DB, value);
        //    }
        //}
        #endregion

        void Initializing()
        {
            //CheckDependency(DBKey.GAME_SETTING_DB, key => GAME_SETTING_DB = new GameSettingDB());
            //CheckDependency(DBKey.USER_DB, key => USER_DB = new UserDB());
            //CheckDependency(DBKey.GAMEPLAY_DB, key => GAMEPLAY_DB = new GameplayDB());
            //CheckDependency(DBKey.SHOP_DB, key => SHOP_DB = new ShopDB());
            //CheckDependency(DBKey.FIREBASE_CONFIG_DB, key => FIREBASE_CONFIG_DB = new FirebaseConfigDB());

            Load();
        }

        void Load()
        {
            //gameSettingDB = LoadDataByKey<GameSettingDB>(DBKey.GAME_SETTING_DB);
            //userDB = LoadDataByKey<UserDB>(DBKey.USER_DB);
            //gameplayDB = LoadDataByKey<GameplayDB>(DBKey.GAMEPLAY_DB);
            //shopDB = LoadDataByKey<ShopDB>(DBKey.SHOP_DB);
            //firebaseConfigDB = LoadDataByKey<FirebaseConfigDB>(DBKey.FIREBASE_CONFIG_DB);           
        }
    }
    internal class DBKey
    {
        //public static readonly string GAME_SETTING_DB = "GAME_SETTING_DB";
        //public static readonly string USER_DB = "USER_DB";
        //public static readonly string GAMEPLAY_DB = "GAMEPLAY_DB";
        //public static readonly string SHOP_DB = "SHOP_DB";
        //public static readonly string FIREBASE_CONFIG_DB = "FIREBASE_CONFIG_DB";
    }
}