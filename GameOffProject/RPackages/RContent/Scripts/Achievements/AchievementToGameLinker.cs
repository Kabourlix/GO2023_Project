// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using System;
using System.IO;
using SDKabu.KCore;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.WSA.Application;
using Object = UnityEngine.Object;

// Created by Kabourlix Cendrée on 11/11/2023

namespace Rezoskour.Content
{
    public class AchievementToGameLinker : MonoBehaviour
    {
        private IAchievementService? achievementService = null!;


        private void Start()
        {
            achievementService = KServiceInjection.Get<IAchievementService>();
            if (achievementService == null)
            {
                return;
            }

            achievementService.OnAchievementUnlocked += OnAchievementUnlocked;

            achievementService.TryIncrementValue("gameLaunched");
        }

        private void OnAchievementUnlocked(string _obj)
        {
            Debug.Log("<color=blue> Achievement unlocked: </color>" + _obj);
        }
    }
}