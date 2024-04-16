using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 targetScale;  // ��ǥ ������
    private bool isReturning = false;
    private bool isZooming = false;  // Ȯ��/��� ���� ������ ǥ��
    public float returnSpeed = 0.1f;
    public float zoomScaleFactor = 1.5f;  // Ȯ�� ����
    public float zoomSpeed = 0.05f;       // Ȯ�� �ӵ�
    private int originalSortingOrder;     // ���� sorting order ����

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = rectTransform.localScale;  // ���� ������ ����
        targetScale = originalScale;  // �ʱ� ��ǥ ������ ����
        originalSortingOrder = canvas.sortingOrder; // ���� sorting order ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * zoomScaleFactor;  // ��ǥ ������ ����
        canvas.sortingOrder = 100;  // Ȯ��� ī�带 ���� ������
        isZooming = true;  // Ȯ�� ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;  // ���� �����Ϸ� ��ǥ ����
        canvas.sortingOrder = originalSortingOrder; // sorting order�� ������� ����
        isZooming = true;  // ��� ����
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card Touched");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Dragging");
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        isReturning = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Dragging");
        isReturning = true;
        CardData a = CardManager.cardManager.GetCardData(this.name);
        a.Effect();
        FanOutCardsUI.Instance.cards.Remove(this.rectTransform);
        this.gameObject.SetActive(false);
        FanOutCardsUI.Instance.StartArrangeCards();
        if (FanOutCardsUI.Instance != null)
        {
            FanOutCardsUI.Instance.StartArrangeCards();
        }
    }

    private void Update()
    {
        if (isReturning)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, originalPosition, returnSpeed);
            if (Vector3.Distance(rectTransform.anchoredPosition, originalPosition) < 0.01f)
            {
                rectTransform.anchoredPosition = originalPosition;
                isReturning = false;
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
        }
        if (isZooming)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, zoomSpeed);
            if (Vector3.Distance(rectTransform.localScale, targetScale) < 0.01f)
            {
                rectTransform.localScale = targetScale;
                isZooming = false;
            }
        }
    }
}
