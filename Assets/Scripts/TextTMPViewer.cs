using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP; // Text - TextMeshPro UI [�÷��̾��� ü��]
    [SerializeField]
    private TextMeshProUGUI textPlayerGold; // Text - TextMeshPro UI [�÷��̾��� ���]
    [SerializeField]
    private TextMeshProUGUI textWave; // Text - TextMeshPro UI [���� ���̺� / �� ���̺�]
    [SerializeField]
    private PlayerHP playerHP; // �÷��̾��� ü�� ����
    [SerializeField]
    private PlayerGold playerGold; // �÷��̾��� ��� ����
    [SerializeField]
    private WaveSystem waveSystem; // ���̺� ����

    private void Update()
    {
        textPlayerHP.text = "<color=#FF0000>" + playerHP.CurrentHP + "</color>" + "/" + playerHP.MaxHP;
        textPlayerGold.text = playerGold.CurrentGold.ToString();
        textWave.text = "<color=#00EBFF>" + waveSystem.CurrentWave + "</color>" + "/" + waveSystem.MaxWave;
    }

}
