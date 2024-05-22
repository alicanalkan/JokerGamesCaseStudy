namespace JokerGames.Services
{
    public interface IServiceLocator 
    {
        T GetService<T>();
        void RegisterService<T>(T service);
    }
}
