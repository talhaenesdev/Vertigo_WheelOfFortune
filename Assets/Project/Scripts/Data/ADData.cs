using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ADData", menuName = "Game/ADData Config")]
    public class AdData : ScriptableObject
    {
        [Range(0, 10)]
        public int AdWaitTimeSeconds;
    }
}
