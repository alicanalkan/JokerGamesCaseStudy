using System;
using System.Threading.Tasks;


namespace JokerGames.Utils
{
    public static class  Utils
    {
        public static async Task WaitUntil(Func<bool> predicate, int sleep = 50)
        {
            while (!predicate())
            {
                await Task.Delay(sleep);
            }
        }
        
    }
}
