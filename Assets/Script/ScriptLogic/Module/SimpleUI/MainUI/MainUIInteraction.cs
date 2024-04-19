using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIInteraction : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        Debug.Log("OnPlayButtonClicked!");
        GameManager.Instance.StartBatte();
        // GameManager.Instance.StartBatte();
    }
    public void OnBagButtonClicked()
    {
        Debug.Log("OnBagButtonClicked!");
    }
    public void OnSettingsButtonClicked()
    {
        Debug.Log("OnSettingsButtonClicked!");
    }
}
