// Created by Kabourlix Cendrée on 10/11/2023

using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Rezoskour/Enemy Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyType type;
        public EnemyType Type => type;

        [SerializeField] private int maxHealth = 3;
        public int MaxHealth => maxHealth;

        [SerializeField] private int damage = 1;
        public int Damage => damage;

        [SerializeField] private float speed = 1f;
        public float Speed => speed;
    }
}