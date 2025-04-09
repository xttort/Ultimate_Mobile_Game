using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Подключаем пространство имён для работы со сценами
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Camera playerCam;
    public float roadDistance = 0.6f; // Расстояние между дорогами
    public float swipeThreshold = 25f; // Минимальная дистанция для свайпа
    public float forwardSpeed = 5f; // Скорость движения вперёд
    public float jumpForce = 10f; // Сила прыжка
    public float slideDuration = 1f; // Длительность подката
    public float fastFallGravityMultiplier = 2f; // Множитель гравитации для ускоренного падения
    public string code;
    public int score = 0;
    public TMP_Text scoreTxtPanel;


    public GameObject slideSoundPrefab; //Префаб со звуком слайда

    [Header("Trigger References")]//тригеры для проверки объекта справа и слева
    public Trigger leftTrigger;
    public Trigger rightTrigger;

    private int currentLane = 1; // Текущая дорога (0 - левая, 1 - центральная, 2 - правая)
    private Vector2 touchStartPos; // Начальная позиция касания
    private bool isSwiping = false; // Флаг для отслеживания свайпа
    private bool isJumping = false; // Флаг для отслеживания прыжка
    private bool isSliding = false; // Флаг для отслеживания подката
    private bool isFastFalling = false; // Флаг для ускоренного падения

    private Rigidbody rb; // Компонент Rigidbody для физики
    private Vector3 originalScale; // Исходный размер персонажа
    private float originalGravityScale; // Исходная гравитация

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        originalScale = transform.localScale; // Сохраняем исходный размер персонажа
        originalGravityScale = Physics.gravity.y; // Сохраняем исходную гравитацию
    }

    void Update()
    {

        MoveForward(); // Движение вперёд
        HandleTouchInput(); // Обработка свайпов

        // Ускоренное падение
        if (isFastFalling)
        {
            ApplyFastFall();
        }
    }

    //oncl collectDust()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<dust>())
        {
            //Debug.Log("!");
            score++;
            scoreTxtPanel.text = "score:"+ score.ToString();
            Destroy(other.gameObject);
        }

    }
    void PlaySlideSound()
    {
        if (slideSoundPrefab != null)
        {
            // Создаём экземпляр звука и удаляем его после проигрывания
            GameObject soundInstance = Instantiate(slideSoundPrefab, transform.position, Quaternion.identity);
            Destroy(soundInstance, 3f);
        }
    }

        // Метод для перезагрузки текущей сцены
        public void RestartCurrentScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }

    // Движение вперёд
    void MoveForward()
    {
        rb.velocity = new Vector3(transform.forward.x * forwardSpeed, rb.velocity.y, transform.forward.z * forwardSpeed);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed); старое
    }

    // Обработка свайпов
    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && forwardSpeed > 0)//двигаемся когда есть скорость
        {
            Touch touch = Input.GetTouch(0); // Получаем информацию о касании

            switch (touch.phase)
            {
                case TouchPhase.Began: // Начало касания
                    touchStartPos = touch.position; // Запоминаем начальную позицию
                    isSwiping = true; // Начинаем отслеживать свайп
                    break;

                case TouchPhase.Moved: // Движение пальца
                    if (isSwiping)
                    {
                        Vector2 touchCurrentPos = touch.position; // Текущая позиция касания
                        Vector2 swipeDelta = touchCurrentPos - touchStartPos; // Вектор свайпа

                        // Проверяем, превышает ли свайп минимальную дистанцию
                        if (swipeDelta.magnitude > swipeThreshold)
                        {
                            // Определяем направление свайпа
                            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                            {
                                // Горизонтальный свайп (влево/вправо) с проверкой есть ли объект слева и справа
                                if (swipeDelta.x > 0 && !rightTrigger.notMove)
                                {
                                    ChangeLane(1); // Свайп вправо
                                    GetComponent<Player>().PlaySlideSound();
                                }
                                else if(swipeDelta.x < 0 && !leftTrigger.notMove)
                                {
                                    ChangeLane(-1); // Свайп влево
                                    GetComponent<Player>().PlaySlideSound();
                                }
                            }
                            else
                            {
                                // Вертикальный свайп (вверх/вниз)
                                if (swipeDelta.y > 0 && !isJumping)
                                {
                                    Jump(); // Свайп вверх (прыжок)
                                    GetComponent<Player>().PlaySlideSound();
                                }
                                else if (swipeDelta.y < 0)
                                {
                                    if (isJumping)
                                    {
                                        FastFall(); // Свайп вниз в воздухе (ускоренное падение)
                                        GetComponent<Player>().PlaySlideSound();
                                    }
                                    else if (!isSliding)
                                    {
                                        Slide(); // Свайп вниз на земле (подкат)
                                        GetComponent<Player>().PlaySlideSound();
                                    }
                                }
                            }
                            isSwiping = false; // Завершаем свайп
                        }
                    }
                    break;

                case TouchPhase.Ended: // Конец касания
                    isSwiping = false; // Завершаем свайп
                    break;
            }
        }
    }

    // Переключение между дорогами
    void ChangeLane(int direction)
    {
        int newLane = currentLane + direction;

        // Проверяем, чтобы не выйти за пределы дорог
        if (newLane < 0 || newLane > 2)
            return;

        currentLane = newLane;

        // Вычисляем новую позицию по оси X
        //Vector3 newPosition = transform.position;старое
        //newPosition.x = (currentLane - 1) * roadDistance; // -1, 0, 1
        //transform.position = newPosition;

        transform.position = new Vector3(transform.position.x + transform.right.x*direction*roadDistance, transform.position.y, transform.position.z + transform.right.z*direction * roadDistance);
        //switch (direction)
        //{
        //    case -1:
        //        transform.position = new Vector3(transform.position.x + transform.right.x , transform.position.y, transform.position.z + transform.right.z);
        //        break;
        //    case 1:
        //        break;


        //}
    }

    // Прыжок
    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Сбрасываем вертикальную скорость
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Применяем силу для прыжка
        }
    }

    // Ускоренное падение
    void FastFall()
    {
        if (!isFastFalling)
        {
            isFastFalling = true;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z); // Уменьшаем вертикальную скорость
        }
    }

    // Подкат
    void Slide()
    {
        if (!isSliding)
        {
            isSliding = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.2f, originalScale.z); // Уменьшаем высоту персонажа
            rb.velocity += Vector3.up * Physics.gravity.y * (1000 - 1) * Time.deltaTime;
            Invoke("ResetSlide", slideDuration); // Сбрасываем подкат через указанное время
        }
    }

    // Сброс подката
    void ResetSlide()
    {
        transform.localScale = originalScale; // Восстанавливаем исходный размер
        isSliding = false;
    }

    // Применение ускоренного падения
    void ApplyFastFall()
    {
        rb.velocity += Vector3.up * Physics.gravity.y * (fastFallGravityMultiplier - 1) * Time.deltaTime;
    }

    // Обработка столкновений
    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, приземлился ли персонаж
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isFastFalling = false;
        }

        // Проверяем столкновение с барьером
        if (collision.gameObject.CompareTag("Barrier"))
        {
            RestartCurrentScene();
        }
    }
}