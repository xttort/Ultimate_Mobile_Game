using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // —сылка на персонажа
    public Vector3 offset; // —мещение камеры

    void Update()
    {
        transform.position = player.position + offset;
    }
}