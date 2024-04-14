using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private bool isReturning = false;  // 카드가 원래 위치로 돌아가는지 여부
    public float returnSpeed = 0.1f;  // 복귀 속도
    FanOutCardsUI fanOutScript;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        fanOutScript = GetComponentInParent<FanOutCardsUI>();
    }
    private void Start()
    {
        fanOutScript.StartArrangeCards(); // 카드 배열 업데이트
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
        isReturning = false;  // 드래그 시작 시, 복귀 중단
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Dragging");
        isReturning = true;  // 드래그가 끝나면 원래 위치로 복귀 시작 // 부모 오브젝트에서 FanOutCardsUI 스크립트 찾기
        if (fanOutScript != null)
        {
            fanOutScript.StartArrangeCards(); // 카드 배열 업데이트
        }
    }

    private void Update()
    {
        if (isReturning)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, originalPosition, returnSpeed);
            if (Vector3.Distance(rectTransform.anchoredPosition, originalPosition) < 0.01f)  // 거의 도달했을 때
            {
                rectTransform.anchoredPosition = originalPosition;  // 정확한 위치에 고정
                isReturning = false;  // 움직임 완료
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
