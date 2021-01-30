using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellFX : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public ParticleSystem particleSystem;

    public void SetColor(Gradient colorGradient)
    {
        trailRenderer.colorGradient = colorGradient;
        particleSystem.startColor = colorGradient.colorKeys[0].color;
    }
}
