using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasMask : MonoBehaviour
{
    // Start is called before the first frame update
    public float plusTime = 10;
    private void OnTriggerEnter(Collider other)
    {

        try
        {
            other.GetComponent<PlayerTimer>().timer += plusTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);

        }
        catch
        {
            Debug.Log("Not player");
        }
    }
}
