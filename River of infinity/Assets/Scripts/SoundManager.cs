using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private SoundManager() { }

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        // �������� ����� GameObject ��� ��������������� �����.
        GameObject soundObject = new GameObject("SoundObject");

        // �������� ��������� AudioSource � ����� GameObject.
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        // ���������� AudioClip ��� ���������������.
        audioSource.clip = audioClip;

        // ��������� ������ ���������, ����� ��� ���������, ����� � ��.
        audioSource.volume = volume;
        // audioSource.loop = false; // ����� (true/false)

        // �������������� ����.
        audioSource.Play();

        // ������� GameObject ����� ��������� ���������������, ���� �� ����������� ��� ��������� �������������.
        Destroy(soundObject, audioClip.length);
    }

    public static void StopSound(AudioClip audioClip)
    {
        // ������ ��������� �����.
    }
}

