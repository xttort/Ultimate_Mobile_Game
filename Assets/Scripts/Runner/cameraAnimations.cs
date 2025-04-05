using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAnimations : MonoBehaviour
{
    Animator cameraAnimation;
    private Vector2 touchStartPos; // Начальная позиция касания
    private bool isSwiping = false; // Флаг для отслеживания свайпа
    public float swipeThreshold = 25f; // Минимальная дистанция для свайпа
    int pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        cameraAnimation = GetComponent<Animator>();
    }

    void swipeAnim()
    {
        if (Input.touchCount > 0)
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
                                // Горизонтальный свайп (влево/вправо)
                                if (swipeDelta.x > 0)
                                {
                                    if (pos != 1)
                                    {
                                        transform.localPosition = new Vector3(0, 0, 0);
                                        transform.localRotation =  Quaternion.Euler (0, 0, 0);
                                        cameraAnimation.SetTrigger("right"); ;
                                        pos += 1;
                                    }
                                }
                                else
                                {
                                    
                                    if (pos != -1)
                                    {
                                        transform.localPosition = new Vector3(0, 0, 0);
                                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                                        cameraAnimation.SetTrigger("left");
                                        pos -= 1;
                                    }
                                }
                            }

                            isSwiping = false; // Завершаем свайп
                        }
                    }
                    break;

                case TouchPhase.Ended: // Конец касания
                    isSwiping = false; // Завершаем свайп
                    //cameraAnimation.SetBool("right", false);
                    //cameraAnimation.SetBool("left", false);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        swipeAnim();
    }
}
