// Created by Kabourlix Cendrée on 12/11/2023

namespace Rezoskour.Content.Shop
{
    public interface ICurrencyUser
    {
        public int Balance { get; }

        public void GainCurrency(int _amount);

        /// <summary>
        /// Try to spend the specified amount of currency.
        /// </summary>
        /// <returns>True if the transaction was successful, false otherwise.</returns>
        public bool TrySpendCurrency(int _amount);
    }
}