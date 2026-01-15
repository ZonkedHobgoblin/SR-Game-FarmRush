using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioClip backgroundMusic;
    private AudioClip menuMusic;
    private AudioClip fireAudio;
    private AudioClip gameoverAudio;
    private AudioClip damageAudio;
    private AudioClip uiClickAudio;
    private AudioClip loadAudio;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 0.5f;
        sfxSource.volume = 0.8f;

        backgroundMusic = Resources.Load<AudioClip>("Audio/Music");
        menuMusic = Resources.Load<AudioClip>("Audio/MenuMusic");
        fireAudio = Resources.Load<AudioClip>("Audio/Shoot");
        damageAudio = Resources.Load<AudioClip>("Audio/Damage");
        gameoverAudio = Resources.Load<AudioClip>("Audio/Gameover");
        uiClickAudio = Resources.Load<AudioClip>("Audio/UIClick");
        loadAudio = Resources.Load<AudioClip>("Audio/Load");
    }

    private void Start()
    {
        sfxSource.PlayOneShot(loadAudio);
        if (SceneManager.GetActiveScene().name == "MainLevel")
        {
            musicSource.clip = backgroundMusic;
        }
        else
        {
            musicSource.clip = menuMusic;
        }
        musicSource.Play();
        musicSource.loop = true;
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }
    }

    public void PlayUIClick()
    {
        sfxSource.PlayOneShot(uiClickAudio);
    }

    public void PlayDamageSound()
    {
        sfxSource.PlayOneShot(damageAudio);
    }

    public void PlayFireSound()
    {
        sfxSource.PlayOneShot(fireAudio);
    }

    public void PlayGameoverSound()
    {
        musicSource.Stop();
        sfxSource.PlayOneShot(gameoverAudio);
    }
}