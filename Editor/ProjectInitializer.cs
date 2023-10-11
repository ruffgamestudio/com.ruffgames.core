using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace com.ruffgames.core.Core.Editor
{
    public class ProjectInitializer : OdinEditorWindow
    {
        private const string CorePackagePath = "Packages/com.ruffgames.core/Runtime/Dependencies";

        private static readonly List<string> PackagesToImport = new List<string>()
        {
            "Core",
            "DOTween",
            "Odin",
            "Epic",
        };
        
        [MenuItem("Ruff Games/Project Initializer")]
        private static void OpenWindow()
        {
            GetWindow<ProjectInitializer>().Show();
        }

        [Button(ButtonSizes.Large)]
        public void ImportPackages()
        {
            if (!Directory.Exists(CorePackagePath))
            {
                EditorUtility.DisplayDialog("Error","Directory doesn't exist. Are you trying to run this script in Packages project?","ok");
                return;
            }

            var files = System.IO.Directory.GetFiles(CorePackagePath);

            foreach (var file in files)
            {
                foreach (var package in PackagesToImport)
                {
                    if (file.Contains(package))
                    {
                        Debug.Log("Importing package.."+ file);
                        AssetDatabase.ImportPackage(file,false);
                    }
                       
                }
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
        }
    }
}
