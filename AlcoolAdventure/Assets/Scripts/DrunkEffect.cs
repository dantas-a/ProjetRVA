using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkEffect : MonoBehaviour
{
    public Volume postProcessVolume;
    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;
    private Vignette vignette;

    public float distortionIntensity = 0.3f;
    public float wobbleSpeed = 2.0f;
    public float wobbleAmount = 0.1f;
    public float flou = 0.5f;
    public float ombre = 0.4f;

    void Start()
    {
        postProcessVolume.profile.TryGet(out lensDistortion);
        postProcessVolume.profile.TryGet(out motionBlur);
        postProcessVolume.profile.TryGet(out vignette);
    }

    void Update()
    {
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount + distortionIntensity;
        }

        if (motionBlur != null)
        {
            motionBlur.intensity.value = flou; // Flou constant
        }

        if (vignette != null)
        {
            vignette.intensity.value = ombre; // Ombre autour des bords
        }
    }
}
