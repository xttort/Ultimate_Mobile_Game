using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{

    public Transform spawnArea;

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
                    ReturnObjectToPool(hit.collider.gameObject);
                    Debug.Log("Объект уничтожен: " + hit.collider.name);
                }
            }
        }
    }

    void ReturnObjectToPool(GameObject obj)
    {
        // "Удаляем" объект, возвращая его в пул
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        obj.SetActive(false);

        // Перемещаем объект к месту спавна (но он останется неактивным)
        if (spawnArea != null)
        {
            obj.transform.position = spawnArea.position;
        }
    }
}
