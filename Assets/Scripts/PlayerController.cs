using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    [Header("Jump")]
    /*public float playerHeight;
    bool grounded;*/
    public float jumpForce;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
    }

    void Update()  // 컴퓨터마다 다르지만 대략 1초에 60번 실행
    {
        Move();                 // 1️ 키보드 입력에 따라 이동
        CameraRotation();       // 2️ 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 3️ 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전 

        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        /*if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }*/
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float moveDirX = Input.GetAxisRaw("Horizontal");
            float moveDirZ = Input.GetAxisRaw("Vertical");
            Vector3 moveHorizontal = transform.right * moveDirX;
            Vector3 moveVertical = transform.forward * moveDirZ;

            Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed * 2;

            myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
            return;
        }
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    void Jump()
    {
        myRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 힘을 가해 점프
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }


}