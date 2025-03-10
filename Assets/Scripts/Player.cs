using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float roadDistance = 0.6f; // Расстояние между дорогами
    public float swipeDistation = 25f; // Минимальная дистанция для свайпа
    public float jumpForce = 5f; // Сила прыжка
    public float slideTime = 1f; // Длительность подката
    public float fastFallGravityMultiplier = 2f; // Множитель гравитации для ускоренного падения

    private int roadCur = 1; // Текущая дорога (0 - левая, 1 - центральная, 2 - правая)
    private Vector2 touchStartPos;
    private bool isSwiping = false; // Флаг для отслеживания свайпа
    private bool isJumping = false; // Флаг для отслеживания прыжка
    private bool isSliding = false; // Флаг для отслеживания подката
    private bool isFastFalling = false; // Флаг для ускоренного падения

    private Rigidbody rb; // Компонент Rigidbody (для прыжка)
    private Vector3 originalScale; // Исходный размер персонажа
    private float originalGravityScale; // Исходная гравитация

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        originalGravityScale = Physics.gravity.y;
    }

    void Update()
    {
        // Функция для перемещения (свайп)
        HandTouch();

        // Ускоренное падение
        if (isFastFalling)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fastFallGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void HandTouch()
    {
        // Проверка, есть ли касание
        if (Input.touchCount > 0)
        {
            // Получение информации о первом касании
            Touch touch = Input.GetTouch(0);

            // Состояние касания
            switch (touch.phase)
            {
                case TouchPhase.Began: // Начало свайпа
                    touchStartPos = touch.position; // Начальная позиция касания
                    isSwiping = true; // Начатие свайпа
                    break;

                case TouchPhase.Moved: // Движение пальца
                    if (isSwiping)
                    {
                        Vector2 touchCurrentPos = touch.position; // Текущая позиция касания
                        Vector2 swipeDelta = touchCurrentPos - touchStartPos;

                        // Проверерка на превышает ли свайп минимальную дистанцию
                        if (swipeDelta.magnitude > swipeDistation)
                        {
                            // Определение направления свайпа
                            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                            {
                                // Горизонтальный свайп (влево/вправо)
                                if (swipeDelta.x > 0)
                                {
                                    ChangeRoad(1); // Свайп вправо
                                }
                                else
                                {
                                    ChangeRoad(-1); // Свайп влево
                                }
                            }
                            else
                            {
                                // Вертикальный свайп (вверх/вниз)
                                if (swipeDelta.y > 0 && !isJumping)
                                {
                                    Jump(); // Свайп вверх (прыжок)
                                }
                                else if (swipeDelta.y < 0)
                                {
                                    if (isJumping)
                                    {
                                        FastFall(); // Свайп вниз в воздухе (ускоренное падение)
                                    }
                                    else if (!isSliding)
                                    {
                                        Slide(); // Свайп вниз на земле (подкат)
                                    }
                                }
                            }
                            isSwiping = false; // Завершение свайпа
                        }
                    }
                    break;

                case TouchPhase.Ended: // Конец свайпа
                    isSwiping = false; // Завершение свайпа
                    break;
            }
        }
    }

    void ChangeRoad(int direction)
    {
        int newRoad = roadCur + direction;
        // Проверка выхода за пределы
        if (newRoad < 0 || newRoad > 2)
            return;

        roadCur = newRoad;

        Vector3 newPosition = transform.position; // Получение новой позиции
        newPosition.x = (roadCur - 1) * roadDistance; // -1, 0, 1
        transform.position = newPosition;
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Сила для прыжка
        }
    }

    void FastFall()
    {
        if (!isFastFalling)
        {
            isFastFalling = true;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z); // Уменьшаем вертикальную скорость
        }
    }

    void Slide()
    {
        if (!isSliding)
        {
            isSliding = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z); // Уменьшение высоты персонажа
            Invoke("ResetSlide", slideTime); // Сбрасывание флага подката через указанное время
        }
    }

    void ResetSlide()
    {
        transform.localScale = originalScale; // Восстанавление исходного размера
        isSliding = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Проверка, приземлился ли персонаж
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isFastFalling = false;
        }
    }
}