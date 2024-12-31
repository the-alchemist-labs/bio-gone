using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public enum BackgroundMusicId
{
    Main,
    Battle,
}

public enum SoundId
{
    Click,
    Step,
    Dice,
    ShopBell,
    Buy,
    CloseDoor,
    Coin,
    Exp,
    LoseLife,
    GainLife,
    PowerUp,
    WinBattle,
    LoseBattle,
    Flee,
    LevelUp,
    WinGame,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public float SFXVolume;
    public float BGMVolume;

    [SerializeField] private GameObject audioSourcePrefab;
    private ObjectPool<AudioSource> _pool;
    private Dictionary<SoundId, AudioClip> _soundAudioClips;
    private Dictionary<BackgroundMusicId, AudioClip> _bgmAudioClips;
    private AudioSource _bgmAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        SFXVolume = PlayerPrefs.GetFloat(PlayerPrefKeys.SFX_Volume, SFXVolume);
        BGMVolume = 0.08f; // PlayerPrefs.GetFloat(PlayerPrefKeys.BGM_Volume, BGMVolume);

        _pool = new ObjectPool<AudioSource>(
            createFunc: CreateAudioSource,
            actionOnGet: GetAudioSource,
            actionOnRelease: ReleaseAudioSource,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 30
        );

        _soundAudioClips = InitializeAudioClipDictionary<SoundId>("Audio/SFX");
        _bgmAudioClips = InitializeAudioClipDictionary<BackgroundMusicId>("Audio/BGM");
        _bgmAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void UpdateSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.SFX_Volume, volume);
        SFXVolume = volume;
    }

    public void UpdateBgVolume(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeys.BGM_Volume, volume);
        _bgmAudioSource.volume = volume;
    }

    public void PlaySound(SoundId soundId)
    {
        if (_soundAudioClips.TryGetValue(soundId, out AudioClip clip))
        {
            AudioSource audioSource = _pool.Get();
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(ReleaseAudioSourceAfterPlay(audioSource));
        }
    }

    public void PlayBGM(BackgroundMusicId backgroundMusicId)
    {
        if (_bgmAudioClips.TryGetValue(backgroundMusicId, out AudioClip clip))
        {
            if (clip.loadState != AudioDataLoadState.Loaded) clip.LoadAudioData();
            _bgmAudioSource.volume = BGMVolume;
            _bgmAudioSource.clip = clip;
            _bgmAudioSource.loop = true;
            _bgmAudioSource.Play();
        }
    }

    private AudioSource CreateAudioSource()
    {
        GameObject audioSourceObject = Instantiate(audioSourcePrefab.gameObject);
        audioSourceObject.SetActive(false);
        return audioSourceObject.GetComponent<AudioSource>();
    }

    private IEnumerator ReleaseAudioSourceAfterPlay(AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        _pool.Release(audioSource);
    }

    private void GetAudioSource(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(true);
        audioSource.volume = SFXVolume;
    }

    private void ReleaseAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.gameObject.SetActive(false);
    }

    private Dictionary<TEnum, AudioClip> InitializeAudioClipDictionary<TEnum>(string path) where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                id => id,
                id => Resources.Load<AudioClip>($"{path}/{id}")
            );
    }
}