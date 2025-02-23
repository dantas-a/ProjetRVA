using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkEffect : MonoBehaviour
{
    public Volume postProcessVolume;
    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;
    private Vignette vignette;

    public List<float> distortionIntensity;
    public List<float> wobbleSpeed;
    public List<float> wobbleAmount;
    public List<float> flou ;
    public List<float> ombre;
    public int lvl = 0;

    void Start()
    {
        postProcessVolume.profile.TryGet(out lensDistortion);
        postProcessVolume.profile.TryGet(out motionBlur);
        postProcessVolume.profile.TryGet(out vignette);
        EventSystemManager.Instance.SubscribeToEvent("Acte 2", () => lvl++);
        EventSystemManager.Instance.SubscribeToEvent("Acte 3", () => lvl++);
    }

    void Update()
    {
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(Time.time * wobbleSpeed[lvl]) * wobbleAmount[lvl] + distortionIntensity[lvl];
        }

        if (motionBlur != null)
        {
            motionBlur.intensity.value = flou[lvl]; // Flou constant
        }

        if (vignette != null)
        {
            vignette.intensity.value = ombre[lvl]; // Ombre autour des bords
        }
    }
}
