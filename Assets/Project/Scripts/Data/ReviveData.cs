using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ReviveData", menuName = "Game/ReviveData Config")]
    public class ReviveData : ScriptableObject
    {
        public int CoinReviveCost;
    }
}
