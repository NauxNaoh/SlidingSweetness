//using System;
//using System.Collections.Generic;
//using N.DB;
//using UnityEngine;

//namespace NauxDB
//{
//    public enum UserRoleStatus { Normal, Premium, VIP }

//    [Serializable]
//    public class PurchaseMultiplePack 
//    {
//        public string packageID;
//        public int Amount;
//    }

//    [Serializable]
//    public class PurchaseDailyPack 
//    {
//        public string packageID;
//        public int Amount;
//        public int Day;
//        public int MaxPurchase;
        
//        public PurchaseDailyPack(string id) 
//        {
//            packageID = id;
//            Amount = 1;
//            Day = DateTime.UtcNow.Day;
//        }
        
//        public void AddPurchase() => Amount++;

//        public bool CanPurchase() 
//        {
//            if (Amount < MaxPurchase) 
//            {
//                AddPurchase();
//                return true;
//            }
//            return false;
//        }

//        public void Reset() 
//        {
//            if (DateTime.UtcNow.Day != Day)
//            {
//                Amount = 0;
//                Day = DateTime.UtcNow.Day;
//            }
//        }
//    }

//    [Serializable]
//    public class ShopDB 
//    {
//        [SerializeField] private bool RemovedAd;
//        [SerializeField] private UserRoleStatus userRoleStatus;
//        [SerializeField] private List<string> ListPurchaseOnlyOne;
//        [SerializeField] private List<PurchaseMultiplePack> ListMultiplePack;
//        [SerializeField] private List<PurchaseDailyPack> ListDailyPack;

//        // public access varialbles
//        public bool IsPremiumUser => userRoleStatus == UserRoleStatus.Premium;
//        public bool IsVipUser => userRoleStatus == UserRoleStatus.VIP;
//        public bool IsRemovedAd => RemovedAd || IsPremiumUser || IsVipUser;
//        public bool IsProductPurchased(string packageID) => ListPurchaseOnlyOne.Contains(packageID);
//        public bool IsProductPurchased(string packageID, int limit) => ListMultiplePack.Exists(item => item.packageID == packageID && item.Amount >= limit);
//        public bool IsDailyProductPurchased(string packageID) => ListDailyPack.Exists(item => item.packageID == packageID && item.Amount > 0);
//        public int GetDailyProductPurchasedAmount(string packageID) 
//        {
//            var index = ListDailyPack.FindIndex(item => item.packageID == packageID);
//            if (index > -1) 
//            {
//                return ListDailyPack[index].Amount;
//            }
//            return 0;
//        }

//        public ShopDB() 
//        {
//            RemovedAd = false;
//            userRoleStatus = UserRoleStatus.Normal;
//            ListPurchaseOnlyOne = new();
//            ListMultiplePack = new();
//            ListDailyPack = new();
//        }

//        internal void ResetDailyData() 
//        {
//            foreach (var item in ListDailyPack)
//            {
//                item.Reset();
//            }   
//        }

//        internal void SetPurchaseDailyItem(string id) 
//        {
//            int index = ListDailyPack.FindIndex(item => item.packageID == id);
//            if (index > -1) 
//            {
//                ListDailyPack[index].AddPurchase();
//                return;
//            }
//            ListDailyPack.Add(new(id));
//        }

//        internal void SetPurchaseRemovedAd() 
//        {
//            RemovedAd = true;
//            Save();
//        }

//        internal void SetPurchaseUserRole(UserRoleStatus status) 
//        {
//            userRoleStatus = status;
//            switch (userRoleStatus)
//            {
//                case UserRoleStatus.Normal:
//                    break;
//                case UserRoleStatus.Premium:
//                    RemovedAd = true;
//                    break;
//                case UserRoleStatus.VIP:
//                    RemovedAd = true;
//                    break;
//            }
//            Save();
//        }

//        internal void SetPurchaseOnlyOneProduct(string packageID) 
//        {
//            if (!ListPurchaseOnlyOne.Contains(packageID)) 
//            {
//                ListPurchaseOnlyOne.Add(packageID);
//            }
//            Save();
//        }

//        internal void SetPurchaseMultipleProduct(string packageID) 
//        {
//            int index = ListMultiplePack.FindIndex(item => item.packageID == packageID);
//            if (index > -1) 
//            {
//                ListMultiplePack[index].Amount++;
//            } else {
//                ListMultiplePack.Add(new PurchaseMultiplePack {
//                    packageID = packageID,
//                    Amount = 1
//                });
//            }
//            Save();
//        }

//        public void Save() => DBController.Instance.SHOP_DB = this;
//    }
//}