using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHoverHandler : MonoBehaviour
{

    public Color bright;
    public Color dark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = bright;
    }

    public void OnHoverExit()
    {
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = dark;
    }
}
