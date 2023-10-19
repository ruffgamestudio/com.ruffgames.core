
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace com.ruffgames.core.Editor
{

	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold,
				fixedWidth = 150
				
			};
		}
	}
	
	[InitializeOnLoad]
	public class AndroidBuildButton
	{
		static AndroidBuildButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}
		
		private static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();
			
			if (GUILayout.Button(new GUIContent("Android Build", "Android Build"), ToolbarStyles.commandButtonStyle))
			{
				APKBuild();
			}
		}
		
		private static void APKBuild()
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            
			var outputPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

			var playerOptions = new BuildPlayerOptions()
			{
				target = BuildTarget.Android,
				subtarget = (int)StandaloneBuildSubtarget.Player,
				targetGroup = BuildPipeline.GetBuildTargetGroup(BuildTarget.Android),
				options = BuildOptions.CompressWithLz4HC,
				locationPathName = outputPath
			};
			BuildPipeline.BuildPlayer(playerOptions);
		}
	}

}
#endif