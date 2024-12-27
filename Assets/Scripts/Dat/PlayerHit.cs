using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float knockbackDistance = 1f; // Khoảng cách giật lùi
    public float reactionDuration = 0.5f; // Thời gian giật và quay lại vị trí ban đầu
    
    Animator animator;
    private Vector3 originalPosition;
    private bool isReacting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(Vector3 damageDirection)
    {
        if (isReacting) return; // Nếu đang trong trạng thái giật, không giật tiếp

        isReacting = true;

        // Tính toán vị trí giật
        originalPosition = transform.localPosition;
        Vector3 knockbackPosition = originalPosition + damageDirection.normalized * knockbackDistance;

        // Bắt đầu hiệu ứng giật và quay về
        animator.SetTrigger("Hit");
        StartCoroutine(ReactToHit(knockbackPosition));
    }

    private IEnumerator ReactToHit(Vector3 knockbackPosition)
    {
        // Giật về phía knockbackPosition
        float elapsed = 0f;
        while (elapsed < reactionDuration / 2)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, knockbackPosition, elapsed / (reactionDuration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Giật xong, quay về vị trí ban đầu
        elapsed = 0f;
        while (elapsed < reactionDuration / 2)
        {
            transform.localPosition = Vector3.Lerp(knockbackPosition, originalPosition, elapsed / (reactionDuration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Kết thúc phản ứng
        transform.localPosition = originalPosition;
        animator.ResetTrigger("Hit");
    }

    public void SetHit()
    {
        if (isReacting) isReacting = false;
        return;
    }
}
