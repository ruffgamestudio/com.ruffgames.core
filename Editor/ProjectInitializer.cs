#if  UNITY_EDITOR

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace com.ruffgames.core.Editor
{
    public class ProjectInitializer : EditorWindow
    {
        private const string CorePackagePath = "Packages/com.ruffgames.core/Runtime/Dependencies";
        private const string ProjectContextPath = "Assets/Plugins/Zenject/OptionalExtras/IntegrationTests/SceneTests/TestDestructionOrder/RenameThisResources";
     

        private const string CompanyName = "RuffGames";
        private string ProductName;
        private static readonly List<string> PackagesToImport = new List<string>()
        {
            "DOTween",
            "Odin",
            "Nice Vibrations",
            "Epic Toon",
            "POLYGON - Particle",
            "Extenject",
            "True Shadow",
            "ruffgamescore",
            "GUI PRO Kit",
            "UI Particle",
        };
        
        [MenuItem("Ruff Games/Project Initializer")]
        private static void OpenWindow()
        {
            GetWindow<ProjectInitializer>().Show();
        }
        private void OnGUI()
        {
            DrawLogo();
            
            if (GUILayout.Button("Setup PlayerSettings", GUILayout.Height(80)))
            {
                SetupPlayerSettings();
            }
            if (GUILayout.Button("ImportPackages", GUILayout.Height(80)))
            {
                ImportPackages();
            }
            if (GUILayout.Button("ImportSDK", GUILayout.Height(80)))
            {
                ImportSDK();
            }
        }
        

        private void DrawLogo()
        {
            var banner = (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.ruffgames.core/Runtime/UI/logo.png", typeof(Texture));
            if (banner is null) return;
            var width = 400;
            var height = 400;
            GUI.DrawTexture (new Rect ((Screen.width / 2) - (width/2), (Screen.height / 2) - (height/2), width, height), banner,ScaleMode.ScaleToFit,true);
        }
        private void SetupPlayerSettings()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            
            ProductName = GetCleanProductName(Application.productName);
            var appIdentifier = $"com.{CompanyName.ToLower()}.{ProductName}".ToLower().Trim();
            
            PlayerSettings.productName = ProductName;
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android,appIdentifier);
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android,ScriptingImplementation.IL2CPP);
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            
            var aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
            PlayerSettings.Android.targetArchitectures = aac;
            
        }
        
        private void ImportPackages()
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

            var projectContext = Resources.Load(ProjectContextPath) as GameObject;
            PrefabUtility.SaveAsPrefabAsset(projectContext, "Assets/Resources/");

        }

        private void ImportSDK()
        {
            if (!Directory.Exists(CorePackagePath))
            {
                EditorUtility.DisplayDialog("Error", "Directory doesn't exist. Are you trying to run this script in Packages project?", "ok");
                return;
            }

            var files= System.IO.Directory.GetFiles(CorePackagePath);


            foreach (var file in files)
            {
               
                    if (file.Contains("SDK"))
                    {
                        Debug.Log("Importing package.." + file);
                        AssetDatabase.ImportPackage(file, false);
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
#endif
