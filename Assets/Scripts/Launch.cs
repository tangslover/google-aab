using System.Collections;
using System.Collections.Generic;
using Google.Play.AssetDelivery;
using UnityEngine;

public class Launch : MonoBehaviour {
    [SerializeField] private Transform baseRoot;

    // Start is called before the first frame update
    void Start() {
        var op = PlayAssetDelivery.RetrieveAssetPackAsync("asset_base");
        op.Completed += OnRetrieveAssetPack;
    }

    private void OnRetrieveAssetPack(PlayAssetPackRequest request) {
        var location = request.GetAssetLocation("ui/prefabs/launch.unity3d");
        var assetBundle = AssetBundle.LoadFromFile(location.Path, 0, location.Offset);
        var asset = assetBundle.LoadAsset<GameObject>("Launch");
        var instance = Instantiate(asset, baseRoot);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;
        instance.transform.localScale = Vector3.one;
    }
}