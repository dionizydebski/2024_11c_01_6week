using System;

namespace LevelMenager
{
    [Serializable]
    public class SaveData
    {
        public float PositionX;
        public float PositionY;
        public int coins;
        public int diamonds;
        public int healthPotions;
        public int skulls;
        public int keys;
    }
}
