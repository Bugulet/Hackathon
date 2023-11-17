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
        FindObjectOfType<SensorDataText>().GetComponent<TextMeshProUGUI>().text = $"T:{Temperature}\nH:{Humidity}\nE:{Emission}";
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
