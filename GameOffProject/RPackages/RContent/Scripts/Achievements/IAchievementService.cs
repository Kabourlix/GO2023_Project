// Created by Kabourlix Cendrée on 06/11/2023

#nullable enable

using System;
using JetBrains.Annotations;
using SDKabu.KCore;

namespace Rezoskour.Content
{
    public interface IAchievementService : IKService
    {
        public event Action<string>? OnAchievementUnlocked;

        public Achievement[] GetAchievements();
        public bool TryGetAchievement(string _id, out Achievement _achievement);

        public bool IsAchievementUnlocked(string _id);

        public bool TryRegisterAchievement(Achievement _achievement);

        public bool TryRegisterValue(string _id, int _defaultValue, bool _ignoreMessage = false);
        public bool TryUnregisterValue(string _id);

        public bool TryGetValue(string _id, out int _value);

        public bool TryUpdateValue(string _id, int _newValue);

        /// <summary>
        /// Increment a value by 1 if it is registered.
        /// </summary>
        /// <returns>True if the value with given id has been incremented</returns>
        public bool TryIncrementValue(string _id);
    }
}