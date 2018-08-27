using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;

namespace FlexAssetBundle
{
    public class AssetBundleLoader : AssetBundleBaseLoader
    {
        [SerializeField] string[] bundlesToLoad;

        public void LoadAssets()
        {
            if (isLoading) return;
            StartCoroutine(LoadMyAssets());
        }

        bool hasFinishedLoading = false;
        string[] loadedAssetsNames;
        WaitForSeconds initializationWaitingTime = new WaitForSeconds(.5f);
        IEnumerator LoadMyAssets()
        {
            AssetBundleController.instance.FireBundleLoadStart();
            yield return Initialize();

            isLoading = true;

            AssetBundleAsync bundleAsync;

            for (int i = 0; i < bundlesToLoad.Length; ++i)
            {
                if (string.IsNullOrEmpty(bundlesToLoad[i])) continue;

                if (!abm.IsVersionCached(bundlesToLoad[i]))
                {
                    bundleAsync = abm.GetBundleAsync(bundlesToLoad[i]);
                    yield return bundleAsync;

                    if (bundleAsync.AssetBundle != null)
                    {
                        Instantiate(bundleAsync.AssetBundle.LoadAsset(bundleAsync.AssetBundle.GetAllAssetNames()[0]), this.transform);
                        abm.UnloadBundle(bundleAsync.AssetBundle);
                    }

                    abm.Dispose();

                    if (!bundleAsync.Failed)
                    {
                        AssetBundleController.instance.FireBundleLoadEnd();
                    }
                    else
                        AssetBundleController.instance.ShowError();
                }
                else
                {
                    AssetBundleController.instance.FireBundleLoadEnd();
                }
                hasFinishedLoading = true;
                isLoading = false;
            }
        }
    }
}