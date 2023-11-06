// Created by Hugo DA MAÏA on 06/11/2023

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Created by Hugo DA MAÏA on 06/11/2023

namespace Rezoskour.Content
{
    public class AchievementSystem : IAchievementService
    {
        private Dictionary<string, int> registeredValues = new();
        private readonly Dictionary<string, Achievement> registeredAchievement = new();

        public void Init(string _pathToRegisteredData)
        {
            foreach (Achievement achievement in LoadAchievements(_pathToRegisteredData).AsSpan())
            {
                registeredAchievement.Add(achievement.AchievementName, achievement);
            }

            Debug.Log($"{nameof(IAchievementService)} has loaded {registeredAchievement.Count} achievements.");
        }

        public void Dispose()
        {
        }

        public Achievement[] GetAchievements() => registeredAchievement.Values.ToArray();

        public bool TryGetAchievement(string _id, out Achievement _achievement)
            => registeredAchievement.TryGetValue(_id, out _achievement);


        public bool IsAchievementUnlocked(string _id)
        {
            bool result = registeredAchievement.TryGetValue(_id, out Achievement a);
            return result && a.IsUnlocked;
        }

        #region Values updates and registration

        public bool TryRegisterValue(string _id, int _defaultValue)
        {
            if (registeredValues.ContainsKey(_id))
            {
                Debug.LogWarning($"{_id} is already registered.");
                return false;
            }

            registeredValues.Add(_id, _defaultValue);
            return true;
        }

        public bool TryUnregisterValue(string _id)
        {
            if (!registeredValues.ContainsKey(_id))
            {
                Debug.LogWarning($"{_id} cannot be unregistered since it is not in the collection.");
                return false;
            }

            registeredValues.Remove(_id);
            return true;
        }

        public bool TryUpdateValue(string _id, int _newValue)
        {
            if (!registeredValues.ContainsKey(_id))
            {
                Debug.LogWarning($"{_id} is not registered as a followed value.");
                return false;
            }

            registeredValues[_id] = _newValue;
            return true;
        }

        public bool TryIncrementValue(string _id)
        {
            if (!registeredValues.ContainsKey(_id))
            {
                Debug.LogWarning($"{_id} is not registered as a followed value.");
                return false;
            }

            registeredValues[_id]++;
            return true;
        }

        #endregion


        private Achievement[]? LoadAchievements(string _pathToJson)
        {
            //TODO
            return null;
        }
    }
}