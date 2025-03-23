using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class codeNumber : MonoBehaviour
{
    public string plusNum;
    public TMP_Text numText;
    private void Start()
    {
        numText.text = plusNum;
    }
    private void OnTriggerEnter(Collider other)
    {

        try
        {
            other.GetComponent<Player>().code += plusNum;
            transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);

        }
        catch
        {
            Debug.Log("Not player");
        }
    }
}
