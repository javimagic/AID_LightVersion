using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class BundleBuilder : Editor
{
    [MenuItem("Assets/Build AssetBundles")]
    static void ExportBundle()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\Javimagic\Desktop\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        // BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}