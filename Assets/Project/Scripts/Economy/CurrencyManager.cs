using Assets.Project.Scripts.Data;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    internal class CurrencyManager : MonoBehaviour, ICurrencyService
    {

        [SerializeField]
        private UserCurrencyData _userCurrencyData;

        public void AddCoins(int amount) => _userCurrencyData.Coins += amount;

        public bool SpendCoins(int amount)
        {
            if (_userCurrencyData.Coins >= amount)
            {
                _userCurrencyData.Coins -= amount;
                return true;
            }
            return false;
        }

        public int GetCurrentCoins() => _userCurrencyData.Coins;
    }
}
