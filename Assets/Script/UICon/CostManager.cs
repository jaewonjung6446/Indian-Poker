using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public static CostManager instance;
    public int maxCost = 3;
    public int currentCost;
    public Image[] costImages;
    public Vector2 startPosition = new Vector2(50, 50); // 시작 위치
    public float spacing = 40; // 이미지 간 간격

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        maxCost = GameManager.gameManager.currentCost;
        SetupCostImages();
        UpdateCostDisplay();
    }

    // 이미지 위치 설정
    void SetupCostImages()
    {
        for (int i = 0; i < costImages.Length; i++)
        {
            if (costImages[i] != null)
            {
                RectTransform rt = costImages[i].GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(startPosition.x + i * spacing, startPosition.y);
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(0, 0);
                rt.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    }

    public void UseCost(int amount)
    {
        GameManager.gameManager.currentCost -= amount;
        if (currentCost < 0) currentCost = 0;
        UpdateCostDisplay();
    }

    void UpdateCostDisplay()
    {
        for (int i = 0; i < costImages.Length; i++)
        {
            costImages[i].enabled = i < GameManager.gameManager.currentCost;
        }
    }
}
