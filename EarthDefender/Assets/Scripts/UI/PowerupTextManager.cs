using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTextManager : MonoBehaviour
{
    public void PopUpText(string text)
    {
        StartCoroutine(PopupText(text));
    }

    IEnumerator PopupText(string text)
    {
        GetComponent<Text>().text = text;
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<Text>().text = "";
    }
}
