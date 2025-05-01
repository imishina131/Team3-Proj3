using UnityEngine;

public class FlickerGlow : MonoBehaviour
{
    [SerializeField] private Renderer glowRenderer;
    [SerializeField] private Color baseEmissionColor = Color.white;
    [SerializeField] private float flickerSpeed = 2f;
    [SerializeField] private float intensity = 1f;

    private Material glowMaterial;
    private float time;

    void Start()
    {
        glowMaterial = glowRenderer.material;
    }

    void Update()
    {
        time += Time.deltaTime * flickerSpeed;
        float lerp = Mathf.PingPong(time, 1f); 
        Color flickerColor = baseEmissionColor * (lerp * intensity);
        glowMaterial.SetColor("_EmissionColor", flickerColor);

        DynamicGI.SetEmissive(glowRenderer, flickerColor);
    }
}