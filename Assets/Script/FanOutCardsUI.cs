using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOutCardsUI : MonoBehaviour
{
    public List<RectTransform> cards = new List<RectTransform>();
    public float angleStep = 10f;
    public float radius = 300f;
    public float transitionDuration = 0.5f; // ��ȯ�� �ɸ��� �ð�

    public void StartArrangeCards()
    {
        StartCoroutine(ArrangeCardsCoroutine());
    }

    private IEnumerator ArrangeCardsCoroutine()
    {
        float timeElapsed = 0;

        // �ʱ� ��ġ�� ȸ�� ����
        Vector3[] startPositions = new Vector3[cards.Count];
        Quaternion[] startRotations = new Quaternion[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            startPositions[i] = cards[i].localPosition;
            startRotations[i] = cards[i].localRotation;
        }

        while (timeElapsed < transitionDuration)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                float angle = angleStep * i - (angleStep * (cards.Count - 1) / 2);
                float angleRad = angle * Mathf.Deg2Rad;

                Vector3 targetPosition = new Vector3(Mathf.Sin(angleRad) * radius, Mathf.Cos(angleRad) * radius, 0);
                Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

                cards[i].localPosition = Vector3.Lerp(startPositions[i], targetPosition, timeElapsed / transitionDuration);
                cards[i].localRotation = Quaternion.Lerp(startRotations[i], targetRotation, timeElapsed / transitionDuration);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ������ ��ġ Ȯ��
        for (int i = 0; i < cards.Count; i++)
        {
            float angle = angleStep * i - (angleStep * (cards.Count - 1) / 2);
            float angleRad = angle * Mathf.Deg2Rad;
            cards[i].localPosition = new Vector3(Mathf.Sin(angleRad) * radius, Mathf.Cos(angleRad) * radius, 0);
            cards[i].localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}