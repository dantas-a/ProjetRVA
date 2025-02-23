using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class PsychedelicEffect : MonoBehaviour
{
    public Volume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;

    public float effectDuration = 40f; // Temps total de l'effet
    public float fadeOutDuration = 10f; // Temps de retour à la normale
    public float fadeInDuration = 10f;
    public float colorShiftSpeed = 1.5f;

    private float currentIntensity = 1f;

    void Start()
    {
        postProcessVolume.profile.TryGet(out chromaticAberration);
        postProcessVolume.profile.TryGet(out colorAdjustments);
        postProcessVolume.profile.TryGet(out lensDistortion);
        EventSystemManager.Instance.SubscribeToEvent("Champignons consommés", () => StartCoroutine(EffectRoutine()));
    }

    IEnumerator EffectRoutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            currentIntensity = Mathf.Lerp(0f, 1f, elapsedTime / fadeOutDuration);
            UpdateEffects(currentIntensity);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Durée totale avant le fade-out
        elapsedTime = 0f;
        while (elapsedTime < effectDuration)
        {
            UpdateEffects(1f); // Intensité maximale
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fade-out progressif
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            currentIntensity = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            UpdateEffects(currentIntensity);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Remettre les effets à zéro
        UpdateEffects(0f);
    }

    void UpdateEffects(float intensityMultiplier)
    {
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.PingPong(Time.time, 1.0f) * intensityMultiplier;
        }

        if (colorAdjustments != null)
        {
            colorAdjustments.hueShift.value = Mathf.Sin(Time.time * colorShiftSpeed) * 180f * intensityMultiplier;
            colorAdjustments.saturation.value = Mathf.PingPong(Time.time * 50, 100) * intensityMultiplier;
        }

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(Time.time * 3) * 0.4f * intensityMultiplier;
        }
    }
}
