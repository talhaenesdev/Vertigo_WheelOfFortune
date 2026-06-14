using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "ADData", menuName = "Game/ADData Config")]
    public class ADData : ScriptableObject
    {
        public int AdWaitTimeSeconds;
    }
}
