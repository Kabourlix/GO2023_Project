// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using UnityEngine;

namespace Rezoskour.Content.SceneManagement
{
    public class LoadFromMenu : MonoBehaviour
    {
        [SerializeField] private string sceneName = null!;

        public void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}