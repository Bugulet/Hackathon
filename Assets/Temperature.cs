using UnityEngine;

public class Temperature : MonoBehaviour
{
    public Renderer rend; // Reference to the Renderer component
    public float minTemperature = 0f; // Minimum temperature value
    public float maxTemperature = 45f; // Maximum temperature value

    void Start()
    {
        // Ensure the Renderer component is assigned
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Renderer component not found or assigned.");
                return;
            }
        }

        // Example: Start with a default temperature value
        float initialTemperature = 50f;
        UpdateColor(initialTemperature);
    }

    void Update()
    {
        Debug.Log("Ai uitat sa stergi scriptul asta!");
    }

    void UpdateColor(float temperature)
    {
        // Normalize temperature to a range between 0 and 1
        float normalizedTemperature = Mathf.InverseLerp(minTemperature, maxTemperature, temperature);

        // Interpolate between two colors based on the normalized temperature
        Color startColor = Color.blue; // Cold color
        Color endColor = Color.red;   // Hot color
        Color lerpedColor = Color.Lerp(startColor, endColor, normalizedTemperature);

        // Apply the color to the Renderer component
        rend.material.color = lerpedColor;
    }
}
