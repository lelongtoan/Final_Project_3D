using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    PlayerSkillData playerSkill;
    [Header("Thuộc tính nhân vật")]
    public float Wallspeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;

    public float dashTime;
    public bool isDashing = false;
    public Vector3 dashDirection;
    public float counDDash;
    public float manaDash;
    bool isCound = false;

    public bool freeze = false;
    private Rigidbody rb;
    private Animator animator;

    [Header("UI và hiệu ứng")]
    public GameObject speedEff;
    private GameObject runEff;
    public Button runButton;

    public Image runimage;

    [SerializeField] private Joystick joystick;
    public Camera playerCam;

    private void Update()
    {
        if (!isCound)
        {
            runimage.fillAmount = 1;
        }
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "LobbyMap")
        {
            Wallspeed = 10;
        }
        else
        {
            Wallspeed = 7f;
        }
        manaDash = GetComponent<PlayerInfor>().maxMP * 0.1f;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        runimage = GameObject.FindWithTag("RIMG").GetComponent<Image>();

        joystick = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        runButton = GameObject.Find("Run").GetComponent<Button>();
        runButton.onClick.AddListener(StartDash);

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
            if (!freeze)
            {
                HandleMovement();
            }
        }
    }
    public void UpdateManaDash()
    {
        manaDash = GetComponent<PlayerInfor>().maxMP * 0.1f;
    }
    private void HandleMovement()
    {
        float ipVertical = joystick.Vertical != 0 ? joystick.Vertical : Input.GetAxis("Vertical");
        float ipHorizontal = joystick.Horizontal != 0 ? joystick.Horizontal : Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(ipHorizontal, 0, ipVertical).normalized;

        if (move.magnitude > 0.1f)
        {
            Vector3 forward = playerCam.transform.forward.normalized;
            Vector3 right = playerCam.transform.right.normalized;
            forward.y = 0f;
            right.y = 0f;
            Vector3 desiredVelocity = (forward * ipVertical + right * ipHorizontal) * Wallspeed;

            rb.MovePosition(rb.position + desiredVelocity * Time.fixedDeltaTime);
            RotateCharacter(desiredVelocity);

            animator.SetFloat("Speed", 2);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
    
    
    private void HandleDash()
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

    public void RotateCharacter(Vector3 playerMovementInput)
    {
        if (playerMovementInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerMovementInput);
            transform.rotation = rotation;
        }
    }

    public void StartDash()
    {
        if (!isCound)
        {
            if (!isDashing)
            {
                if (GetComponent<PlayerInfor>().manaPoint >= manaDash)
                {
                    GetComponent<PlayerInfor>().manaPoint -= manaDash;
                    isCound = true;
                    StartCoroutine(CounDown(8));
                    dashTime = dashDuration;
                    isDashing = true;
                    GetComponent<SoundEffect>().PlaySound("Dash");
                    dashDirection = transform.forward;
                    Debug.Log("das");
                    runEff.SetActive(true);
                    animator.SetFloat("Speed",2);

                }
                else
                {
                    Debug.Log("Không đủ mana");
                    return;
                }
            }
            else
            {
                Debug.Log("Đang Dash");
            }
        }
        else
        {
            Debug.Log(" Chưa hồi chiêu");
        }
    }

    public void StopDash()
    {
        isDashing = false;
        rb.velocity = Vector3.zero;
        runEff.SetActive(false);
    }

    private bool IsMoving()
    {
        float ipVertical = joystick.Vertical;
        float ipHorizontal = joystick.Horizontal;
        return ipVertical != 0 || ipHorizontal != 0;
    }

    IEnumerator CounDown(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float fill = 0f;
            fill += elapsedTime / time;
            runimage.fillAmount = fill;
            yield return null;
        }
        isCound = false;
    }
}
