// Created by Hugo DA MAÏA on 05/11/2023

using SDKabu.KCore;

namespace Rezoskour.Content
{
    public interface IAchievementService : IKService
    {
        public void Init(string _pathToRegisteredData);

        public Achievement[] GetAchievements();
        public bool TryGetAchievement(string _id, out Achievement _achievement);

        public bool IsAchievementUnlocked(string _id);

        public bool TryRegisterValue(string _id, int _defaultValue);
        public bool TryUnregisterValue(string _id);

        public bool TryUpdateValue(string _id, int _newValue);

        /// <summary>
        /// Increment a value by 1 if it is registered.
        /// </summary>
        /// <returns>True if the value with given id has been incremented</returns>
        public bool TryIncrementValue(string _id);
    }
}