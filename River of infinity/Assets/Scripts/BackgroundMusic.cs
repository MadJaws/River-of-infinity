using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> randomSounds;
    [SerializeField] private AudioClip fixedSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(PlayRandomAndFixedSounds());
    }

    private IEnumerator PlayRandomAndFixedSounds()
    {
        while (true)
        {
            // Воспроизводим случайный звук
            if (randomSounds.Count > 0)
            {
                int randomIndex = Random.Range(0, randomSounds.Count);
                AudioClip randomClip = randomSounds[randomIndex];
                if (randomClip.name == "skibidiBGSong3" || randomClip.name == "skibidiBGSong4" || randomClip.name == "skibidiBGSong1" ||
                    randomClip.name == "skibidiBGSong5" || randomClip.name == "Djskinieisong")
                {
                    audioSource.volume = 0.4f;
                    audioSource.PlayOneShot(randomClip);
                    yield return new WaitForSeconds(randomClip.length);
                }
                else
                {
                    audioSource.PlayOneShot(randomClip);
                    yield return new WaitForSeconds(randomClip.length);
                }
                
            }

            // Воспроизводим фиксированный звук
            audioSource.clip = fixedSound;
            audioSource.loop = false;
            audioSource.Play();
            yield return new WaitForSeconds(fixedSound.length);
        }
    }
}

