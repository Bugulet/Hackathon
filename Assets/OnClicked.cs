using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnClicked : MonoBehaviour
{
    public int Temperature;
    public int Humidity;
    public int Emission;

    private void OnMouseOver()
    {
        FindObjectOfType<SensorDataText>().GetComponent<TextMeshProUGUI>().text = $"{gameObject.name}\nTemp:{Temperature}°C\nH:{Humidity}%\nE:{Emission}PPM";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
