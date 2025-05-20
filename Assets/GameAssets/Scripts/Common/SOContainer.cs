using N.Patterns;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SlidingSweetness
{
    public class SOContainer : PersistentSingleton<SOContainer>
    {
        [Required]
        [SerializeField] private DifficultLevelSetting difficultLevelSetting;
        [Required]
        [SerializeField] private BlockSkinSetting blockSkinSetting;




        public static DifficultLevelSetting DifficultLevel => Instance.difficultLevelSetting;
        public static BlockSkinSetting BlockSkin => Instance.blockSkinSetting;
    }
}