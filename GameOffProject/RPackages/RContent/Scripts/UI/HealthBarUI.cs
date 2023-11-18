// Copyrighted by team Rézoskour
// Created by alexandre buzon on 15

using System.Collections.Generic;
using SDKabu.KCharacter;
using UnityEngine;
using UnityEngine.UI;

namespace Rezoskour.Content
{
    public class HealthBarUI : MonoBehaviour
    {
        private KHealthComp healthBar;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;
        private List<Image> heartsList = new();

        // Start is called before the first frame update
        private void Start()
        {
            foreach (Image heart in heartsList)
            {
                Destroy(heart.gameObject);
            }

            heartsList.Clear();

            healthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBrain>().HealthComp;
            healthBar.OnHealthChanged += UpdateHealthBar;
            healthBar.OnMaxHealthChanged += IncreaseMaxHealth;
            Init();
            //healthBar.OnIncomingDamage
        }

        private void OnDestroy()
        {
            healthBar.OnHealthChanged -= UpdateHealthBar;
            healthBar.OnMaxHealthChanged -= IncreaseMaxHealth;
        }

        private void Init()
        {
            int maxHealth = healthBar.MaxHealth;
            for (int i = 0; i < maxHealth / 2; i++)
            {
                GameObject heart = Instantiate(heartPrefab, transform);
                heartsList.Add(heart.GetComponent<Image>());
            }

            int health = healthBar.Health;
            foreach (Image heart in heartsList)
            {
                if (health >= 2)
                {
                    heart.sprite = fullHeart;
                    health -= 2;
                }
                else if (health == 1)
                {
                    heart.sprite = halfHeart;
                    health -= 1;
                }
                else
                {
                    heart.sprite = emptyHeart;
                }
            }
        }

        private void IncreaseMaxHealth(IKActor _arg1, int _maxHealth)
        {
            int currentMaxHealth = heartsList.Count * 2;
            int newMaxHealth = _maxHealth - currentMaxHealth;
            if (_maxHealth <= 20 && newMaxHealth % 2 == 0) //Can't have more than 10 hearts and can't have half a heart
            {
                for (int i = 0; i < newMaxHealth / 2; i++)
                {
                    GameObject heart = Instantiate(heartPrefab, transform);
                    heartsList.Add(heart.GetComponent<Image>());
                }
            }
        }

        private void UpdateHealthBar(IKActor _arg1, int _arg2)
        {
            int health = _arg2;
            foreach (Image heart in heartsList)
            {
                if (health >= 2)
                {
                    heart.sprite = fullHeart;
                    health -= 2;
                }
                else if (health == 1)
                {
                    heart.sprite = halfHeart;
                    health -= 1;
                }
                else
                {
                    heart.sprite = emptyHeart;
                }
            }
        }
    }
}