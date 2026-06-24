using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "UserCurrencyData", menuName = "Game/UserCurrencyData Config")]
    public class UserCurrencyData : ScriptableObject
    {
        [Min(0)]
        public int Coins;
    }
}
