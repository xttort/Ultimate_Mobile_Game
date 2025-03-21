using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float spawnAreaX = 18f;
    public float minForce = 100f;
    public float spawnRate = 1f;
    public float maxForce = 300f;
    public GameObject[] objects;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            SpawnObjects();
            timer = 0f;
        }
    }

    void SpawnObjects()
    {
        GameObject fruitPrefab = objects[Random.Range(0, objects.Length)];

        // Создаем фрукт на сцене
        GameObject fruit = Instantiate(fruitPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // Придаем фрукту случайную силу броска вверх
        Rigidbody rb = fruit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float force = Random.Range(minForce, maxForce);
            rb.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Генерируем случайную позицию по оси X в пределах spawnAreaWidth
        float x = Random.Range(-spawnAreaX / 2, spawnAreaX / 2);
        return new Vector3(x, transform.position.y, 0);
    }

}
