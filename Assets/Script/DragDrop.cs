using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private bool isReturning = false;  // ī�尡 ���� ��ġ�� ���ư����� ����
    public float returnSpeed = 0.1f;  // ���� �ӵ�
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
        fanOutScript.StartArrangeCards(); // ī�� �迭 ������Ʈ
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
        isReturning = false;  // �巡�� ���� ��, ���� �ߴ�
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Dragging");
        isReturning = true;  // �巡�װ� ������ ���� ��ġ�� ���� ���� // �θ� ������Ʈ���� FanOutCardsUI ��ũ��Ʈ ã��
        if (fanOutScript != null)
        {
            fanOutScript.StartArrangeCards(); // ī�� �迭 ������Ʈ
        }
    }

    private void Update()
    {
        if (isReturning)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, originalPosition, returnSpeed);
            if (Vector3.Distance(rectTransform.anchoredPosition, originalPosition) < 0.01f)  // ���� �������� ��
            {
                rectTransform.anchoredPosition = originalPosition;  // ��Ȯ�� ��ġ�� ����
                isReturning = false;  // ������ �Ϸ�
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
