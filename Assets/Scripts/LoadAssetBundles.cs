using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class LoadAssetBundles : MonoBehaviour {

    private AssetBundle myTestedAssetBundle;
    // public string url = "https://138.100.77.240/share.cgi?ssid=0f6Qhib&fid=0f6Qhib&filename=animal&openfolder=forcedownload&ep=";
    public string url = "https://138.100.77.240/share.cgi?ssid=0gJDVpq&fid=0gJDVpq&filename=animal&openfolder=forcedownload&ep=";
    public string path;
    public List<string> assets;

    private WebClient descarga = new WebClient();

    void Awake () {
        ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        descarga.DownloadFile(new System.Uri(url), path);
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
