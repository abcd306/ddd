using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;

    private int maxLives = 3;
    private int currentLives;

    private float invincibleTime = 20f;
    private float hurtDuration = 0.3f;

    private bool isHurt = false;
    private bool isInvincible = false;
    private bool playerdie = false;

    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;
    private bool isPaused = false;

    private bool isControlLocked = false; // ✅ 입력 잠금 변수 추가

    private SpriteRenderer spr;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    [Header("Auto Run Settings")]
    public float runDistance = 8f;
    public float runToEdgeSpeed = 5f;
    private bool isRunningToEdge = false;
    private bool isFeathering = false;

    public enum Direction { Right, Left }
    public Direction currentDirection = Direction.Right;

    private Rigidbody2D rigid;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        SlcCol.enabled = false;
        RunnCol.enabled = true;
    }

    private void Start()
    {
        RestoreFullHP();
        SetGroundTrue();
    }

    private void Update()
    {
        if (isControlLocked) return; // ✅ 컨트롤 잠금 시 입력 무시

        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerdie) return;
            Invoke("TryJump", 0.08f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGroundTrue();
            PlayerAnimator.SetInteger("State", 0);
            isJumping = false;
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D: 플레이어가 충돌한 오브젝트 이름 → " + collider.gameObject.name);

        // 테스트용 RunTrigger 작동 여부 확인
        if (collider.gameObject.CompareTag("RunTrigger"))
        {
            if (!isRunningToEdge)  // 중복 실행 방지
            {
                StartCoroutine(RunFixedDistance());
            }
        }

        // Stop 트리거도 여기서 처리
        if (collider.CompareTag("Stop"))
        {
            Debug.Log("🚫 Stop Trigger Detected! Controls locked for 3 seconds.");

            // 슬라이딩 강제 해제
            PlayerAnimator.SetInteger("State", 0);
            SlcCol.enabled = false;
            RunnCol.enabled = true;

            StartCoroutine(LockControlsForSeconds(3f));
        }

        IEnumerator RunFixedDistance()
        {
            if (isRunningToEdge)
                yield break; // 이미 실행 중이면 종료

            isRunningToEdge = true;
            PlayerAnimator.SetInteger("State", 0);

            float startX = transform.position.x;
            float targetX = (currentDirection == Direction.Right) ? startX + runDistance : startX - runDistance;

            spr.flipX = (currentDirection == Direction.Left);

            while ((currentDirection == Direction.Right && transform.position.x < targetX) ||
                   (currentDirection == Direction.Left && transform.position.x > targetX))
            {
                rigid.velocity = new Vector2(
                    (currentDirection == Direction.Right ? runToEdgeSpeed : -runToEdgeSpeed),
                    rigid.velocity.y
                );
                yield return null;
            }

            rigid.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.5f);

            FlipDirection();
            isRunningToEdge = false;

            // 디버그 추가
            Debug.Log("RunFixedDistance 완료, 현재 방향: " + currentDirection);
        }
    }

    public void FlipDirection()
    {
        currentDirection = (currentDirection == Direction.Right) ? Direction.Left : Direction.Right;
        spr.flipX = (currentDirection == Direction.Left);

        int scrollDir = (currentDirection == Direction.Right) ? 1 : -1;

        scrollDir = ScrollManager.Instance.GetDirection();
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;
        UIManager.instance.UpdateLivesUI(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            isHurt = true;
            isInvincible = true;
            PlayerAnimator.SetInteger("State", 3);

            Invoke("EndHurtAnimation", hurtDuration);
            Invoke("RecoverFromDamage", invincibleTime);

            StartCoroutine(AlphaBlink());

            StopManager stopManager = FindObjectOfType<StopManager>();
            if (stopManager != null)
            {
                stopManager.speed = 0f;
            }
        }
    }

    void EndHurtAnimation()
    {
        isHurt = false;
        PlayerAnimator.SetInteger("State", 0);
    }

    void RecoverFromDamage()
    {
        isInvincible = false;
        spr.color = fullA;
    }

    IEnumerator AlphaBlink()
    {
        while (isInvincible)
        {
            spr.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spr.color = fullA;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Die()
    {
        playerdie = true;
        isControlLocked = true; // 🔧 이거 추가!
        Debug.Log("Game Over");
        PlayerAnimator.SetInteger("State", 4);
        SpeedManager.Instance.moveSpeed = 0f;
        ScrollManager.Instance.speed = 0f;

        StopManager stopManager = FindObjectOfType<StopManager>();
        if (stopManager != null)
        {
            stopManager.speed = 0f;
        }
    }

    public void ForceDie()
    {
        currentLives = 0;
        UIManager.instance.UpdateLivesUI(0);
        Die();
    }


    void TryJump()
    {
        if (isHurt || isControlLocked) return; // ✅ 잠금 확인 추가

        if (isGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            PlayerAnimator.SetInteger("State", 1);
            isGround = false;
            isJumping = true;
            jumpCount++;
        }
        else if (isJumping && jumpCount < jumpLevel)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * 38f, ForceMode2D.Impulse);
            PlayerAnimator.Play("Jump", 0, 0f);
            isJumping = false;
        }
    }

    void Slide()
    {
        if (isHurt || isControlLocked) return; // ✅ 잠금 확인 추가

        if (Input.GetKey(KeyCode.C) && isGround)
        {
            PlayerAnimator.SetInteger("State", 2);
            SlcCol.enabled = true;
            RunnCol.enabled = false;
        }
        else if (isGround)
        {
            PlayerAnimator.SetInteger("State", 0);
            SlcCol.enabled = false;
            RunnCol.enabled = true;
        }
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    public void RestoreFullHP()    // 디버깅용 코드 수정
    {
        currentLives = maxLives;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLivesUI(currentLives);
        else
            Debug.LogWarning("UIManager.instance is null");
    }

    private IEnumerator LockControlsForSeconds(float seconds)
    {
        isControlLocked = true;
        yield return new WaitForSeconds(seconds);
        isControlLocked = false;
    }

    public void ResetState()
    {
        isInvincible = false;
        spr.color = fullA;

        CancelInvoke("RecoverFromDamage");

        PlayerAnimator.SetInteger("State", 0);
        SlcCol.enabled = false;
        RunnCol.enabled = true;

        StartCoroutine(LockControlsForSeconds(3f));
    }
}
