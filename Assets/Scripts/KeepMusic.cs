using UnityEngine;

public class KeepMusic : MonoBehaviour
{
    private static KeepMusic instance; // Статическая ссылка на экземпляр

    void Awake()
    {
        // Если экземпляр уже существует, уничтожаем новый объект
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Делаем неуничтожаемым
        }
    }
}