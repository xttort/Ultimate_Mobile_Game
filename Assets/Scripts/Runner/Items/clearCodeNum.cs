using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCodeNum : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        try
        {
            other.GetComponent<Player>().code ="";
            transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        }
        catch
        {
            Debug.Log("Not player");
        }
    }
}
