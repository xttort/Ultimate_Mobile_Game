using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Получаем первое касание

            // Проверяем фазу касания (начало касания)
            if (touch.phase == TouchPhase.Began)
            {
                // Преобразуем позицию касания в луч (ray) в мировых координатах
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                // Проверяем, есть ли объект в точке касания
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && (hit.collider.gameObject.CompareTag("Fruit") || hit.collider.gameObject.CompareTag("Bomb")))
                {
                    // Если объект обнаружен, удаляем его
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Объект уничтожен: " + hit.collider.name);
                }
            }
        }
    }
}
