using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //public Transform player; // —сылка на персонажа
    public int cameraViewType = 1;
    public Transform backTransform; // —мещение камеры вид сзади
    public Transform mapTransform; // —мещение камеры вид сверху - обзор на карту
    

    private void Start()
    {

    }
    void CopyTransform(Transform newTransform)// копирует поворот и позицию пустой точки дл€ камеры
    {
        transform.position = newTransform.position; //new Vector3(Mathf.Lerp(transform.position.x, newTransform.position.x, newTransform.position.x/transform.position.x), newTransform.position.y, newTransform.position.z);
        transform.rotation = newTransform.rotation;
    }
    void LateUpdate()
    {


        switch (cameraViewType)//выбор типа вида - сзади - 1, сверху - 2
        {
            case 1:
                CopyTransform(backTransform);
                break;
            case 2:
                CopyTransform(mapTransform);
                break;
        }



    }

    void Update()
    {
        //if (gameObject.transform.position.x > backTransform.position.x && cameraAnimation.GetBool("left") == false)
        //{
        //    cameraAnimation.SetBool("left", true);
        //    Debug.Log("!L");
        //}

        //if (gameObject.transform.position.x < backTransform.position.x && cameraAnimation.GetBool("right") == false)
        //{
        //    cameraAnimation.SetBool("right", true);
        //}

        //if (gameObject.transform.position.x == backTransform.position.x && cameraAnimation.GetBool("right") == false && cameraAnimation.GetBool("left") == false)
        //{
        //    cameraAnimation.SetBool("right", false);
        //    cameraAnimation.SetBool("left", false);
        //}
        
    }
}
