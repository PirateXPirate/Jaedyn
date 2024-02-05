using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUiManager : MonoBehaviour
{

    [SerializeField] Text MapText;
    [SerializeField] EndGame EndGameData;
    void Start()
    {
        MapText.text = EndGameData.LevelIndex.ToString() + "/9";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
