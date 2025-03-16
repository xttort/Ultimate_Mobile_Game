using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //public Transform player; // —сылка на персонажа
    public int cameraViewType = 1;
    public Transform backTransform; // —мещение камеры вид сзади
    public Transform mapTransform; // —мещение камеры вид сверху - обзор на карту

    void CopyTransform(Transform newTransform)// копирует поворот и позицию пустой точки дл€ камеры
    {
        gameObject.transform.position = newTransform.position;
        gameObject.transform.rotation = newTransform.rotation;
    }
    void LateUpdate()
    {
        switch(cameraViewType)//выбор типа вида - сзади - 1, сверху - 2
        {
            case 1:
                CopyTransform(backTransform);
                break;
            case 2:
                CopyTransform(mapTransform);
                break;
        }

    }
}
