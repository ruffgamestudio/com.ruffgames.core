using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    [CreateAssetMenu(menuName = "Data/Project Reference Data")]
    public class ProjectReferenceData : SerializedScriptableObject
    {
        [TableList]
        public LevelConfig LevelConfig;
    }
}
