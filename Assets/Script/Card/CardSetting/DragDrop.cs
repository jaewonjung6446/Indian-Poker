using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 targetScale;  // 목표 스케일
    private bool isReturning = false;
    private bool isZooming = false;  // 확대/축소 진행 중인지 표시
    public float returnSpeed = 0.1f;
    public float zoomScaleFactor = 1.5f;  // 확대 배율
    public float zoomSpeed = 0.05f;       // 확대 속도
    private int originalSortingOrder;     // 원래 sorting order 저장

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = rectTransform.localScale;  // 원래 스케일 저장
        targetScale = originalScale;  // 초기 목표 스케일 설정
        originalSortingOrder = canvas.sortingOrder; // 원래 sorting order 저장
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * zoomScaleFactor;  // 목표 스케일 설정
        canvas.sortingOrder = 100;  // 확대된 카드를 가장 앞으로
        isZooming = true;  // 확대 시작
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;  // 원래 스케일로 목표 설정
        canvas.sortingOrder = originalSortingOrder; // sorting order를 원래대로 복귀
        isZooming = true;  // 축소 시작
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
