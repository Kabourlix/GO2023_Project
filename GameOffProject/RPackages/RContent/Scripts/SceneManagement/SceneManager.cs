// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using UnityEngine.SceneManagement;

// Created by Kabourlix Cendrée on 11/11/2023

namespace Rezoskour.Content.SceneManagement
{
    public static class SceneManager
    {
        public static void LoadScene(string _sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
        }
    }
}