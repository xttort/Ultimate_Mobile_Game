using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // настройки спавна
    public float spawnAreaX = 18f; // ширина области спавна по оси x
    public float minForce = 0.001f; // минимальная сила броска
    public float spawnRate = 1f; // частота спавна (раз в секунду)
    public float maxForce = 0.005f; // максимальная сила броска
    public GameObject[] objects; // массив префабов объектов для спавна
    public int maxActiveObjects = 10; // максимальное количество активных объектов

    private List<GameObject> objectPool = new List<GameObject>(); // пул объектов для переиспользования
    private float timer = 0f; // таймер для отслеживания времени спавна

    private void Start()
    {
        // создаем пул объектов при старте игры
        for (int i = 0; i < maxActiveObjects; i++)
        {
            // выбираем случайный префаб из массива и создаем его
            GameObject obj = Instantiate(objects[Random.Range(0, objects.Length)], Vector3.zero, Quaternion.identity);
            obj.SetActive(false); // деактивируем объект
            objectPool.Add(obj); // добавляем в пул
        }
    }

    private void Update()
    {
        timer += Time.deltaTime; // увеличиваем таймер

        // проверяем, прошло ли достаточно времени для спавна
        if (timer >= spawnRate)
        {
            TrySpawnObject(); // пытаемся заспавнить объект
            timer = 0f; // сбрасываем таймер
        }
    }

    void TrySpawnObject()
    {
        // ищем первый неактивный объект в пуле
        GameObject objToSpawn = objectPool.Find(obj => !obj.activeInHierarchy);

        // если нашли неактивный объект
        if (objToSpawn != null)
        {
            // устанавливаем случайную позицию спавна
            objToSpawn.transform.position = GetRandomSpawnPosition();
            objToSpawn.SetActive(true); // активируем объект

            // получаем компонент Rigidbody для физики
            Rigidbody rb = objToSpawn.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // сбрасываем физические параметры
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // применяем случайную силу вверх
                float force = Random.Range(minForce, maxForce);
                rb.AddForce(new Vector3(0, 1, 0) * force, ForceMode.Impulse);
            }
        }
    }

    // генерирует случайную позицию для спавна
    Vector3 GetRandomSpawnPosition()
    {
        // случайное положение по x в пределах заданной ширины
        float x = Random.Range(-spawnAreaX / 2, spawnAreaX / 2);
        // возвращаем позицию с текущей y-координатой спавнера
        return new Vector3(x, transform.position.y, 0);
    }
}