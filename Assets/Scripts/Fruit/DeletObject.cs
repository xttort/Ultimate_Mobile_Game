using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Подключаем пространство имён для работы со сценами
using UnityEngine;

public class DeletObject : MonoBehaviour
{
    public int health = 3;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Fruit"))
            health--;
        Destroy(col.gameObject);
        if(health <= 0)
        {
            health = 3;
            RestartCurrentScene();
        }
    }

    public void RestartCurrentScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }
}
