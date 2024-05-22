using System.Collections;
using System.Collections.Generic;
using JokerGames.Data;
using UnityEngine;

namespace JokerGames.Services
{
    public interface ISaveService
    {

        void Save(PlayerData playerData);

        PlayerData Load();
    }
}
