using UnityEngine;
using System.Collections;

public class Lightningflase : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // 애니메이션 처음부터 한 번 재생
        animator.Play("LightningAnimation", -1, 0f);
        // 불투명하게 초기화
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        // 애니메이션 종료 후 투명화 코루틴 시작
        StartCoroutine(PlayAndFadeOut());
    }

    private IEnumerator PlayAndFadeOut()
    {
        // 애니메이션이 1초짜리라고 가정 (클립 길이에 맞게 수정 가능)
        yield return new WaitForSeconds(1f);

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}