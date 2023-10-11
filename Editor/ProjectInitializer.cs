using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace com.ruffgames.core.Editor
{
    public class ProjectInitializer : OdinEditorWindow
    {
        [SerializeField] public Texture aTexture;

        private const string CorePackagePath = "Packages/com.ruffgames.core/Runtime/Dependencies";
        [ShowInInspector,ReadOnly] private const string CompanyName = "RuffGames";
        [ShowInInspector,ReadOnly] private string ProductName;
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
        public void SetupPlayerSettings()
        {
            ProductName = GetCleanProductName(Application.productName);
            var appIdentifier = $"com.{CompanyName.ToLower()}.{ProductName}".ToLower().Trim();
            
            PlayerSettings.productName = ProductName;
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android,appIdentifier);
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android,ScriptingImplementation.IL2CPP);
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
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

        private string GetCleanProductName(string productName)
        {
            return Regex.Replace(productName, @"[^0-9a-zA-Z]+", "").ToLower().Trim();
        }    
    }
}
