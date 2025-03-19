using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public float speed = 4f; //скорость движения
    public Transform startPos; //начальная позиция языка
    public Transform endPos; //конечная позиция языка
    public Transform birdPos; // позиция птицы
    public Transform trigerMoveStart; //позиция тригера старта
    public Transform trigerMoveEnd; //позиция тригера конца
    public float timeWait = 3f; //время ожидания в точке начала или конца
    

    private bool atack = false; //флаг атаки(выдвижение языка)
    private float time;
    Color someColor = new Color(0.5f, 0.2f, 0.25f); // создание нового цвета


    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", someColor);
        time = timeWait; //задание изначального времени
    }

    void Update()
    {
        //запуск логики только если был пройдет триггер запуска
        if (TongueIsActive())
        {
            //изменение цвета объекта
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", someColor);
            if (atack)//проверка на атаку
                StartAtack();//начало атаки
            else
                EndAtack();//конец атаки
        }
        
    }

    //функция для проверки перешёл ли игрок тригер начала движения языка
    bool TongueIsActive()
    {
        //проверка что ингрок находится между двумя тригерами
        if (birdPos.position.x >= trigerMoveStart.position.x && birdPos.position.x < trigerMoveEnd.position.x)
            return true;
        if (birdPos.position.x >= trigerMoveEnd.position.x)
            return false;
        return false;
    }

    void StartAtack()
    {
        //перемещение к концу движения
        transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime); 

        //проверка на достижение конца
        if (transform.position.z == endPos.position.z)
        {
            time -= Time.deltaTime;
            TimerOut(); //проверка прошло ли время
        }            
    }

    void EndAtack()
    {
        //пермещение к началу
        transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);

        //проверка на достижение начала
        if (transform.position.z == startPos.position.z)
        {
            time -= Time.deltaTime;
            TimerOut(); //проверка прошло ли время
        }
    }

    void TimerOut()
    {
        //запуск смены цвета за некоторое время до конца полного истечения времени
        if(time <= timeWait / 3)
        {
            if(atack)
                someColor = new Color(1, 1, 1);
            else
                someColor = new Color(0.5f, 0.2f, 0.25f);
        }
        if(time <= 0)
        {
            if (atack)
                atack = false;

            else
                atack = true;

            time = timeWait;
        }

    }
}