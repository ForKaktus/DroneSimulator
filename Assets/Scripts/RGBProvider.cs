using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBProvider : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private float speed;
    [SerializeField] private float intensity;
    [SerializeField] private Material[] rgbMaterials;
    [SerializeField] private AnimationCurve rgbCurve;
    
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        StartCoroutine(UpdateMaterial());
    }

    private IEnumerator UpdateMaterial()
    {
        Color color = new Color();
        
        while (true)
        {
            foreach (var material in rgbMaterials)
            {
                color = gradient.Evaluate(rgbCurve.Evaluate(Time.time * speed));
                material.SetColor(EmissionColor, color * intensity);
            }
            
            yield return null;
        }
    }
}
