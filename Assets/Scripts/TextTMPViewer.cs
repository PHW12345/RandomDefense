using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP; // Text - TextMeshPro UI [플레이어의 체력]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [플레이어의 골드]
    [SerializeField]
    private TextMeshProUGUI textWave; // Text - TextMeshPro UI [현재 웨이브 / 총 웨이브]
    [SerializeField]
    private PlayerHP playerHP; // 플레이어의 체력 정보
    [SerializeField]
    private PlayerGold playerGold; // 플레이어의 골드 정보
    [SerializeField]
    private WaveSystem waveSystem; // 웨이브 정보

    private void Update()
    {
        textPlayerHP.text = "<color=#FF0000>" + playerHP.CurrentHP + "</color>" + "/" + playerHP.MaxHP;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = "<color=#00EBFF>" + waveSystem.CurrentWave + "</color>" + "/" + waveSystem.MaxWave;
    }

}
