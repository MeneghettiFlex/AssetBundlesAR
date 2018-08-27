using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loading circle behaviour from https://www.salusgames.com/2017/01/08/circle-loading-animation-in-unity3d/
/// </summary>
public class LoadingCircleBehaviour : MonoBehaviour
{
    enum Type { Default, PingPong, Static };
    [SerializeField] Type type;
    [SerializeField] bool rotate;

    RectTransform rectComponent;
    Image image;
    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] float fillSpeed = 1f;
    bool isFilling;

    void Awake()
    {
        rectComponent = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {
        isFilling = true;
        image.fillAmount = 0;
        rectComponent.rotation = Quaternion.identity;

        if (rotate) StartCoroutine(Rotate());
        switch (type)
        {
            case Type.Default:
                StartCoroutine(DefaultType());
                break;
            case Type.PingPong:
                StartCoroutine(PingPongType());
                break;
            case Type.Static:
                break;

        }
    }

    IEnumerator Rotate()
    {
        while(rotate)
        {
            rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator DefaultType()
    {
        while (true)
        {
            if (isFilling)
            {
                image.fillAmount += fillSpeed * Time.deltaTime;
                if (image.fillAmount >= 1)
                {
                    isFilling = !isFilling;
                    image.fillClockwise = !image.fillClockwise;
                }
            }
            else
            {
                image.fillAmount -= fillSpeed * Time.deltaTime;
                if (image.fillAmount <= 0)
                {
                    isFilling = !isFilling;
                    image.fillClockwise = !image.fillClockwise;
                }
            }
            yield return null;
        }
    }

    IEnumerator PingPongType()
    {
        while (true)
        {
            if (isFilling)
            {
                image.fillAmount += fillSpeed * Time.deltaTime;
                if (image.fillAmount >= 1)
                {
                    isFilling = !isFilling;
                }
            }
            else
            {
                image.fillAmount -= fillSpeed * Time.deltaTime;
                if (image.fillAmount <= 0)
                {
                    isFilling = !isFilling;
                }
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}