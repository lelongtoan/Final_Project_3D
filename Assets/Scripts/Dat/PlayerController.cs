using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Thuộc tính nhân vật")]
    public float speed = 5f;
    public float runSpeed = 8f; // Tốc độ khi chạy
    //public float jumpForce = 10f;

    public bool freeze = false; // Biến để cấm nhân vật di chuyển
    private float animSpeed = 0f;
    public bool isRunning = false;
    Rigidbody rb;
    Animator animator;
    Coroutine runningCoroutine; // Lưu lại Coroutine để có thể dừng khi cần
    public Camera camera;


    [SerializeField] private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        PlayerInfor playerInfo = gameObject.GetComponent<PlayerInfor>();

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (playerInfo.mp > 0)
            {
                if (!isRunning)
                {
                    isRunning = true;
                    speed = runSpeed;
                    animator.SetBool("IsRun", true);
                    runningCoroutine = StartCoroutine(SubtractValue());
                }
                else
                {
                    StopRunning();
                }
            }
            else
            {
                Debug.Log("Not enough Mana");
                StopRunning();
            }
        }

        if (!freeze)
        {
            HandleMovement();
        }

    }

    public void HandleMovement()
    {
        // Lấy giá trị từ joystick hoặc bàn phím
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");

        // Điều chỉnh độ nhạy cho joystick
        float joystickSensitivity = 1.5f; // Tăng hệ số để joystick mạnh hơn

        // Tạo vector di chuyển, nếu là joystick thì nhân với độ nhạy
        Vector3 move = new Vector3(ipHorizontal, 0, ipVertical);

        // Nếu di chuyển bằng joystick, nhân với hệ số để tăng tốc độ
        //if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        //{
        //    move *= joystickSensitivity;
        //}


        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        Vector3 desired = (forward * ipVertical + right * ipHorizontal) * speed;


        // Xoay nhân vật theo hướng di chuyển
        RotateCharacter(desired);

        // Cập nhật animation dựa trên tốc độ
        if (move != Vector3.zero)
        {
            animSpeed = isRunning ? 2 : 1;
        }
        else
        {
            animSpeed = 0;
        }

        animator.SetFloat("Speed", animSpeed);

        // Di chuyển nhân vật
        rb.MovePosition(rb.position + desired * Time.fixedDeltaTime);
        
    }

    public void RotateCharacter(Vector3 playerMovementInput)
    {
        if (playerMovementInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerMovementInput);
            transform.rotation = rotation;
        }
    }

    IEnumerator SubtractValue()
    {
        while (gameObject.GetComponent<PlayerInfor>().mp > 0)
        {
            yield return new WaitForSeconds(1f); // Đợi 1 giây
            gameObject.GetComponent<PlayerInfor>().PlayerUseSkill(5);

            if (gameObject.GetComponent<PlayerInfor>().mp <= 0)
            {
                StopRunning();
            }
        }
    }

    // Hàm để dừng chạy
    private void StopRunning()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        isRunning = false;
        speed = 5f; // Khôi phục lại tốc độ ban đầu
        animator.SetBool("IsRun", false);
    }
}
