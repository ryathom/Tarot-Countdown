using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip mainTheme;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayMusic(mainTheme);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}