using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ADData", menuName = "Game/ADData Config")]
    internal class AdData : ScriptableObject
    {
        public int AdWaitTimeSeconds;
    }
}
