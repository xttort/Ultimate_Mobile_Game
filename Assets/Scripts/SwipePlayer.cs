using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePlayer : MonoBehaviour
{
    public float roadDistance = 0.6f;
    private int roadCur = 2;
    private Vector2 touchStartPos;


    // Update is called once per frame
    void Update()
    {
        //������� ��� ����������� (�����)
        HandTouch();    

    }

    void HandTouch()
    {
        //�������� ���� �� �������
        if (Input.touchCount > 0)
        {
            //��������� ���� � ������ �������
            Touch touch = Input.GetTouch(0);
            //��������� �������
            switch (touch.phase)
            {
                case TouchPhase.Began://������ ������
                    touchStartPos = touch.position; //��������� ������� �������
                    break;

                case TouchPhase.Ended://����� ������
                    Vector2 touchEndPos = touch.position; //�������� ������� �������
                    float swipeDistance = touchEndPos.x - touchStartPos.x;

                    if (Mathf.Abs(swipeDistance) > 50f) // ����������� ��������� ��� ������
                    {
                        if (swipeDistance > 0)
                        {
                            ChangeRoad(1); // ����� ������
                        }
                        else
                        {
                            ChangeRoad(-1); // ����� �����
                        }
                    }
                    break;
            }
        }
    }
    void ChangeRoad(int direction)
    {
        int newRoad = roadCur + direction;
        //��������� ������ �� �������
        if (newRoad < 0 || newRoad > 2)
            return;

        roadCur = newRoad;

        Vector3 newPosition = transform.position;// ��������� ����� �������
        newPosition.x = (roadCur - 1) * roadDistance; // -1, 0, 1
        transform.position = newPosition;
    }
}
