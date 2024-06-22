using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Transform cameraTransform;
    // Transform���� ī�޶� �����ӿ� ���� �޶����Ƿ�,�ش� ���� ī�޶� �Ѱ��ֱ� ����
    // CameraTransform ���� ����

    public CharacterController characterController;
    // CharacterController�� 3D ������Ʈ�� �����ϱ� ���� characterController ���� ����

    public float moveSpeed = 10f;
    // �̵� �ӵ�
    public float jumpSpeed = 10f;
    // ���� �ӵ�
    public float gravity = -20f;
    // �߷�
    public float yVelocity = 0;
    // Y�� ������

    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        // h ������ Ű������ ���ΰ� (��, ��) �� �о�� ����� �ѱ��.
        // ��, ��, A, D Ű

        float v = Input.GetAxis("Vertical");
        // v ������ Ű������ ���ΰ� (��, ��) �� �о�� ����� �ѱ��.
        // ��, ��, W, S Ű

        Vector3 moveDirection = new Vector3(h, 0, v);
        // (x��, y��, z�� = h ����, 0, v ����) ���� �о�� ���� Vector3���� ����
        // �ش� ���� Vector3 ������ moveDirection ������ �ѱ��.

        moveDirection = cameraTransform.TransformDirection(moveDirection);
        // moveDirection ���� ī�޶� ��ġ

        moveDirection *= moveSpeed;
        // �������� moveDirection ���� moveDirection * moveSpeed ���� ���� ���� ��.

        if (characterController.isGrounded)
        // ����, characterController�� ���� �پ��ִٸ�
        {
            yVelocity = 0;
            // y�� ������ ���� 0�̰�,
            if (Input.GetKeyDown(KeyCode.Space))
            // �����̽� �� Ű�� ���� ������ �ǽ��ϰ�,
            {
                yVelocity = jumpSpeed;
                // ����ڰ� ������ jumpSpeed ���� yVelocity ������ �Ѱܼ� ó���Ѵ�.
            }
        }

        yVelocity += (gravity * Time.deltaTime);
        // yVelocity ���� yVelocity + (�߷°� * Time.deltaTime)

        moveDirection.y = yVelocity;
        // ����� yVelocity ���� moveDirection.y (Y�� ������ ����) �� �Ѱ��ش�.

        characterController.Move(moveDirection * Time.deltaTime);
        // ���������� characterController�� �������� ���� * Time.deltaTime ��
    }
}
