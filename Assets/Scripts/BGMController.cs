using UnityEngine;

public enum BGMType { Stage = 0 , Boss }
public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips; // ������� ���� ���
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        //���� ��� ���� ������� ����
        audioSource.Stop();

        // Tip. �ٸ� Ŭ�������� BGM�� ������ �� ������ ����ϸ� � BGM�� ������� Ȯ���Ϸ���
        // Inspector View�� bgmClip[]�� Ȯ���ؾ� �� �� �ֱ� ������ �������� �̿��� �������� �����ش�.

        // ������� ���� ��Ͽ��� index��° ����������� ���� ��ü
        audioSource.clip = bgmClips[(int)index];
        //�ٲ� ������� ���
        audioSource.Play();
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }

}
