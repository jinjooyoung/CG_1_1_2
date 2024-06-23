using UnityEngine;
using System.Collections;

public class EmissionController : MonoBehaviour
{
    public Material sharedMaterial; // ������ Emission ��Ƽ����
    public float pulseSpeed = 1.0f; // Emission Intensity ��ȭ �ӵ�
    public float minIntensity = 2.0f; // Emission Intensity�� �ּҰ�
    public float maxIntensity = 12.0f; // Emission Intensity�� �ִ밪
    public Color emissionColor = Color.white; // Emission ����

    private void Start()
    {
        // Emission�� Ȱ��ȭ
        sharedMaterial.EnableKeyword("_EMISSION");
        // �ڷ�ƾ ����
        StartCoroutine(PulseEmission());
    }

    private IEnumerator PulseEmission()
    {
        while (true)
        {
            // Emission Intensity�� ���̴� �κ�
            yield return StartCoroutine(ChangeEmissionIntensity(minIntensity, maxIntensity));
            // Emission Intensity�� ���ߴ� �κ�
            yield return StartCoroutine(ChangeEmissionIntensity(maxIntensity, minIntensity));
        }
    }

    private IEnumerator ChangeEmissionIntensity(float from, float to)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < pulseSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / pulseSpeed);
            float intensity = Mathf.Lerp(from, to, t);
            Color finalColor = emissionColor * Mathf.LinearToGammaSpace(intensity);
            sharedMaterial.SetColor("_EmissionColor", finalColor);
            yield return null;
        }
    }

    private void OnDisable()
    {
        // Emission�� ��Ȱ��ȭ
        sharedMaterial.DisableKeyword("_EMISSION");
    }
}
