using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePlayer : MonoBehaviour
{
    public float roadDistance = 0.6f;
    private int roadCur = 2;
    private Vector2 touchStartPos;


    // Update is called once per frame
    void Update()
    {
        //функция для перемещения (свайп)
        HandTouch();    

    }

    void HandTouch()
    {
        //проверка есть ли касание
        if (Input.touchCount > 0)
        {
            //получение инфы о первом касании
            Touch touch = Input.GetTouch(0);
            //состояние касания
            switch (touch.phase)
            {
                case TouchPhase.Began://начало свайпа
                    touchStartPos = touch.position; //начальная позицая касания
                    break;

                case TouchPhase.Ended://клнец свайпа
                    Vector2 touchEndPos = touch.position; //конечная позиция касания
                    float swipeDistance = touchEndPos.x - touchStartPos.x;

                    if (Mathf.Abs(swipeDistance) > 50f) // минимальная дистанция для свайпа
                    {
                        if (swipeDistance > 0)
                        {
                            ChangeRoad(1); // свайп вправо
                        }
                        else
                        {
                            ChangeRoad(-1); // свайп влево
                        }
                    }
                    break;
            }
        }
    }
    void ChangeRoad(int direction)
    {
        int newRoad = roadCur + direction;
        //пороверка выхода за пределы
        if (newRoad < 0 || newRoad > 2)
            return;

        roadCur = newRoad;

        Vector3 newPosition = transform.position;// получение новой позиции
        newPosition.x = (roadCur - 1) * roadDistance; // -1, 0, 1
        transform.position = newPosition;
    }
}
