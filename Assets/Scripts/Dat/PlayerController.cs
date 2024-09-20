using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Thuộc tính nhân vật")]
    public float speed = 5f;
    public float runSpeed = 8f; // Tốc độ khi chạy
    public float jumpForce = 10f;

    public bool freeze = false; // Biến để cấm nhân vật di chuyển
    private float animSpeed = 0f;
    public bool isRunning = false;
    Rigidbody rb;
    Animator animator;
    Coroutine runningCoroutine; // Lưu lại Coroutine để có thể dừng khi cần

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
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
        float ipVertical = Input.GetAxis("Vertical");
        float ipHorizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ipHorizontal, 0, ipVertical);

        RotateCharacter(move);

        if (move != Vector3.zero)
        {
            animSpeed = isRunning ? 2 : 1;
        }
        else
        {
            animSpeed = 0;
        }

        animator.SetFloat("Speed", animSpeed);

        rb.velocity = move * speed + new Vector3(0, rb.velocity.y, 0); // Di chuyển bằng Rigidbody.velocity
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
