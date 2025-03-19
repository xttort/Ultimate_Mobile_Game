using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // Метод для загрузки сцены по имени
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
