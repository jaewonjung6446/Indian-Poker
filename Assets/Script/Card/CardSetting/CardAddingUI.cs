using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ��� ����� ���� �߰�

public class CardAddingUI : MonoBehaviour
{
    public GameObject cardPrefab; // ī�� ������
    public Button addButton; // ī�� �߰� ��ư

    void Start()
    {
        // ��ư�� onClick �̺�Ʈ�� �޼��� ����
        addButton.onClick.AddListener(AddCard);
    }

    void AddCard()
    {
        int a = Random.Range(0, CardManager.cardManager.cardPrefabs.Length);
        GameObject gameObject = CardManager.cardManager.cardPrefabs[a];
        // ī�� �ν��Ͻ� ����
        GameObject newCard = Instantiate(gameObject, transform);

        // ������ ī�带 cards ����Ʈ�� �߰�
        FanOutCardsUI.Instance.cards.Add(newCard.gameObject.GetComponent<RectTransform>());

        // ī�� �迭 ������ (������)
        FanOutCardsUI.Instance.StartArrangeCards();
    }
}
