using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeDirectionItem : MonoBehaviour
{
    public bool avtoRotate = true;

    public bool left = false;
    public bool right = false;
    public bool back = false;

    public float waitTime = 5;//время ожидания для выбора направления

    private float playersForwardSpeed;//коробка для сохранения скорости игрока
    public float waitTimer = 0;//коробка для подсчёта прошедшего времени таймера

    private Vector2 touchStartPos; // Начальная позиция касания
    private bool isSwiping = false; // Флаг для отслеживания свайпа
    public float swipeThreshold = 25f; // Минимальная дистанция для свайпа

    private Player player;
    //public Camera playerCam;
    void Start()
    {
        
    }

    void ChangeCameraView(int type)
    {
        player.playerCam.GetComponent<CameraMove>().cameraViewType = type;//берём из срипта игрока камеру, у неё берём скрипт CameraMove, меняем в нём тип отображения на 2 (вид сверху)
    }

    void StopInCollisionPLayer(Collision collision)
    {
        try//проверка на сталкновение с игроком
        {
            player = collision.gameObject.GetComponent<Player>();
            if(player.forwardSpeed != 0)
            {
                ChangeCameraView(2);
                playersForwardSpeed = player.forwardSpeed;//сохранение скорости до столкновения
                player.forwardSpeed = 0; // остановка игрока
            }
        }
        catch
        {
            Debug.Log("Not a player");
        }
    }

    void waitToGo(float time)
    {
        Debug.Log(waitTimer);
        waitTimer += Time.deltaTime;//изменение значения таймера
        //Debug.Log(waitTimer);

        if (waitTimer >= time)//если таймер отработал своё время
        {
            player.forwardSpeed = playersForwardSpeed;//возврящаем игроку скорость
            ChangeCameraView(1);//возращяем камеру на место
            transform.position = new Vector3(transform.position.x, transform.position.y - 6f, transform.position.z);//убираем вниз предмет (P.S. потом просто передвинем)(P.S.S чтобы не удалять и систему не грузить)
            waitTimer = 0;
        }
    }

    void ChangeDirection()
    {
        if (Input.touchCount > 0)
        {
            //Debug.Log("rotateTime");
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
                                    //ChangeLane(1); // Свайп вправо
                                    //player.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
                                    player.transform.Rotate(0, 90, 0, Space.World);
                                }
                                else
                                {
                                    //ChangeLane(-1); // Свайп влево
                                    //player.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
                                    player.transform.Rotate(0, -90, 0, Space.World);
                                }
                            }
                            else
                            {
                                // Вертикальный свайп (вверх/вниз)
                                if (swipeDelta.y > 0)
                                {
                                    //Jump(); // Свайп вверх (прыжок)
                                    //player.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
                                    //player.transform.Rotate(0, 180, 0, Space.World);
                                }
                                else if (swipeDelta.y < 0)
                                {
                                    //    if (isJumping)
                                    //    {
                                    //        FastFall(); // Свайп вниз в воздухе (ускоренное падение)
                                    //    }
                                    //    else if (!isSliding)
                                    //    {
                                    //        Slide(); // Свайп вниз на земле (подкат)
                                    //    }
                                }
                            }
                            isSwiping = false; // Завершаем свайп
                        }
                    }
                    break;

                case TouchPhase.Ended: // Конец касания
                    isSwiping = false; // Завершаем свайп
                    player.forwardSpeed = playersForwardSpeed;//возврящаем игроку скорость
                    ChangeCameraView(1);//возращяем камеру на место
                    transform.position = new Vector3(transform.position.x, transform.position.y - 6f, transform.position.z);//убираем вниз предмет (P.S. потом просто передвинем)(P.S.S чтобы не удалять и систему не грузить)
                    waitTimer = 0;
                    break;
            }
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        StopInCollisionPLayer(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        waitToGo(waitTime);
        //ChangeDirection();
    }

    private void Update()
    {
        if(waitTimer > 0)
        {
            ChangeDirection();
        }
    }
}
