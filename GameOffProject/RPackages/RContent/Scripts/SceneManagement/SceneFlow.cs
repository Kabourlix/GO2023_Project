// Created by Kabourlix Cendrée on 11/11/2023

using System.Collections;
using System.Collections.Generic;
using Rezoskour.Content.SceneManagement;
using UnityEngine;

namespace Rezoskour.Content
{
    public class SceneFlow : MonoBehaviour
    {
        [SerializeField] private string sceneName = null!;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f); //Improve this
            SceneManager.LoadScene(sceneName);
        }
    }
}