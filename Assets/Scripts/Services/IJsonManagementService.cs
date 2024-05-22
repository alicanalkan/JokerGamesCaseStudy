namespace JokerGames.Services
{
    public interface IJsonManagementService
    {
        string SerializeJson(object data);
        T DeserializeJson<T>(string jsonData);
    }
}
