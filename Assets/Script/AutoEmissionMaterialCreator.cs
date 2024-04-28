using UnityEngine;

public class AutoEmissionMaterialCreator : MonoBehaviour
{
    public Texture2D sourceTexture;
    public Material baseMaterial;

    void Start()
    {
        Debug.Log("Start 메서드 실행");
        if (sourceTexture == null)
            Debug.LogError("sourceTexture가 할당되지 않았습니다.");
        if (baseMaterial == null)
            Debug.LogError("baseMaterial이 할당되지 않았습니다.");

        Color[] colors = sourceTexture.GetPixels();
        Debug.Log($"텍스처에서 {colors.Length}개의 색상을 가져왔습니다.");

        foreach (Color color in colors)
        {
            if (colorIsEmissive(color))
            {
                Material newMat = new Material(baseMaterial);
                Color hdrColor = color * 2.0f; // HDR 색상 강도 증폭
                newMat.SetColor("_EmissionColor", Color.yellow);
                newMat.EnableKeyword("_EMISSION");
                Debug.Log("Emission 속성이 설정된 새 메테리얼 생성");
            }
        }
    }

    bool colorIsEmissive(Color color)
    {
        bool isEmissive = color.maxColorComponent > 0.3f;
        Debug.Log($"색상 {color}는 Emission 처리: {isEmissive}");
        return isEmissive;
    }
}
