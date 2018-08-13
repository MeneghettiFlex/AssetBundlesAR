using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextFromParentName : MonoBehaviour
{
    void Awake() // Just a helper to update the text labels automaticaly.
    {
        this.GetComponent<TextMeshProUGUI>().text = this.transform.parent.name;
    }
}