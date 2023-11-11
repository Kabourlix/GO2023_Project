// Created by Kabourlix Cendrée on 06/11/2023

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


// Created by Hugo DA MAÏA on 06/11/2023

namespace Rezoskour.Content
{
    public class AchievementSystem : IAchievementService
    {
        private record AchievementCondition(string AchievementName, int ValueToReach);

        public event Action<string>? OnAchievementUnlocked;


        private readonly Dictionary<string, int> registeredValues = new();
        private readonly Dictionary<string, Achievement> registeredAchievement = new();
        private readonly Dictionary<string, List<AchievementCondition>> achievementsPerWatchedValue = new();

        private readonly string pathToAchievements;
        private readonly string pathToWatchedValue;

        public AchievementSystem(string _pathToAchievements = "Achievements.json",
            string _pathToWatched = "WatchedValues.json")
        {
            pathToAchievements = _pathToAchievements;
            pathToWatchedValue = _pathToWatched;

            LoadAllPossibleAchievement(pathToAchievements);
            LoadValues(pathToWatchedValue);

            Debug.Log($"{nameof(IAchievementService)} has loaded {registeredAchievement.Count} achievements.");
        }


        public void Dispose()
        {
            SaveAchievements(pathToAchievements);
            SaveWatchedValues(pathToWatchedValue);
        }

        public Achievement[] GetAchievements() => registeredAchievement.Values.ToArray();

        public bool TryGetAchievement(string _id, out Achievement _achievement)
            => registeredAchievement.TryGetValue(_id, out _achievement);


        public bool IsAchievementUnlocked(string _id)
        {
            bool result = registeredAchievement.TryGetValue(_id, out Achievement a);
            return result && a.IsUnlocked;
        }

        public bool TryRegisterAchievement(Achievement _achievement)
        {
            if (registeredAchievement.ContainsKey(_achievement.AchievementName))
            {
                Debug.LogError($"{_achievement.AchievementName} is already registered with this name.");
                return false;
            }

            registeredAchievement.Add(_achievement.AchievementName, _achievement);
            int index = 0;
            foreach (string watchedValueName in _achievement.ValueToWatch)
            {
                AchievementCondition condition = new(_achievement.AchievementName, _achievement.valueToReach[index]);
                if (achievementsPerWatchedValue.TryGetValue(watchedValueName,
                        out List<AchievementCondition> linkedAchievements))
                {
                    linkedAchievements.Add(condition);
                }
                else
                {
                    achievementsPerWatchedValue.Add(watchedValueName,
                        new List<AchievementCondition> { condition });
                }

                index++;
            }

            return true;
        }

        #region Values updates and registration

        public bool TryRegisterValue(string _id, int _defaultValue, bool _ignoreMessage = false)
        {
            if (registeredValues.ContainsKey(_id))
            {
                if (!_ignoreMessage)
                {
                    Debug.LogWarning($"{_id} is already registered.");
                }

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
            UpdateAchievements(_id);
            return true;
        }

        public bool TryGetValue(string _id, out int _value)
        {
            if (!registeredValues.ContainsKey(_id))
            {
                Debug.LogWarning($"{_id} is not registered as a followed value.");
                _value = 0;
                return false;
            }

            _value = registeredValues[_id];
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
            UpdateAchievements(_id);
            return true;
        }

        private void UpdateAchievements(string _watchedValueName)
        {
            if (!achievementsPerWatchedValue.ContainsKey(_watchedValueName))
            {
                Debug.LogWarning($"{_watchedValueName} is not watched by any achievement.");
                return;
            }

            int watchedValue = registeredValues[_watchedValueName];


            foreach ((string achievementName, int conditionValue) in achievementsPerWatchedValue[_watchedValueName])
            {
                if (!registeredAchievement.ContainsKey(achievementName))
                {
                    Debug.LogError($"{achievementName} is not registered as an achievement.");
                    continue;
                }

                Achievement a = registeredAchievement[achievementName];
                if (a.IsUnlocked)
                {
                    continue;
                }

                a.IsUnlocked = watchedValue >= conditionValue;

                if (a.IsUnlocked)
                {
                    OnAchievementUnlocked?.Invoke(achievementName);
                }

                registeredAchievement[achievementName] = a;
            }
        }

        #endregion

        private void LoadAllPossibleAchievement(string _path)
        {
            //Look for all achievements in the game

            string path = Path.Combine(Application.persistentDataPath, _path);
            string json = string.Empty;
            if (!File.Exists(path))
            {
                TextAsset? asset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(_path));
                if (asset == null)
                {
                    Debug.LogError($"File {_path} does not exist.");
                    return;
                }

                json = asset.text;
            }
            else
            {
                json = File.ReadAllText(path);
            }

            //string json = Resources.Load<TextAsset>(_path).text;
            Achievement[] allAchievements =
                JsonConvert.DeserializeObject<Achievement[]>(json) ?? Array.Empty<Achievement>();

            foreach (Achievement a in allAchievements)
            {
                foreach (string valueName in a.ValueToWatch)
                {
                    TryRegisterValue(valueName, 0); //Register condition
                }

                TryRegisterAchievement(a);
            }
        }

        private void LoadValues(string _path)
        {
            string path = Path.Combine(Application.persistentDataPath, _path);
            string json = string.Empty;
            if (!File.Exists(path))
            {
                TextAsset? asset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(_path));
                if (asset == null)
                {
                    Debug.LogError($"File {_path} does not exist.");
                    return;
                }

                json = asset.text;
            }
            else
            {
                json = File.ReadAllText(path);
            }

            //string json = Resources.Load<TextAsset>(_pathToWatchedValueInResources).text;
            WatchedValue[] allValues =
                JsonConvert.DeserializeObject<WatchedValue[]>(json) ?? Array.Empty<WatchedValue>();

            foreach (WatchedValue v in allValues)
            {
                if (TryRegisterValue(v.valueName, v.value, true))
                {
                    continue;
                }

                registeredValues[v.valueName] = v.value;
            }
        }

        private void SaveAchievements(string _pathInResources)
        {
            Achievement[] allAchievements = GetAchievements();
            if (allAchievements.Length == 0)
            {
                Debug.LogWarning("No achievement to save.");
                return;
            }

            string json = JsonConvert.SerializeObject(allAchievements);

            string path = Path.Combine(Application.persistentDataPath, _pathInResources);
            File.WriteAllText(path, json);
        }

        private void SaveWatchedValues(string _pathInResources)
        {
            WatchedValue[] allValues = registeredValues.Select(_pair => new WatchedValue(_pair.Key, _pair.Value))
                .ToArray();
            if (allValues.Length == 0)
            {
                Debug.LogWarning("No watched value to save.");
                return;
            }

            string json = JsonConvert.SerializeObject(allValues);

            string path = Path.Combine(Application.persistentDataPath, _pathInResources);
            File.WriteAllText(path, json);
        }
    }
}