using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Необходимо для работы с TextMeshPro

public class DeletObject : MonoBehaviour
{

    public GameObject slideSoundPrefab; //Префаб со звуком слайда

    [Header("Health Settings")]
    public int maxHealth = 3; // Максимальное количество жизней
    private int currentHealth; // Текущее количество жизней


    [Header("References")]
    public Transform respawnArea; // Область для респавна объектов
    public TMP_Text healthText; // Ссылка на TextMeshPro компонент

    private void Start()
    {
        currentHealth = maxHealth; // Инициализируем здоровье
        UpdateHealthUI(); // Обновляем интерфейс
    }

    public void PlaySound()
    {
        if (slideSoundPrefab != null)
        {
            // Создаём экземпляр звука и удаляем его после проигрывания
            GameObject soundInstance = Instantiate(slideSoundPrefab, transform.position, Quaternion.identity);
            Destroy(soundInstance, 3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем тег столкнувшегося объекта
        if (collision.gameObject.CompareTag("Fruit") || collision.gameObject.CompareTag("Bomb"))
        {
            if(collision.gameObject.CompareTag("Fruit"))
            {
                TakeDamage(); // Наносим урон
                GetComponent<DeletObject>().PlaySound();
            }
            ReturnObjectToPool(collision.gameObject); // Возвращаем объект в пул
        }
    }

    public void TakeDamage()
    {
        currentHealth--; // Уменьшаем здоровье
        UpdateHealthUI(); // Обновляем интерфейс

        if (currentHealth <= 0)
        {
            GameOver(); // Обрабатываем конец игры
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            // Обновляем текст с использованием TextMeshPro
            healthText.text = $"Health: {currentHealth}/{maxHealth}";

            // Меняем цвет текста при низком здоровье
            healthText.color = currentHealth <= 1 ? Color.red : Color.white;
        }
    }

    private void ReturnObjectToPool(GameObject obj)
    {
        // Сбрасываем физические параметры объекта
        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        obj.SetActive(false); // Деактивируем объект

        // Возвращаем объект в точку респавна
        if (respawnArea != null)
        {
            obj.transform.position = respawnArea.position;
        }
    }

    private void GameOver()
    {
        currentHealth = maxHealth; // Восстанавливаем здоровье
        UpdateHealthUI(); // Обновляем интерфейс
        RestartCurrentScene(); // Перезагружаем сцену
    }

    private void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}