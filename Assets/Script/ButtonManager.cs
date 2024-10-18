using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public int nbScene;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        GameManager.instance.Play(nbScene);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit ?");
    }
}
