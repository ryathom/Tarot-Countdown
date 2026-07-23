using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    [SerializeField] private AudioClip[] drawSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayDrawSoundClip(Transform spawnTransform)
    {
        if (drawSounds == null || drawSounds.Length == 0)
        {
            Debug.LogWarning("No draw sounds assigned.");
            return;
        }

        int randomIndex = Random.Range(0, drawSounds.Length);
        AudioClip selectedSound = drawSounds[randomIndex];

        PlaySoundFXClip(selectedSound, spawnTransform, 0.01f);
    }
}