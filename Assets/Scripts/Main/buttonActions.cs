using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonActions : MonoBehaviour
{
    public void exitGameAction()
    {
        Application.Quit();
    }
    public void resetDataAction()
    {
        PlayerPrefs.SetInt("Scene", 1);
        PlayerPrefs.Save();
    }
}
