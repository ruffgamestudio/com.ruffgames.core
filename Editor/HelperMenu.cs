using com.ruffgames.core.Runtime.Scripts.Storage;
using UnityEditor;

namespace com.ruffgames.core.Editor
{
#if UNITY_EDITOR
    public class HelperMenu
    {
        [MenuItem("Data/Clear Data", false, 1)]
        private static void ClearStorage()
        {
            var storage = new JsonStorage();
            storage.Clear();
        }
    }
#endif
}