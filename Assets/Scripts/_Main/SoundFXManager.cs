using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    [SerializeField] private AudioClip drawSound;

    [SerializeField] private AudioClip hoverSound;

    [SerializeField] private AudioClip playCardSound;

    [SerializeField] private AudioClip discardCardSound;

    [SerializeField] private AudioClip incorrectRunSound;

    [SerializeField] private AudioClip gainFateSound;

    [SerializeField] private AudioClip empressSound;

    [SerializeField] private AudioClip moonSound;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume, float pitch = 1f)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.pitch = pitch;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayDrawSoundClip(Transform spawnTransform)
    {
      PlaySoundFXClip(drawSound, spawnTransform, 0.1f, 0.95f);
    }

    public void PlayHoverSoundClip(Transform spawnTransform)
    {

        PlaySoundFXClip(hoverSound, spawnTransform, 0.01f);
    }

    public void PlayempressSoundClip(Transform spawnTransform)
    {

        MusicManager.Instance.DuckMusic(empressSound.length);

        PlaySoundFXClip(empressSound, spawnTransform, 0.10f);
    }

    public void PlayCardSoundClip(Transform spawnTransform)
    {
        PlaySoundFXClip(playCardSound, spawnTransform, 0.4f);
    }
    public void PlayDiscardSoundClip(Transform spawnTransform)
    {
        PlaySoundFXClip(discardCardSound, spawnTransform, 0.1f);
    }

    public void PlayIncorrectRunSound(Transform spawnTransform)
    {
        PlaySoundFXClip(incorrectRunSound, spawnTransform, 0.4f);
    }

    public void PlayGainFateSound(Transform spawnTransform)
    { 
        PlaySoundFXClip(gainFateSound, spawnTransform, 0.05f);
    }

    public void PlayMoonSoundClip(Transform spawnTransform)
    {
        MusicManager.Instance.DuckMusic(moonSound.length);
        PlaySoundFXClip(moonSound, spawnTransform, 0.6f, 0.95f);
    }
}
