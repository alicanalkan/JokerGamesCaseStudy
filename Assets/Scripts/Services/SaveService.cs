using System.IO;
using JokerGames.Data;
using UnityEngine;

namespace JokerGames.Services
{
    public class SaveService : ISaveService
    {

        private readonly IJsonManagementService _jsonManagementService;
        public SaveService(IJsonManagementService jsonManagementService)
        {
            _jsonManagementService = jsonManagementService;
        }
        
        /// <summary>
        /// Save Player Data to Temp Folder
        /// </summary>
        /// <param name="playerData"></param>
        public void Save(PlayerData playerData)
        {
            string path = Application.persistentDataPath + "/" + "JokerPlayer" + ".dat";
            var data = _jsonManagementService.SerializeJson(playerData);
            File.WriteAllText(path,data);
        }

        /// <summary>
        /// Load PlayerData from temp folder
        /// </summary>
        /// <returns></returns>
        public PlayerData Load()
        {
            string path = Application.persistentDataPath + "/" + "JokerPlayer" + ".dat";
            if (File.Exists(path))
            {
                var data =  File.ReadAllText(path);

                var playerData = _jsonManagementService.DeserializeJson<PlayerData>(data);
        
                return playerData;
            }
            else
            {
                PlayerData data = new PlayerData();
                return data;
            }
        }
    }
}
