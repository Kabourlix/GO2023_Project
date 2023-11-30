// Copyrighted by team Rézoskour
// Created by alexandre buzon on 14

using System;
using Rezoskour.Content.Shop;
using SDKabu.KCharacter;
using UnityEngine;

namespace Rezoskour.Content
{
    public class PlayerBrain : MonoBehaviour, IKActor, ICurrencyUser
    {
        public Guid ID { get; } = Guid.NewGuid();
        public KHealthComp HealthComp { get; private set; } = null!;

        private void Start()
        {
            HealthComp = new KHealthComp(4, this);
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            HealthComp.Dispose();
        }

        public void IncreaseMaxHealth(int _amount)
        {
            //HealthComp.IncreaseMaxHealth(_amount);
        }

        public void DecreaseMaxHealth(int _amount)
        {
            //HealthComp.DecreaseMaxHealth(_amount);
        }

        public int Balance { get; private set; }

        public void GainCurrency(int _amount)
        {
            Balance += _amount;
        }

        public bool TrySpendCurrency(int _amount)
        {
            if (Balance < _amount)
            {
                return false;
            }

            Balance -= _amount;
            return true;
        }
    }
}