using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Thuộc tính nhân vật")]
    float speed;
    public float Wallspeed = 5f;
    public float dashSpeed = 10f;
    public Vector3 dashDirection;

    public bool freeze = false; 
    private float animSpeed = 0f;
    public bool isRunning = false;
    Rigidbody rb;
    Animator animator;
    Coroutine runningCoroutine; 
    public Camera playercam;

    public GameObject speedEff;
    public GameObject runEff;
    Button run;
    [Header("Lướt")]
    public float dashDuration = 0.5f;
    private float dashTime;
    private bool isDashing = false;

    [SerializeField] private Joystick joystick;
    void Start()
    {
        speed = Wallspeed;
        run = GameObject.Find("Run").GetComponent<Button>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        run.onClick.AddListener(StartDash);
        runEff = Instantiate(speedEff, transform.position, Quaternion.identity);
        runEff.SetActive(false);
        runEff.transform.SetParent(transform);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            HandleDash();
        }
        else
        {
            HandleMovement();
        }
    }

    private bool IsMoving()
    {
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");

        // Kiểm tra nếu có bất kỳ giá trị nào từ joystick hoặc bàn phím
        return ipVertical != 0 || ipHorizontal != 0;
    }
    public void HandleDash()
    {
        if (dashTime > 0f)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTime -= Time.fixedDeltaTime;
        }
        else
        {
            StopDash();
        }
    }
    public void HandleMovement()
    {
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ipHorizontal, 0, ipVertical).normalized;
        Vector3 forward = playercam.transform.forward.normalized;
        Vector3 right = playercam.transform.right.normalized;
        forward.y = 0f;
        right.y = 0f;

        Vector3 desired = (forward * ipVertical + right * ipHorizontal) * speed;
        RotateCharacter(desired);

        animator.SetFloat("Speed", move.magnitude);
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

        while (playerInfo.manaPoint > 0 && IsMoving() && freeze != true)
        {
            playerInfo.PlayerUseSkill(5);
            yield return new WaitForSeconds(1f);

            if (playerInfo.manaPoint <= 0)
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

    private void OnDashClick()
    {
        if (!isDashing)
        {
            Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
            if (moveDirection != Vector3.zero)
            {
                dashDirection = moveDirection;
                isDashing = true;
                dashTime = dashDuration;

                //animator.SetTrigger("Dash");
            }
        }
    }

    public void SetEffSpeed(bool enable)
    {
        runEff.SetActive(enable);
    }
    public void StartDash()
    {
        dashTime = dashDuration;
        isDashing = true;
        speed = dashSpeed;
        runEff.SetActive(true);
    }
    public void StopDash()
    {
        isDashing = false;
        speed = Wallspeed;
        runEff.SetActive(false);
    }
}
