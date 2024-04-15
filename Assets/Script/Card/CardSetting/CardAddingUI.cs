using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ��� ����� ���� �߰�

public class CardAddingUI : MonoBehaviour
{
    public RectTransform cardPrefab; // ī�� ������
    public Button addButton; // ī�� �߰� ��ư

    void Start()
    {
        // ��ư�� onClick �̺�Ʈ�� �޼��� ����
        addButton.onClick.AddListener(AddCard);
    }

    void AddCard()
    {
        // ī�� �ν��Ͻ� ����
        RectTransform newCard = Instantiate(cardPrefab, transform);

        // ������ ī�带 cards ����Ʈ�� �߰�
        FanOutCardsUI.Instance.cards.Add(newCard);

        // ī�� �迭 ������ (������)
        FanOutCardsUI.Instance.StartArrangeCards();
    }
}
