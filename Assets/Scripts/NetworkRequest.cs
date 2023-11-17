using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkRequest : MonoBehaviour
{
    // The URL of the JSON endpoint
    private string jsonEndpoint = "https://smartcity-l5jm.onrender.com/lastdata";

    public Renderer zona1;
    public Renderer zona2;
    public Renderer zona3;
    public Renderer zona4;
    public Renderer zona5;
    public Renderer zona6;
    public Renderer zona7;
    public Renderer zona8;
    public Renderer zona9; // Reference to the Renderer component
    public float minTemperature = 50f; // Minimum temperature value
    public float maxTemperature = 100f; // Maximum temperature value


    void zone(Renderer zona)
    {
        if (zona == null)
        {
            zona = GetComponent<Renderer>();
            if (zona == null)
            {
                Debug.LogError("Renderer component not found or assigned.");
                return;
            }
        }

        // Example: Start with a default temperature value
        float initialTemperature = 18f;
        UpdateColor(initialTemperature, zona1);
        UpdateColor(initialTemperature, zona2);
        UpdateColor(initialTemperature, zona3);
        UpdateColor(initialTemperature, zona4);
        UpdateColor(initialTemperature, zona5);
        UpdateColor(initialTemperature, zona6);
        UpdateColor(initialTemperature, zona7);
        UpdateColor(initialTemperature, zona8);
        UpdateColor(initialTemperature, zona9);
    }

    void Start()
    {
        // Start the coroutine to fetch JSON data at regular intervals
        StartCoroutine(FetchJsonDataRepeatedly(1f)); // Request every 5 seconds
                                                     // Ensure the Renderer component is assigned

    }

    IEnumerator FetchJsonDataRepeatedly(float interval)
    {
        while (true)
        {

            yield return new WaitForSeconds(interval);

            // Send a GET request to the JSON endpoint
            UnityWebRequest request = UnityWebRequest.Get(jsonEndpoint);

            // Wait for the request to complete
            yield return request.SendWebRequest();

            // Check for errors during the request
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Parse and deserialize the JSON data
                string jsonData = request.downloadHandler.text;
                print(jsonData);
                MyDataClass myData = JsonUtility.FromJson<MyDataClass>(jsonData);
                int t = myData.temperature;
                int h = myData.humidity;
                // Access the deserialized data
                Debug.Log("Received temperature data: " + myData.temperature);
                Debug.Log("Received humidity data: " + myData.humidity);
                Debug.Log("Received emission data: " + myData.emission);

                for (int i = 1; i < 9; i++)
                {
                    GameObject.Find("zona" + i).GetComponent<OnClicked>().Temperature = myData.temperature+i;
                    GameObject.Find("zona" + i).GetComponent<OnClicked>().Humidity= myData.humidity+i;
                    GameObject.Find("zona" + i).GetComponent<OnClicked>().Emission = myData.emission+i;

                }

                UpdateColor(t - 7, zona1);
                UpdateColor(t + 3, zona2);
                UpdateColor(t - 4, zona3);
                UpdateColor(t - 7, zona4);
                UpdateColor(t - 1, zona5);
                UpdateColor(t + 3, zona6);
                UpdateColor(t, zona7);
                UpdateColor(t - 2, zona8);
                UpdateColor(t - 7, zona9);
            }
        }
    }

    // Define a data class to represent the structure of your JSON data
    [System.Serializable]
    public class MyDataClass
    {
        public string _id;
        public int temperature;
        public int humidity;
        public string date;
        public int emission;
        public int zone;
        public int __v;
    }

    void UpdateColor(float temperature, Renderer zona)
    {
        // Normalize temperature to a range between 0 and 1
        float normalizedTemperature = Mathf.InverseLerp(minTemperature, maxTemperature, temperature);
        // GetComponent<Material>().
        // Interpolate between two colors based on the normalized temperature
        Color startColor = Color.blue; // Cold color
        Color endColor = Color.red;   // Hot color
        Color lerpedColor = Color.Lerp(startColor, endColor, normalizedTemperature);
        lerpedColor.a = 0.5f;
        // Apply the color to the Renderer component
        zona.material.color = lerpedColor;
    }
}