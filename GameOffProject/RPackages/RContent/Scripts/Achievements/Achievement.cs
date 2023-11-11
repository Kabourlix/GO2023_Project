// Created by Kabourlix Cendrée on 06/11/2023

using System;
using Rezoskour.Content.Shop;
using Unity.Plastic.Newtonsoft.Json;

namespace Rezoskour.Content
{
    [Serializable]
    public struct Achievement
    {
        public string AchievementName;

        public string AchievementDescription;

        public string achievementIconPath;

        public string[] ValueToWatch;

        public int[] valueToReach;

        public IObject reward;
        public bool IsUnlocked { get; set; }

        public static string ToJson(Achievement _a)
        {
            return JsonConvert.SerializeObject(_a);
        }

        public static Achievement FromJson(string _json)
        {
            return JsonConvert.DeserializeObject<Achievement>(_json);
        }
    }

    [Serializable]
    public record WatchedValue(string valueName, int value);
}