using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Thuộc tính nhân vật")]
    public float speed = 5f;
    public float runSpeed = 8f;

    public bool freeze = false; 
    private float animSpeed = 0f;
    public bool isRunning = false;
    Rigidbody rb;
    Animator animator;
    Coroutine runningCoroutine; 
    public Camera camera;

    public GameObject speedEff;
    public GameObject runEff;
    Button run;

    [SerializeField] private Joystick joystick;
    void Start()
    {
        run = GameObject.Find("Run").GetComponent<Button>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        run.onClick.AddListener(OnRunClick);
        runEff = Instantiate(speedEff, transform.position, Quaternion.identity);
        runEff.SetActive(false);
        runEff.transform.SetParent(transform);

    }

    private void FixedUpdate()  
    {
        if (freeze == false)
        {
            HandleMovement();
        }
        if(isRunning && isRunning && IsMoving() && freeze == false)
        {
            if (runningCoroutine == null)
            {
                runningCoroutine = StartCoroutine(SubtractValue());
            }
        }
        else if (!IsMoving() && runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }
    private bool IsMoving()
    {
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");

        // Kiểm tra nếu có bất kỳ giá trị nào từ joystick hoặc bàn phím
        return ipVertical != 0 || ipHorizontal != 0;
    }
    public void HandleMovement()
    {
        // Lấy giá trị từ joystick hoặc bàn phím
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");

        // Điều chỉnh độ nhạy cho joystick
        float joystickSensitivity = 1.5f; // Tăng hệ số để joystick mạnh hơn

        // Tạo vector di chuyển, nếu là joystick thì nhân với độ nhạy
        Vector3 move = new Vector3(ipHorizontal, 0, ipVertical).normalized;

        // Nếu di chuyển bằng joystick, nhân với hệ số để tăng tốc độ
        //if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        //{
        //    move *= joystickSensitivity;
        //}


        Vector3 forward = camera.transform.forward.normalized;
        Vector3 right = camera.transform.right.normalized;
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
        PlayerInfor playerInfo = gameObject.GetComponent<PlayerInfor>();

        while (playerInfo.mp > 0 && IsMoving() && freeze != true)
        {
            playerInfo.PlayerUseSkill(5);
            yield return new WaitForSeconds(1f);

            if (playerInfo.mp <= 0)
            {
                SetEffSpeed(false);
                StopRunning();
                break;
            }
        }
    }

    private void StopRunning()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        isRunning = false;
        speed = 5f;
        animator.SetBool("IsRun", false);
    }

    private void OnRunClick()
    {
        PlayerInfor playerInfo = gameObject.GetComponent<PlayerInfor>();
        if (playerInfo.mp > 0 && freeze != true)
        {
            if (!isRunning)
            {
                SetEffSpeed(true);
                isRunning = true;
                speed = runSpeed;
                animator.SetBool("IsRun", true);
                if (runningCoroutine == null)
                {
                    runningCoroutine = StartCoroutine(SubtractValue());
                }
            }
            else
            {
                SetEffSpeed(false);
                StopRunning();
            }
        }
        else
        {
            Debug.Log("Not enough Mana");
            StopRunning();
        }
    }

    public void SetEffSpeed(bool enable)
    {
        runEff.SetActive(enable);
    }
}
