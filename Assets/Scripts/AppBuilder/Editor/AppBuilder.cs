using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Android.AppBundle.Editor;
using UnityEditor;
using UnityEngine;

public static class AppBuilder {
    [MenuItem("Tools/Build App Bundle - Unity")]
    public static void BuildForUnity() {
        EditorUserBuildSettings.buildAppBundle = true;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;

        var assetPackConfig = new AssetPackConfig();
        assetPackConfig.AddAssetsFolder("asset_base", "Assets/GoogleOutput", AssetPackDeliveryMode.InstallTime);

        Bundletool.BuildBundle(new BuildPlayerOptions() {
            target = BuildTarget.Android,
            targetGroup = BuildTargetGroup.Android,
            locationPathName = Path.Combine(Application.dataPath, "..", "Build/Android", "android-app-bundle.aab"),
            scenes = new[] {
                "Assets/Scenes/SampleScene.unity",
            },
            options = BuildOptions.None
        }, assetPackConfig);
    }

    [MenuItem("Tools/Build App Bundle - Export Project")]
    public static void BuildForGradle() {
        EditorUserBuildSettings.buildAppBundle = false;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

        BuildPipeline.BuildPlayer(new[] {
            "Assets/Scenes/SampleScene.unity",
        }, Path.Combine(Application.dataPath, "..", "Build/Android", "android-app-bundle"), BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Tools/Build Asset Bundle")]
    public static void BuildAssetBundle() {
        var outputPath = "Assets/GoogleOutput";
        if (Directory.Exists(outputPath)) {
            Directory.Delete(outputPath, true);
        }

        Directory.CreateDirectory(outputPath);

        var assetBundleInfos = new AssetBundleBuild[] {
            new AssetBundleBuild() {
                assetBundleName = "ui/prefabs/launch.unity3d",
                assetNames = new[] {
                    "Assets/UI/Output/Prefabs/Launch.prefab"
                },
            }
        };

        BuildPipeline.BuildAssetBundles(outputPath, assetBundleInfos, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

        AssetDatabase.Refresh();
    }
}