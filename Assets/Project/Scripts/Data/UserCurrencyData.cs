using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "UserCurrencyData", menuName = "Game/UserCurrencyData Config")]
    public class UserCurrencyData : ScriptableObject
    {
        public int Coins;
    }
}
