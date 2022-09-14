using System.IO;
using UnityEngine;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.Managers
{
    public class DataManager : MonoBehaviour
    {
        private string _filePath;

        public GameData GameData { get; private set; }

        private void Awake()
        {
            _filePath = Application.persistentDataPath + "/GameData.json";
            GameData = new();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(GameData);
            File.WriteAllText(_filePath, json);
        }

        public void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var fileContents = File.ReadAllText(_filePath);
                GameData = JsonUtility.FromJson<GameData>(fileContents);
            }
        }
    }
}