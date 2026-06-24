using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ReviveData", menuName = "Game/ReviveData Config")]
    public class ReviveData : ScriptableObject
    {
        [Min(0)]
        public int CoinReviveCost;
    }
}
