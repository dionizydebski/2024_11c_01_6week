using UnityEngine;

namespace Collectibles
{
    [CreateAssetMenu(menuName = "Scriptable Object/Health Potion Config", fileName = "Health Potion Config")]
    public class HealthPotionConfig : ScriptableObject
    {
        public string Name;
        public int HealAmount; 
    }
}
