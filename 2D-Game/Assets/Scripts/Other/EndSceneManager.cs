using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    private GameManager GM;
    private TextMeshProUGUI timerText, totalPoint;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        timerText = GameObject.Find("UICanvas/Time").GetComponent<TextMeshProUGUI>();
        totalPoint = GameObject.Find("UICanvas/pointsForCoin").GetComponent<TextMeshProUGUI>();
        timerText.text = GM.timerText.text;
        totalPoint.text = GM.pointsForCoin.text;
    }


}
