// Copyrighted by team Rézoskour
// Created by alexandre buzon on 24

using System.Collections;
using System.Collections.Generic;
using Rezoskour.Content.Shop;
using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "IncreaseHp", menuName = "Rezoskour/Shop Item/Increase Hp")]
    public class IncreaseHp : ShopItem
    {
        [SerializeField] private int amount;

        public override void Buy()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBrain>().IncreaseMaxHealth(amount);
        }
    }
}