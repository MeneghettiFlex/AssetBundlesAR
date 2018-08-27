using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;

namespace FlexAssetBundle
{
    public class AssetBundleBaseLoader : MonoBehaviour
    {
        protected AssetBundleManager abm;
        protected bool isInitialized;
        protected bool isLoading;

        protected void Awake()
        {
            abm = new AssetBundleManager();
            Caching.ClearCache();
        }

        protected IEnumerator Initialize()
        {
            isInitialized = false;
#if UNITY_EDITOR
            abm.UseSimulatedUri();
            //abm.SetBaseUri("https://s3-sa-east-1.amazonaws.com/flexunitytests/AssetBundles");
#else
        abm.SetBaseUri("https://s3-sa-east-1.amazonaws.com/flexunitytests/AssetBundles");
#endif
            AssetBundleManifestAsync initAsync = abm.InitializeAsync();

            yield return initAsync;
            isInitialized = initAsync.Success;
        }
    }
}