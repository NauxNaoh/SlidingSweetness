//using System;
//using N.DB;
//using ScoreHistorySystem;

//namespace NauxDB
//{
//    [Serializable]
//    public class GameSettingDB 
//    {
//        public bool Sound;
//        public bool Music;
//        public bool Vibration;
//        public float SoundVolume = 1;
//        public float MusicVolume = 1;

//        public GameSettingDB() 
//        {
//            Sound = true;
//            Music = true;
//            Vibration = true;
//        }

//        public void SwitchSound() 
//        {
//            Sound = !Sound;
//            SoundVolume = Sound ? 1 : 0;
//            Save();
//        }

//        public void SwitchMusic() 
//        {
//            Music = !Music;
//            MusicVolume = Music ? 1 : 0;
//            Save();
//        }

//        public void SwitchVibration() 
//        {
//            Vibration = !Vibration;
//            Save();
//        }

//        public void Save() => DBController.Instance.GAME_SETTING_DB = this;
//    }
//}