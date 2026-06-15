using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "UserCurrencyData", menuName = "Game/UserCurrencyData Config")]
    internal class UserCurrencyData : ScriptableObject
    {
        public int Coins;
    }
}
