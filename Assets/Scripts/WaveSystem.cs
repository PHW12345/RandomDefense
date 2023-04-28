using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // ���� ���������� ��� ���̺� ����
    [SerializeField]
    private Wave[] waves2; // �ι�° ���̺� ����
    [SerializeField]
    private Wave[] waves3; // �ι�° ���̺� ����
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1; // ���� ���̺� �ε���

    // ���̺� ���� ����� ���� Get ������Ƽ (���� ���̺�, �� ���̺�)
    public int CurrentWave => currentWaveIndex + 1; // ������ 0�̱� ������ +1
    public int MaxWave => waves.Length;

    public GameObject winPopup;
    public GameObject wavePanel;
    public GameObject wavePanel2;

    public GameObject dialog_war;

    public Image waveImage;
    
    [SerializeField]
    private PlayerHP currentHp;

    [SerializeField]
    private PlayerGold playerGold; // �÷��̾��� ��� ������Ʈ
    public bool getGlod;

    [SerializeField]
    private BGMController bgmController; // ������� ���� (���� ���� �� ����)

    public bool waveStart;
    //public AudioSource winMusic;
    //public void Start()
    //{
    //    SetWavePanel();
    //}
    [SerializeField]
    private GameObject textBossWarning; // �������� �ؽ�Ʈ ������Ʈ

    //public void Awake()
    //{
    //    // ���� ���� �ؽ�Ʈ ��Ȱ��ȭ
    //    textBossWarning.SetActive(false);
    //}
    public void Start()
    {
        //battleMusic = GetComponent<AudioSource>();
        //mainBgm.backMusic = GetComponent<AudioSource>();
        //bossMusic = GetComponent<AudioSource>();
    }
    public void StartWave()
    {
        //mainBgm.backMusic.enabled = false;

        //if (currentWaveIndex == waves.Length - 1)
        //{
        //    bgmController.ChangeBGM(BGMType.Stage);
        //}
        //else
        //{
        //    bgmController.ChangeBGM(BGMType.Boss);

        //}
        if (waveStart == false)
        {
            bgmController.ChangeBGM(BGMType.Stage);
        }

        //if (currentWaveIndex == waves.Length - 2)
        //{
        //    bgmController.ChangeBGM(BGMType.Boss);
        //    StartCoroutine("Boss");
        //}
        dialog_war.SetActive(false);
        getGlod = true;
        wavePanel.SetActive(false);

        //���� �ʿ� ���� ����, Wave�� ���� ������
        if (enemySpawner.EnemyList.Count < 3 && currentWaveIndex < waves.Length - 1)
        {

            // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ���� ��
            currentWaveIndex++;
            // EnemySpawner�� StartWave() �Լ� ȣ��. ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[currentWaveIndex]);

            //gogogo = true;
        }
        waveStart = true;

    }
    public void StartWave2()
    {
        //mainBgm.backMusic.enabled = false;
        if (waveStart == false)
        {
            bgmController.ChangeBGM(BGMType.Stage);
        }

        if (currentWaveIndex == waves.Length - 2)
        {
            bgmController.ChangeBGM(BGMType.Boss);
            StartCoroutine("Boss");
        }


        dialog_war.SetActive(false);
        getGlod = true;
        wavePanel.SetActive(false);
        //wavePanel2.SetActive(false);

        // ���� �ʿ� ���� ����, Wave�� ���� ������
        // StartCoroutine("Delay03");
        if (enemySpawner.EnemyList.Count < 3 && currentWaveIndex < waves.Length - 1)
        {
            // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ���� ��
            currentWaveIndex++;
            // EnemySpawner�� StartWave() �Լ� ȣ��. ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[currentWaveIndex]);
            enemySpawner.StartWave2(waves2[currentWaveIndex]);

            //gogogo = true;
        }
        waveStart = true;
    }
    public void StartWave3()
    {
        //mainBgm.backMusic.enabled = false;
        if (waveStart == false)
        {
            bgmController.ChangeBGM(BGMType.Stage);
        }

        if (currentWaveIndex == waves.Length - 2)
        {
            bgmController.ChangeBGM(BGMType.Boss);
            StartCoroutine("Boss");
        }


        dialog_war.SetActive(false);
        getGlod = true;
        wavePanel.SetActive(false);
        //wavePanel2.SetActive(false);

        // ���� �ʿ� ���� ����, Wave�� ���� ������
        // StartCoroutine("Delay03");
        if (enemySpawner.EnemyList.Count < 3 && currentWaveIndex < waves.Length - 1)
        {
            // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ���� ��
            currentWaveIndex++;
            // EnemySpawner�� StartWave() �Լ� ȣ��. ���� ���̺� ���� ����
            enemySpawner.StartWave(waves[currentWaveIndex]);
            enemySpawner.StartWave2(waves2[currentWaveIndex]);
            enemySpawner.StartWave3(waves3[currentWaveIndex]);

            //gogogo = true;
        }
        waveStart = true;
    }
    public void Update()
    {
        //25�� �̻� ��3��, 15���̻� �� 2��, 1���̻� �� 1��
        if (SceneManager.GetActiveScene().name == "Stage0")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage0", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore00"))
                {
                    if(PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore00", 1);
                    }
                }
            }    
        }
        else if (SceneManager.GetActiveScene().name == "Stage1")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage1", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore01"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore01", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore01", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore01", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage2", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore02"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore02", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore02", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore02", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage3", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore03"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore03", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore03", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore03", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage4")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage4", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore04"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore04", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore04", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore04", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage5")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage5", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore05"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore05", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore05", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore05", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage6")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage6", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore06"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore06", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore06", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore06", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage7")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage7", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore07"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore07", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore07", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore07", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage8")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage8", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore08"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore08", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore08", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore08", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage9")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage9", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore09"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore09", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore09", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore09", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage10")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage10", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore10"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore10", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore10", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore10", 1);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage11")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartCoroutine("Delay01");
                PlayerPrefs.SetInt("Stage11", 1);
                if (PlayerHP.currentHP > PlayerPrefs.GetInt("StageScore11"))
                {
                    if (PlayerHP.currentHP >= 25)
                    {
                        PlayerPrefs.SetInt("StageScore11", 3);
                    }
                    else if (PlayerHP.currentHP >= 15)
                    {
                        PlayerPrefs.SetInt("StageScore11", 2);
                    }
                    else if (PlayerHP.currentHP >= 1)
                    {
                        PlayerPrefs.SetInt("StageScore11", 1);
                    }
                }
            }
        }
        //if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex == waves.Length - 1 && PlayerHP.currentHP > 0 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
        //{
        //    StartCoroutine("Delay01"); 
        //}

        //if (enemySpawner.EnemyList.Count > 0)
        //{
        //    wavePanel.SetActive(false);
        //    if(wavePanel2 != null)
        //    {
        //        wavePanel2.SetActive(false);
        //    }
        //}
        if (SceneManager.GetActiveScene().name == "Stage0")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex != -1 && enemySpawner.wavedone == true)
            {
                StartWave();
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage1" || SceneManager.GetActiveScene().name == "Stage2" || SceneManager.GetActiveScene().name == "Stage3" || SceneManager.GetActiveScene().name == "Stage4" || SceneManager.GetActiveScene().name == "Stage5")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex != -1 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true)
            {
                //wavePanel.SetActive(true);
                //wavePanel.SetActive(false);
                StartWave2();

                //if (wavePanel2 != null)
                //{
                //    StartWave();
                //    //wavePanel2.SetActive(true);
                //}
                //else
                //{
                //}

                //StartCoroutine("HitColorAnimation");
                //playerGold.CurrentGold += 200;
                //if (getGlod == true)
                //{
                //    GetGold();
                //}
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage6" || SceneManager.GetActiveScene().name == "Stage7" || SceneManager.GetActiveScene().name == "Stage8" || SceneManager.GetActiveScene().name == "Stage9" || SceneManager.GetActiveScene().name == "Stage10" || SceneManager.GetActiveScene().name == "Stage11")
        {
            if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex != -1 && enemySpawner.wavedone == true && enemySpawner.wavedone2 == true && enemySpawner.wavedone3 == true)
            {
                StartWave3();
            }
        }
    }
    public void GetGold()
    {
        playerGold.CurrentGold += 100;
        getGlod = false;
    }
    //private IEnumerator HitColorAnimation()
    //{
    //    waveImage.color = Color.red;

    //    yield return new WaitForSeconds(3f);

    //    waveImage.color = Color.white;

    //}
    private IEnumerator Delay01()
    {
        bgmController.StopBGM();
        yield return new WaitForSeconds(2.5f);
        winPopup.SetActive(true);
        //yield return new WaitForSeconds(1.0f);
        //mainBgm.backMusic.enabled = true;
    }
    private IEnumerator Boss()
    {
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        textBossWarning.SetActive(false);
    }
}
[System.Serializable]
public struct Wave
{
    public float spawnTime; // ���� ���̺� �� ���� �ֱ�
    public int maxEnemyCount; // ���� ���̺� �� ���� ����
    public GameObject[] enemyPrefabs; // ���� ���̺� �� ���� ����
}

