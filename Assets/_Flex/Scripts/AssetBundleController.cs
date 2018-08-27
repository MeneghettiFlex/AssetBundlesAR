using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleController : MonoBehaviour
{
    public GameObject errorText;

    [SerializeField] float canvasFadeSpeed = 1f;
    CanvasGroup loadingCanvas;
    bool isFading;
    bool hasLoadedAsset;

    #region Delegates
    public delegate void BundleLoadHandler();//(string bundleName);
    public BundleLoadHandler OnBundleLoadStart;
    public BundleLoadHandler OnBundleLoadEnd;

    public void FireBundleLoadStart()
    {
        if (OnBundleLoadStart != null) OnBundleLoadStart();
        StartCoroutine(ShowLoadingCanvas());
    }
    public void FireBundleLoadEnd()
    {
        if (OnBundleLoadEnd != null) OnBundleLoadEnd();
        StartCoroutine(HideLoadingCanvas());
    }

    public void ShowError()
    {
        errorText.SetActive(true);
    }
#endregion


    public static AssetBundleController instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        loadingCanvas = this.GetComponentInChildren<CanvasGroup>();
        loadingCanvas.gameObject.SetActive(false);
    }

    IEnumerator ShowLoadingCanvas()
    {
        while (isFading) yield return null;

        loadingCanvas.alpha = 0;
        loadingCanvas.gameObject.SetActive(true);
        isFading = true;

        while (true)
        {
            loadingCanvas.alpha += canvasFadeSpeed * Time.deltaTime;
            if (loadingCanvas.alpha >= 0.99f) break;

            yield return null;
        }

        loadingCanvas.alpha = 1f;
        isFading = false;
    }

    IEnumerator HideLoadingCanvas()
    {
        while (isFading) yield return null;

        loadingCanvas.alpha = 1;
        isFading = true;

        while (true)
        {
            loadingCanvas.alpha -= canvasFadeSpeed * Time.deltaTime;
            if (loadingCanvas.alpha <= 0.01f) break;

            yield return null;
        }

        loadingCanvas.alpha = 0f;
        loadingCanvas.gameObject.SetActive(false);
        isFading = false;
    }

    //public void ClearCache()
    //{
    //    if (Caching.ClearCache())
    //    {
    //        Debug.Log("Successfully cleaned the cache.");
    //    }
    //    else
    //    {
    //        Debug.Log("Cache is being used.");
    //    }
    //}
}