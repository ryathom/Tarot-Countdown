using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip mainTheme;

    [Header("Ducking")]
    [SerializeField, Range(0f, 1f)]
    private float duckedVolume = 0.001f;

    [SerializeField]
    private float fadeDuration = 0.2f;

    private float normalVolume;
    private Coroutine duckCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        normalVolume = musicSource.volume;
    }

    private void Start()
    {
        PlayMusic(mainTheme);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("No music clip assigned.");
            return;
        }

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void DuckMusic(float duration)
    {
        if (duckCoroutine != null)
        {
            StopCoroutine(duckCoroutine);
        }

        duckCoroutine = StartCoroutine(DuckMusicRoutine(duration));
    }

    private IEnumerator DuckMusicRoutine(float duration)
    {
        yield return FadeVolume(musicSource.volume, duckedVolume);

        yield return new WaitForSecondsRealtime(duration);

        yield return FadeVolume(musicSource.volume, normalVolume);

        duckCoroutine = null;
    }

    private IEnumerator FadeVolume(float startVolume, float targetVolume)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            musicSource.volume = Mathf.Lerp(
                startVolume,
                targetVolume,
                elapsedTime / fadeDuration
            );

            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}