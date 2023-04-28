using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private TowerDataViewer towerDataViewer;
    [SerializeField]
    private EnemyDataViewer enemyDataViewer;
    [SerializeField]
    private GameObject[] skillPrefab;

    //[SerializeField]
    //private Skill skill;

    private Camera mainCamera;
    private Ray ray;
    //private Ray2D ray2d;
    private RaycastHit hit;
    //private RaycastHit2D hit2d;
    private Transform hitTransform = null; // 마우스 픽킹으로 선택한 오브젝트 임시 저장

    public GameObject make;
    public GameObject upButton;
    public GameObject sellButton;

    public GameObject check;
    public GameObject skillCheck;

    public GameObject infoPanel;
    public GameObject enemyInfoPanel;
    public GameObject dialog_make;
    public GameObject dialog_skill;
    public GameObject dialog_skill01;

    public bool firstMake;
    public bool firstSkill;

    public Button[] skillButton;

    public bool[] checkbox;
    public bool[] checkboxSkill;

    public int[] skillCount = { 5,4,2,2,2,0 };

    public TextMeshProUGUI[] skillCountText;
    private Vector3 distance = Vector3.up * 0.0f;
    private TowerWeapon currentTower;

    //public AudioSource winSound;
    private void Awake()
    {
        // "MainCamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
        // GameObject.findGameObjectWithTag("MainCamera").GetComponent<Camera>(); 와 동일
        mainCamera = Camera.main;
        for(int i = 0; i < skillCountText.Length; i++)
        {
            skillCountText[i].text = "" + skillCount[i];
        }

    }
    public void Start()
    {
        //winSound = GetComponent<AudioSource>();
    }
    //public void fireboll(GameObject clone)
    //{
    //    StartCoroutine("FireBoll");

    //}

    //private IEnumerator FireBoll(GameObject clone)
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    Destroy(gameObject);
    //}
    private void Update()
    {
        // 마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함 
        // ()에 아무것도 없으면 윈도우, 0넣으면 모바일
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        //
        // 마우스 왼쪽 버튼을 눌렀을 때
        
        if (Input.GetMouseButtonDown(0))
        {
            
            // 카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성 
            // ray.origin : 광선의 시작위치(=카메라 위치)
            // ray.direction : 광선의 진행방향
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if(checkboxSkill[0] == true && skillCount[0] >= 1) // 파이어볼
            {
                //if (hit2D.transform.CompareTag("Tile"))
                //{
                //    return;
                //}

                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * 2.5f + Vector3.right * 0.3f;
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[0], mPosition, Quaternion.identity);
                
                AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();
                
                manager.SfxPlay(AudioManager.Sfx.Boom);

                //for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                //{
                //    towerDataViewer.checkbox[i] = false;
                //}
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }         
                else if(firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }

                skillCount[0]--;

                skillCountText[0].text = "" + skillCount[0];
                if (skillCount[0] == 0)
                {
                    //체크박스 정리
                    for (int i = 0; i < checkboxSkill.Length; i++)
                    {
                        checkboxSkill[i] = false;
                    }
                    skillCheck.SetActive(false);

                }

            }
            if (checkboxSkill[1] == true && skillCount[1] >= 1) // 썬더볼트
            {
                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * 4.5f + Vector3.right * 0.5f;
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[1], mPosition, Quaternion.identity);

                AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();

                manager.SfxPlay(AudioManager.Sfx.Thunder);

                //for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                //{
                //    towerDataViewer.checkbox[i] = false;
                //}
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }
                else if (firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }
                skillCount[1]--;

                skillCountText[1].text = "" + skillCount[1];
                if (skillCount[1] == 0)
                {
                    //체크박스 정리
                    for (int i = 0; i < checkboxSkill.Length; i++)
                    {
                        checkboxSkill[i] = false;
                    }
                    skillCheck.SetActive(false);

                }
            }
            if (checkboxSkill[2] == true && skillCount[2] >= 1) // 롤링스톤
            {
                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * 2.6f + Vector3.right * 0.4f;
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[2], mPosition, Quaternion.identity);

                //AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();

                //manager.SfxPlay(AudioManager.Sfx.Boom);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Eff02");
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }
                else if (firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }
                skillCount[2]--;

                skillCountText[2].text = "" + skillCount[2];
                if (skillCount[2] == 0)
                {
                    //체크박스 정리
                    for (int i = 0; i < checkboxSkill.Length; i++)
                    {
                        checkboxSkill[i] = false;
                    }
                    skillCheck.SetActive(false);

                }
            }
            if (checkboxSkill[3] == true) //블리자드
            {
                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * 2.8f + Vector3.right * 0.3f;
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[3], mPosition, Quaternion.identity);

                //AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();

                //manager.SfxPlay(AudioManager.Sfx.Boom);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Eff03");
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }
                else if (firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }
                skillCount[3]--;

                skillCountText[3].text = "" + skillCount[3];
                if (skillCount[3] == 0)
                {
                    //체크박스 정리
                    for (int i = 0; i < checkboxSkill.Length; i++)
                    {
                        checkboxSkill[i] = false;
                    }
                    skillCheck.SetActive(false);

                }
            }
            if (checkboxSkill[4] == true) // 포이즌레인
            {
                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.up * 2.7f + Vector3.right * 0.3f;
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[4], mPosition, Quaternion.identity);

                //AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("Eff04");
                //manager.SfxPlay(AudioManager.Sfx.Boom);
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }
                else if (firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }
                skillCount[4]--;

                skillCountText[4].text = "" + skillCount[4];
                if (skillCount[4] == 0)
                {
                    //체크박스 정리
                    for (int i = 0; i < checkboxSkill.Length; i++)
                    {
                        checkboxSkill[i] = false;
                    }
                    skillCheck.SetActive(false);

                }
            }
            if (checkboxSkill[5] == true) // 썬더팔콘
            {
                Vector3 mPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mPosition.z = 0;
                GameObject clone = Instantiate(skillPrefab[5], mPosition, Quaternion.identity);
                GameObject clone1 = Instantiate(skillPrefab[6], mPosition, Quaternion.identity);
                GameObject clone2 = Instantiate(skillPrefab[7], mPosition, Quaternion.identity);

                //AudioManager manager = GameObject.Find("GameManager").GetComponent<AudioManager>();

                //manager.SfxPlay(AudioManager.Sfx.Boom);
                SoundManager manager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                manager.Play("TF");
                if (firstSkill == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    firstSkill = true;
                    dialog_skill.SetActive(false);
                    dialog_skill01.SetActive(true);
                }
                else if (firstSkill == true)
                {
                    dialog_skill.SetActive(false);
                }
                skillCount[5]--;

                skillCountText[5].text = "" + skillCount[5];
                //if (skillCount[5] == 0)
                //{
                //    //체크박스 정리
                //    for (int i = 0; i < checkboxSkill.Length; i++)
                //    {
                //        checkboxSkill[i] = false;
                //    }
                //    skillCheck.SetActive(false);

                //}
                checkboxSkill[5] = false;
                skillCheck.SetActive(false);
            }
            if (hit2D.collider != null)
            {
                //Destroy(hit2D.collider.gameObject);
                if (hit2D.transform.CompareTag("Enemy"))
                {

                    enemyInfoPanel.SetActive(true);

                    enemyDataViewer.OnPanel(hit2D.transform/*, hit2D.transform, hit2D.transform*/);
                    enemyDataViewer.UpdateEnemyData();
                    Debug.Log("Enemy");
                    //업데이트 데이터 만들기
                    //EnemyHPViewer.OnPanel(hit.transform);
                    //check.SetActive(false);
                    infoPanel.SetActive(false);
                    towerDataViewer.OffPanel();
                    upButton.SetActive(false);
                    sellButton.SetActive(false);
                    make.SetActive(false);

                    // 체크박스 해제
                    for (int i = 0; i < checkbox.Length; i++)
                    {
                        checkbox[i] = false;
                    }
                    for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                    {
                        towerDataViewer.checkbox[i] = false;
                    }
                }
                
                //if (hit2D.transform.CompareTag("Item"))
                //{
                //    Debug.Log("Item");
                    
                //}
            }
            else if (hit2D.collider == null)
            {
                towerDataViewer.OffPanel();
                enemyInfoPanel.SetActive(false);
                
                if(firstMake == false && SceneManager.GetActiveScene().name == "Stage0")
                {
                    dialog_make.SetActive(true);
                }

                make.SetActive(false);
                upButton.SetActive(false);
                sellButton.SetActive(false);
                check.SetActive(false);

                // 체크박스 해제
                for (int i = 0; i < checkbox.Length; i++)
                {
                    checkbox[i] = false;
                }
                for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                {
                    towerDataViewer.checkbox[i] = false;
                }
            }

            // 2D 모니터를 통해 3D 월드의 오브젝트를 마우스로 선택하는 방법
            // 광선에 부딪히는 오브젝트를 검출해서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                // 광선에 부딪힌 오브젝트의 태그가 "Tile"이면
                if (hit.transform.CompareTag("Tile"))
                {
                    //towerSpawner.SpawnTower(hit.transform, 1);
                    //Debug.Log("tile");
                    // 타워를 생성하는 SpawnTower() 호출
                    //towerSpawner.SpawnTower(hit.transform);
                    //AudioSource manager = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
                    //manager.Play();
                    towerDataViewer.OffPanel();
                    enemyInfoPanel.SetActive(false);
                    dialog_make.SetActive(false);

                    upButton.SetActive(false);
                    sellButton.SetActive(false);
                    make.transform.position = hit.transform.position + distance;
                    make.SetActive(true);
                    check.SetActive(false);
                    // 체크박스 해제
                    for (int i = 0; i < checkbox.Length; i++)
                    {
                        checkbox[i] = false;
                    }
                    for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                    {
                        towerDataViewer.checkbox[i] = false;
                    }
                }
                // 타워를 선택하면 해당 타워 정보를 출력하는 타워 정보창 On
                else if (hit.transform.CompareTag("Tower") && checkboxSkill[1] == false)
                {
                    Debug.Log("tower");
                    //AudioSource manager = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
                    //manager.Play();
                    towerDataViewer.OnPanel(hit.transform);
                    make.SetActive(false);
                    //check.SetActive(false);
                    enemyInfoPanel.SetActive(false);

                    // 출력해야하는 타워 정보를 받아와서 저장
                    currentTower = hit.transform.GetComponent<TowerWeapon>();
                    if (currentTower.upDone == false)
                    {
                        upButton.transform.position = hit.transform.position + Vector3.up * 1.5f;
                        sellButton.transform.position = hit.transform.position + Vector3.down * 1.5f;

                        upButton.SetActive(true);
                        sellButton.SetActive(true);
                    }
                    else
                    {
                        sellButton.transform.position = hit.transform.position + Vector3.down * 1.5f;
                        sellButton.SetActive(true);
                    }


                    // 체크박스 해제
                    for (int i = 0; i < checkbox.Length; i++)
                    {
                        checkbox[i] = false;
                    }
                    for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
                    {
                        towerDataViewer.checkbox[i] = false;
                    }

                }
                //else if (hit.transform.CompareTag("Enemy"))
                //{
                //    Debug.Log("Enemy");
                //    towerDataViewer.OffPanel();

                //    enemyInfoPanel.SetActive(true);
                //}
                //hitTransform = null;

                    //else if (hit.transform.)
                    //{
                    //    Debug.Log("untag");
                    //    towerDataViewer.OffPanel();
                    //}
            }
            //else if (hitTransform == null || hitTransform.CompareTag("Tower") == false || hitTransform.CompareTag("Tile") == false)
            //{
            //    //if (hit2D.transform.CompareTag("Enemy") == true)
            //    //{
            //    //    return;
            //    //}

            //    //if (checkbox[9] == true)
            //    //{

            //    //    Vector3 screenPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //    //    GameObject clone = Instantiate(skillPrefab, screenPosition, Quaternion.identity);
            //    //    Debug.Log("dd");
            //    //}

            //    // 타워 정보 패널을 비활성화 한다
            //    towerDataViewer.OffPanel();
            //    enemyInfoPanel.SetActive(false);

            //    make.SetActive(false);
            //    upsell.SetActive(false);
            //    check.SetActive(false);

            //    // 체크박스 해제
            //    for (int i = 0; i < checkbox.Length; i++)
            //    {
            //        checkbox[i] = false;
            //    }
            //    for (int i = 0; i < towerDataViewer.checkbox.Length; i++)
            //    {
            //        towerDataViewer.checkbox[i] = false;
            //    }

            //}
        }
        for (int i = 0; i < skillCount.Length; i++)
        {
            if (skillCount[i] == 0)
            {
                skillButton[i].interactable = false;
            }
            else
            {
                skillButton[i].interactable = true;
            }
        }
        
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    // 마우스를 눌렀을 때 선택한 오브젝트가 없거나 선택한 오브젝트가 타워가 아니면
        //    if (hitTransform == null || hitTransform.CompareTag("Tower") == false || hitTransform.CompareTag("Tile") == false)
        //    {
        //        // 타워 정보 패널을 비활성화 한다
        //        towerDataViewer.OffPanel();
        //        make.SetActive(false);
        //        upsell.SetActive(false);
        //        check.SetActive(false);

        //        for (int i = 0; i < checkbox.Length; i++)
        //        {
        //            checkbox[i] = false;

        //        }

        //    }
        //    //hitTransform = null;
        //}
    }

    public void Spawn(int id)
    {
        towerSpawner.SpawnTower(hit.transform, id);

        make.transform.position = hit.transform.position;

        //if (firstMake == false && SceneManager.GetActiveScene().name == "Stage00")
        //{
        //    firstMake = true;
        //    dialog_make.SetActive(false);
        //    dialog_make01.SetActive(true);
        //}
        //else if (firstMake == true)
        //{
        //    dialog_skill.SetActive(false);
        //}
        firstMake = true;
        dialog_make.SetActive(false);
        make.SetActive(false);
        //GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        //if (checkbox[id] == false)
        //{
        //    check.transform.position = clickObject.transform.position;
        //    check.SetActive(true);
        //    infoPanel.SetActive(true);
        //    towerDataViewer.UpdateTowerData00(id);

        //    for (int i = 0; i < checkbox.Length; i++)
        //    {

        //        if (i == id)
        //        {
        //            checkbox[i] = true;
        //        }
        //        else
        //            checkbox[i] = false;
        //    }

        //    //towerDataViewer.OnPanel(hit.transform);

        //}
        //else if (checkbox[id] == true)
        //{
        //    towerSpawner.SpawnTower(hit.transform, id);

        //    make.transform.position = hit.transform.position;

        //    dialog_make.SetActive(false);
        //    make.SetActive(false);
        //    check.SetActive(false);
        //    infoPanel.SetActive(false);
        //    firstMake = true;
        //    checkbox[id] = false;
        //}
    }
    public void SpawnSkill(int id)
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        if (checkboxSkill[id] == false)
        {
            skillCheck.transform.position = clickObject.transform.position;
            skillCheck.SetActive(true);
            infoPanel.SetActive(true);
            enemyInfoPanel.SetActive(false);
            towerDataViewer.UpdateSkillData(id);
            make.SetActive(false);

            for (int i = 0; i < checkboxSkill.Length; i++)
            {
                if (i == id)
                {
                    checkboxSkill[i] = true;
                }
                else
                    checkboxSkill[i] = false;
            }
        }
        else if (checkboxSkill[id] == true)
        {
            checkboxSkill[id] = false;
            infoPanel.SetActive(false);
            skillCheck.SetActive(false);
            enemyInfoPanel.SetActive(false);
            dialog_skill01.SetActive(false);
            make.SetActive(false);
        }
    }
}