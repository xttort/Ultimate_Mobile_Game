using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject[] roads; // Массив дорог
    public float roadLength = 10f; // Длина одной дороги
    public float roadSpeed = 10f; // Скорость движения дорог

    void Update()
    {
        // Перемещение дорог вперёд
        foreach (GameObject road in roads)
        {
            road.transform.Translate(Vector3.back * roadSpeed * Time.deltaTime);

            // Если дорога ушла за пределы, переместите её вперёд
            if (road.transform.position.z < -roadLength)
            {
                Vector3 newPosition = road.transform.position;
                newPosition.z += roadLength;
                road.transform.position = newPosition;
            }
        }
    }
}