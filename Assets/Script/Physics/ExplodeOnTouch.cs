using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    public float explosionForce = 500f; // ���߷�
    public float explosionRadius = 5f; // ���� ����
    public float upwardsModifier = 1f; // ���� �� ����
    public GameObject explosionEffect; // ���� ȿ��(��ƼŬ �ý��� ��)

    void Update()
    {
        // ���콺 Ŭ�� �Ǵ� ��ġ Ȯ��
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // ȭ����� Ŭ�� �Ǵ� ��ġ ��ġ
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
        // ���� ȿ�� ǥ��
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, explosionPosition, Quaternion.identity);
        }

        // �ֺ� ������Ʈ�� ���߷� ����
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
