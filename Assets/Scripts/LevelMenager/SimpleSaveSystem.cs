using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace LevelMenager
{
    public class SimpleSaveSystem
    {
        public static void SaveXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Create);

            SaveData data = new SaveData();
            EventSystem.SaveGame(data);
            
            Debug.Log($"Saving - Coins: {data.coins}, Diamonds: {data.diamonds}, HealthPotions: {data.healthPotions}");
            
            serializer.Serialize(stream, data);
        
            stream.Close();
        }
    
        public static void LoadXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            FileStream stream = new FileStream(Application.dataPath + "/../save.xml", FileMode.Open);

            SaveData data = serializer.Deserialize(stream) as SaveData;
            
            Debug.Log($"Loaded - Coins: {data.coins}, Diamonds: {data.diamonds}, HealthPotions: {data.healthPotions}");
            
            EventSystem.LoadGame(data);
        
            stream.Close();
        }
    }
}
