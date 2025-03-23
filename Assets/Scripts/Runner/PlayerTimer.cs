using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Подключаем пространство имён для работы со сценами
using TMPro;
using UnityEngine;


public class PlayerTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 15;
    public TMP_Text timerUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerUI.text = (Mathf.Round(timer)).ToString();
        if(timer <0)
        {   
            // Получаем имя текущей сцены
            string currentSceneName = SceneManager.GetActiveScene().name;
            // Перезагружаем сцену
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
