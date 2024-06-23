using UnityEngine;
using System.Collections;

public class EmissionController : MonoBehaviour
{
    public Material sharedMaterial; // 공유된 Emission 머티리얼
    public float pulseSpeed = 1.0f; // Emission Intensity 변화 속도
    public float minIntensity = 2.0f; // Emission Intensity의 최소값
    public float maxIntensity = 12.0f; // Emission Intensity의 최대값
    public Color emissionColor = Color.white; // Emission 색상

    private void Start()
    {
        // Emission을 활성화
        sharedMaterial.EnableKeyword("_EMISSION");
        // 코루틴 시작
        StartCoroutine(PulseEmission());
    }

    private IEnumerator PulseEmission()
    {
        while (true)
        {
            // Emission Intensity를 높이는 부분
            yield return StartCoroutine(ChangeEmissionIntensity(minIntensity, maxIntensity));
            // Emission Intensity를 낮추는 부분
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
        // Emission을 비활성화
        sharedMaterial.DisableKeyword("_EMISSION");
    }
}
