using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float sesitivity = 500f;
    // ���콺 �ΰ���
    public float rotationX;
    // X���� ��ġ
    public float rotationY;
    // Y���� ��ġ
    void Start()
    {

    }

    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X");
        // ���콺 X�� �����Ӱ��� �޾Ƽ� mouseMoveX ������ ����

        float mouseMoveY = Input.GetAxis("Mouse Y");
        // ���콺 Y�� �����Ӱ��� �޾Ƽ� mouseMoveY ������ ����

        rotationY += mouseMoveX * sesitivity * Time.deltaTime;
        // rotationY ������ ���� rotationY + (mouseMoveX * ���콺 �ΰ��� * Time.deltaTime)

        rotationX += mouseMoveY * sesitivity * Time.deltaTime;
        // rotationX ������ ���� rotationX + (mouseMoveY * ���콺 �ΰ��� * Time.deltaTime)


        /*
         * �Ʒ��� if���� ���콺�� �̵��ϴ� �þ߰� ����� �þ�ó�� ���̱� �ϱ� ���� 
         */
        if (rotationX > 35f)
        {
            rotationX = 35f;
            // ���� 35�� �̻� �Ѿ�� ���ϰ� (���� ���ϰ� �������� �ʰ�)
        }

        if (rotationX < -30f)
        {
            rotationX = -30f;
            // �Ʒ��� 30�� �̻� �Ѿ�� ���ϰ� (���� ���ϰ� ���ٶ����� �ʰ�)
        }

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);
        // rotationX��, rotationY��, Z�� 0���� Vector3 �������� ��ȯ�ϰ�,
        // transform ���Ϸ����� ������.
    }
}