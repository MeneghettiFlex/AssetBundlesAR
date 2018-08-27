using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AssetBundles;

namespace FlexAssetBundle
{
    public class AssetBundleSceneLoader : AssetBundleBaseLoader
    {
        [SerializeField] string bundleWithScene;

        public void LoadSceneAsync()
        {
            if (isLoading) return;
            StartCoroutine(LoadMyScene());
        }

        bool hasFinishedLoading = false;
        string loadedAssetsName;
        WaitForSeconds initializationWaitingTime = new WaitForSeconds(.5f);
        IEnumerator LoadMyScene()
        {
            AssetBundleController.instance.FireBundleLoadStart();
            yield return Initialize();

            isLoading = true;

            AssetBundleAsync bundleAsync;

            if (string.IsNullOrEmpty(bundleWithScene)) yield break;

            if (!abm.IsVersionCached(bundleWithScene))
            {
                bundleAsync = abm.GetBundleAsync(bundleWithScene);
                yield return bundleAsync;

                if (bundleAsync.AssetBundle != null)
                {
                    loadedAssetsName = bundleAsync.AssetBundle.GetAllScenePaths()[0];
                    SceneManager.LoadSceneAsync(loadedAssetsName);
                    abm.UnloadBundle(bundleAsync.AssetBundle);
                }

                abm.Dispose();

                if (!bundleAsync.Failed)
                {
                    AssetBundleController.instance.FireBundleLoadEnd();
                }
                else
                {
                    AssetBundleController.instance.ShowError();
                }
            }
            hasFinishedLoading = true;
            isLoading = false;
        }
    }
}