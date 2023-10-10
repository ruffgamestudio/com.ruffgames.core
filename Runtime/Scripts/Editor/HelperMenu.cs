using com.ruffgames.core.Runtime.Scripts.Storage;
using UnityEditor;

namespace com.ruffgames.core.Runtime.Scripts.Editor
{
#if UNITY_EDITOR
    public class HelperMenu
    {
        [MenuItem("Tools/ClearStorage", false, 1)]
        private static void ClearStorage()
        {
            var storage = new JsonStorage();
            storage.Clear();
        }
    }
#endif
}