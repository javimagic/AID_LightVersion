using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class LoadAssetBundlesOffline : MonoBehaviour {

    private AssetBundle myTestedAssetBundle;
    public string path;
    public List<string> assets;

    void Awake () {
        LoadAssetBundle(path);
        InstantiateObjectFromBundle(assets);
	}
	
	// Update is called once per frame
	void LoadAssetBundle (string bundleUrl) {
        myTestedAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
        Debug.Log(myTestedAssetBundle == null ? "Failed to load AssetBundle" : "AssetBundle succesfully loaded");
	}


    void InstantiateObjectFromBundle (List<string> assetsList)
    {
        foreach (string assetName in assetsList)
        {
            var prefab = myTestedAssetBundle.LoadAsset(assetName);
            Instantiate(prefab);
        }
    }

}
