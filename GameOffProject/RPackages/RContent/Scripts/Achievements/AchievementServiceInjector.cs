// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public class AchievementServiceInjector : MonoBehaviour
    {
        private void Awake()
        {
            AchievementSystem service = new();
            KServiceInjection.Add<IAchievementService>(service);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IAchievementService>();
        }
    }
}