using UnityEngine;

public class Trigger : MonoBehaviour
{
    private bool isInsideGround = false; // Флаг для отслеживания состояния внутри "Ground"
    public bool notMove = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // Если мы вошли в первый "Ground", устанавливаем notMove в true
            if (!isInsideGround)
            {
                isInsideGround = true;
                notMove = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // Убедимся, что notMove остается true, пока мы находимся внутри "Ground"
            notMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // Если мы вышли из "Ground", сбрасываем флаг и состояние
            isInsideGround = false;
            notMove = false;
        }
    }
}
