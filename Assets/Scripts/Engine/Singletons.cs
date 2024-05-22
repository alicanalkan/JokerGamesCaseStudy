using System;
using System.Collections.Generic;
using JokerGames.Managers;

namespace JokerGames.Engine
{
    public static class Singletons
    {
        public static readonly Dictionary<Type, string> TypeNameMap = new Dictionary<Type, string>
        {
            {typeof(GameManager),"Player Manager"}
        };
    }
}
