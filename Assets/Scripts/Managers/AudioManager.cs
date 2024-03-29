using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    [SerializeField] Slider volumeMusicSlider;
    [SerializeField] Slider volumeSoundSlider;

    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
            
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }

        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);
        volumeSoundSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    void Start()
    {

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
        }
        Load();


    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    private void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        audioMixer.SetFloat("MusicSound", Mathf.Log10(volumeMusicSlider.value) * 20);
        audioMixer.SetFloat("SfxSound", Mathf.Log10(volumeSoundSlider.value) * 20);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
        PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicSound", Mathf.Log10(value) * 20);
        Save();
    }

    private void SetSoundVolume(float value)
    {
        audioMixer.SetFloat("SfxSound", Mathf.Log10(value) * 20);
        Save();
    }

    private void OnLevelWasLoaded(int level)
    {
        Slider[] onlyInactiveSliders = FindObjectsByType<Slider>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
        volumeMusicSlider = onlyInactiveSliders.First(slider => slider.gameObject.name == "MusicSlider");
        volumeSoundSlider = onlyInactiveSliders.First(slider => slider.gameObject.name == "SfxSlider");

        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);
        volumeSoundSlider.onValueChanged.AddListener(SetSoundVolume);

        Load();
    }

}
