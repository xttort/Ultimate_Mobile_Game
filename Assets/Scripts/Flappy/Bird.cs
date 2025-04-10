using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Подключаем пространство имён для работы со сценами
using UnityEngine;

public class Bird : MonoBehaviour
{
    //Сила прыжка и скорость передвижения
    public float jumpForce = 10f;
    public float speedForward = 5f;
    public float fallSpeed = 2f;

    public GameObject slideSoundPrefab; //Префаб со звуком слайда

    //Компонет RigidBody
    private Rigidbody rb;

    void Start()
    {
        Time.timeScale = 1f;
        //Компонет RigidBody получение
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Функция для полёта вперед
        FlyForward();



        // Проверка касаний на мобильных устройствах
        if (Input.touchCount > 0) // Если есть хотя бы одно касание
        {
            Touch touch = Input.GetTouch(0); // Получаем первое касание
            if (touch.phase == TouchPhase.Began) // Если касание только началось
            {
                Jump();
                GetComponent<Bird>().PlaySound();
            }
        }

        //падение вниз
        FallDown();
    }

    void PlaySound()
    {
        if (slideSoundPrefab != null)
        {
            // Создаём экземпляр звука и удаляем его после проигрывания
            GameObject soundInstance = Instantiate(slideSoundPrefab, transform.position, Quaternion.identity);
            Destroy(soundInstance, 0.5f);
        }
    }

    public void RestartCurrentScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }

    void FallDown()
    {
        // Постоянное падение вниз
        if (rb.velocity.y > -fallSpeed)
        {
            rb.velocity -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void FlyForward()
    {
        rb.velocity = new Vector2(speedForward, rb.velocity.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверяем столкновение с стеной
        if (collision.gameObject.CompareTag("Wall"))
        {
            RestartCurrentScene();
        }
    }

}
