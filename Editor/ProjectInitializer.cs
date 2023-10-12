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
        private const string CorePackagePath = "Packages/com.ruffgames.core/Runtime/Dependencies";
        private const string ProjectContextPath = "Assets/Plugins/Zenject/OptionalExtras/IntegrationTests/SceneTests/TestDestructionOrder/RenameThisResources";
        private const string CompanyName = "RuffGames";
        [ShowInInspector,ReadOnly] private string ProductName;
        private static readonly List<string> PackagesToImport = new List<string>()
        {
            "DOTween",
            "Epic Toon",
            "Nice Vibrations",
            "Tru Shadow",
            "common"
        };
        
        [MenuItem("Ruff Games/Project Initializer")]
        private static void OpenWindow()
        {
            GetWindow<ProjectInitializer>().Show();
        }
        protected override void OnGUI()
        {
            base.OnGUI();
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.ruffgames.core/Runtime/UI/logo.png", typeof(Texture));
            float width = 400;
            float height = 400;
            GUI.DrawTexture (new Rect ((Screen.width / 2) - (width/2), (Screen.height / 2) - (height/2), width, height), banner,ScaleMode.ScaleToFit,true);
            
        }
        
        [Title("Ruff Games Project Initializer", titleAlignment: TitleAlignments.Centered)]
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

            var projectContext = Resources.Load(ProjectContextPath) as GameObject;
            PrefabUtility.SaveAsPrefabAsset(projectContext, "Assets/Resources/");

        }

        private string GetCleanProductName(string productName)
        {
            return Regex.Replace(productName, @"[^0-9a-zA-Z]+", "").ToLower().Trim();
        }

    }
}
