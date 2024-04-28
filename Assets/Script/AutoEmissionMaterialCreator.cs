using UnityEngine;

public class AutoEmissionMaterialCreator : MonoBehaviour
{
    public Texture2D sourceTexture;
    public Material baseMaterial;

    void Start()
    {
        Debug.Log("Start �޼��� ����");
        if (sourceTexture == null)
            Debug.LogError("sourceTexture�� �Ҵ���� �ʾҽ��ϴ�.");
        if (baseMaterial == null)
            Debug.LogError("baseMaterial�� �Ҵ���� �ʾҽ��ϴ�.");

        Color[] colors = sourceTexture.GetPixels();
        Debug.Log($"�ؽ�ó���� {colors.Length}���� ������ �����Խ��ϴ�.");

        foreach (Color color in colors)
        {
            if (colorIsEmissive(color))
            {
                Material newMat = new Material(baseMaterial);
                Color hdrColor = color * 2.0f; // HDR ���� ���� ����
                newMat.SetColor("_EmissionColor", Color.yellow);
                newMat.EnableKeyword("_EMISSION");
                Debug.Log("Emission �Ӽ��� ������ �� ���׸��� ����");
            }
        }
    }

    bool colorIsEmissive(Color color)
    {
        bool isEmissive = color.maxColorComponent > 0.3f;
        Debug.Log($"���� {color}�� Emission ó��: {isEmissive}");
        return isEmissive;
    }
}
