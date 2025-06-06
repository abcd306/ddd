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
        animator.Play("LightningAnimation", 0, 0f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(PlayAndFadeOut());
    }

    private IEnumerator PlayAndFadeOut()
    {
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