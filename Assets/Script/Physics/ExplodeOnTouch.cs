using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    public float explosionForce = 500f; // 폭발력
    public float explosionRadius = 5f; // 폭발 범위
    public float upwardsModifier = 1f; // 위쪽 힘 보정
    public GameObject explosionEffect; // 폭발 효과(파티클 시스템 등)

    void Update()
    {
        // 마우스 클릭 또는 터치 확인
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // 화면상의 클릭 또는 터치 위치
            Vector3 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position;

            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Explode(hit.point);
            }
        }
    }

    void Explode(Vector3 explosionPosition)
    {
        // 폭발 효과 표시
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, explosionPosition, Quaternion.identity);
        }

        // 주변 오브젝트에 폭발력 적용
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
            }
        }
    }
}
