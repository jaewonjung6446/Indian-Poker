using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOutCardsUI : MonoBehaviour
{
    public static FanOutCardsUI Instance;
    public List<RectTransform> cards = new List<RectTransform>();
    public float angleStep = 5f;
    public float radius = 300f;
    public float transitionDuration = 0.5f; // 전환에 걸리는 시간
    public Vector3 center = new Vector3(0,0,0);

    private void Awake()
    {
        Instance = this;
    }
    public void StartArrangeCards()
    {
        StartCoroutine(ArrangeCardsCoroutine());
    }

    public IEnumerator ArrangeCardsCoroutine()
    {
        float timeElapsed = 0;

        // 초기 위치와 회전 저장
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

                Vector3 targetPosition = new Vector3(Mathf.Sin(angleRad) * radius, Mathf.Cos(angleRad) * radius, 0)+ center;
                Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);

                cards[i].localPosition = Vector3.Lerp(startPositions[i], targetPosition, timeElapsed / transitionDuration);
                cards[i].localRotation = Quaternion.Lerp(startRotations[i], targetRotation, timeElapsed / transitionDuration);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치 확정
        for (int i = 0; i < cards.Count; i++)
        {
            float angle = angleStep * i - (angleStep * (cards.Count - 1) / 2);
            float angleRad = angle * Mathf.Deg2Rad;
            cards[i].localPosition = new Vector3(Mathf.Sin(angleRad) * radius, Mathf.Cos(angleRad) * radius, 0)+ center;
            cards[i].localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}