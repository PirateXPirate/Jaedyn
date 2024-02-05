using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPaintingUiManager : MonoBehaviour
{
    [SerializeField] Text PaintingUiText;
    void Start()
    {
        PaintingUiText.text = "0/1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        PaintingUiText.text = "1/1";
    }
}
