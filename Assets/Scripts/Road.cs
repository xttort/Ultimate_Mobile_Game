using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject[] roads; // ������ �����
    public float roadLength = 10f; // ����� ����� ������
    public float roadSpeed = 10f; // �������� �������� �����

    void Update()
    {
        // ����������� ����� �����
        foreach (GameObject road in roads)
        {
            road.transform.Translate(Vector3.back * roadSpeed * Time.deltaTime);

            // ���� ������ ���� �� �������, ����������� � �����
            if (road.transform.position.z < -roadLength)
            {
                Vector3 newPosition = road.transform.position;
                newPosition.z += roadLength;
                road.transform.position = newPosition;
            }
        }
    }
}