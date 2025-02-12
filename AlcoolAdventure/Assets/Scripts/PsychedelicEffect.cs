using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PsychedelicEffect : MonoBehaviour
{
    public Volume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;

    public float colorShiftSpeed = 1.5f;
    
    void Start()
    {
        postProcessVolume.profile.TryGet(out chromaticAberration);
        postProcessVolume.profile.TryGet(out colorAdjustments);
        postProcessVolume.profile.TryGet(out lensDistortion);
    }

    void Update()
    {
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.PingPong(Time.time, 1.0f);
        }

        if (colorAdjustments != null)
        {
            colorAdjustments.hueShift.value = Mathf.Sin(Time.time * colorShiftSpeed) * 180f;
            colorAdjustments.saturation.value = Mathf.PingPong(Time.time * 50, 100);
        }

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(Time.time * 3) * 0.4f;
        }
    }
}
