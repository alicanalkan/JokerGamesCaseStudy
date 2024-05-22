// using Newtonsoft.Json;

using UnityEngine;

namespace JokerGames.Services
{
    public class JsonManagementService : IJsonManagementService
    {
        /// <summary>
        /// Serialize Class to json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SerializeJson(object data)
        {
            return JsonUtility.ToJson(data);
        } 
        
        /// <summary>
        /// DeSerialize json to class
        /// </summary>
        /// <param name="jsonData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DeserializeJson<T>(string jsonData)
        {
            return JsonUtility.FromJson<T>(jsonData);
        }
    }
}
