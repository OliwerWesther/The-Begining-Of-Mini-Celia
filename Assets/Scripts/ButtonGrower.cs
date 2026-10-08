using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGrower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Settings")]
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1f);  // Scale when hovered
    public float scaleSpeed = 10f;

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Vector3 currentTargetScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        currentTargetScale = originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentTargetScale = targetScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentTargetScale = originalScale;
    }

    void Update()
    {
        rectTransform.localScale =
            Vector3.Lerp(rectTransform.localScale, currentTargetScale, Time.deltaTime * scaleSpeed);
    }
}
