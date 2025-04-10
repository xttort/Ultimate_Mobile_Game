using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class demo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textDemo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Scene") == 4)
            textDemo.SetActive(true);
        else
            textDemo.SetActive(false);

    }
}
