using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능 사용을 위해 추가

public class CardAddingUI : MonoBehaviour
{
    public GameObject cardPrefab; // 카드 프리팹
    public Button addButton; // 카드 추가 버튼

    void Start()
    {
        // 버튼의 onClick 이벤트에 메서드 연결
        addButton.onClick.AddListener(AddCard);
    }

    void AddCard()
    {
        int a = Random.Range(0, CardManager.cardManager.cardPrefabs.Length);
        GameObject gameObject = CardManager.cardManager.cardPrefabs[a];
        // 카드 인스턴스 생성
        GameObject newCard = Instantiate(gameObject, transform);

        // 생성된 카드를 cards 리스트에 추가
        FanOutCardsUI.Instance.cards.Add(newCard.gameObject.GetComponent<RectTransform>());

        // 카드 배열 재조정 (선택적)
        FanOutCardsUI.Instance.StartArrangeCards();
    }
}
