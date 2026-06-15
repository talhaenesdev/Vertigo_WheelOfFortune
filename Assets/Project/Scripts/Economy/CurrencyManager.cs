using Assets.Project.Scripts.Data;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    internal class CurrencyManager : MonoBehaviour
    {

        [SerializeField]
        private UserCurrencyData _userCurrencyData;

        internal void AddCoins(int amount) => _userCurrencyData.Coins += amount;

        internal bool SpendCoins(int amount)
        {
            if (_userCurrencyData.Coins >= amount)
            {
                _userCurrencyData.Coins -= amount;
                return true;
            }
            return false;
        }

        internal int GetCurrentCoins() => _userCurrencyData.Coins;
    }
}
