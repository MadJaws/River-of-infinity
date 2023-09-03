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
        // Создайте новый GameObject для воспроизведения звука.
        GameObject soundObject = new GameObject("SoundObject");

        // Добавьте компонент AudioSource к этому GameObject.
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        // Установите AudioClip для воспроизведения.
        audioSource.clip = audioClip;

        // Настройте другие параметры, такие как громкость, петля и др.
        audioSource.volume = volume;
        // audioSource.loop = false; // Петля (true/false)

        // Воспроизведите звук.
        audioSource.Play();

        // Удалите GameObject после окончания воспроизведения, если не планируется его повторное использование.
        Destroy(soundObject, audioClip.length);
    }

    public static void StopSound(AudioClip audioClip)
    {
        // Логика остановки звука.
    }
}

