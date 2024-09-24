using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AreaAudio
    {
        [Tooltip("Set which area name for the sounds that will play in that area")]
        public string areaName;
        public AudioClip ambientClip;
        [Range(0f, 1f)]
        public float ambientVolume = 1.0f; // Volume slider for ambient sound
        public AudioClip musicClip;
        [Range(0f, 1f)]
        public float musicVolume = 1.0f; // Volume slider for music
        public AudioClip[] uniqueSounds;
        [Range(0f, 1f)]
        public float uniqueVolume = 1.0f; // Volume slider for unique sounds
    }

    public AreaAudio[] areas;
    public float fadeDuration = 2.0f;
    [Tooltip("Set which area will play on game start")]
    public string startingArea;  // Field to set the starting area

    private Dictionary<string, AudioSource> ambientSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> musicSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource[]> uniqueSources = new Dictionary<string, AudioSource[]>();
    private string currentArea = "";

    void Start()
    {
        foreach (var area in areas)
        {
            if (area.ambientClip != null)
            {
                AudioSource ambientSource = gameObject.AddComponent<AudioSource>();
                ambientSource.clip = area.ambientClip;
                ambientSource.loop = true;
                ambientSource.volume = 0;
                ambientSource.dopplerLevel = 0; // Set Doppler level to 0
                ambientSource.Play();
                ambientSources[area.areaName] = ambientSource;
            }

            if (area.musicClip != null)
            {
                AudioSource musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.clip = area.musicClip;
                musicSource.loop = true;
                musicSource.volume = 0;
                musicSource.dopplerLevel = 0; // Set Doppler level to 0
                musicSource.Play();
                musicSources[area.areaName] = musicSource;
            }

            if (area.uniqueSounds != null && area.uniqueSounds.Length > 0)
            {
                AudioSource[] uniqueSourceArray = new AudioSource[area.uniqueSounds.Length];
                for (int i = 0; i < area.uniqueSounds.Length; i++)
                {
                    AudioSource uniqueSource = gameObject.AddComponent<AudioSource>();
                    uniqueSource.clip = area.uniqueSounds[i];
                    uniqueSource.loop = true;
                    uniqueSource.volume = 0;
                    uniqueSource.dopplerLevel = 0; // Set Doppler level to 0
                    uniqueSource.Play();
                    uniqueSourceArray[i] = uniqueSource;
                }
                uniqueSources[area.areaName] = uniqueSourceArray;
            }
        }

        //Debug.Log("AudioManager: Initialized with AudioSources.");

        // Set the starting area if specified
        if (!string.IsNullOrEmpty(startingArea))
        {
            ChangeArea(startingArea, initialSetup: true);
        }
    }

    public void ChangeArea(string areaName, bool initialSetup = false)
    {
        //Debug.Log("AudioManager: Attempting to change area to: " + areaName);

        if (currentArea == areaName && !initialSetup)
        {
            //Debug.Log("AudioManager: Already in the area: " + areaName);
            return;
        }

        foreach (var area in areas)
        {
            if (area.areaName == areaName)
            {
                //Debug.Log("AudioManager: Changing to area: " + area.areaName);

                if (!string.IsNullOrEmpty(currentArea) && !initialSetup)
                {
                    StartCoroutine(FadeOutAudio(currentArea));
                }

                if (!initialSetup)
                {
                    StartCoroutine(FadeInAudio(area.areaName));
                }
                else
                {
                    SetInitialVolume(area.areaName);
                }

                currentArea = areaName;
                break;
            }
        }
    }

    private void SetInitialVolume(string areaName)
    {
        foreach (var area in areas)
        {
            if (area.areaName == areaName)
            {
                if (ambientSources.ContainsKey(areaName))
                {
                    ambientSources[areaName].volume = area.ambientVolume;
                }

                if (musicSources.ContainsKey(areaName))
                {
                    musicSources[areaName].volume = area.musicVolume;
                }

                if (uniqueSources.ContainsKey(areaName))
                {
                    foreach (var uniqueSource in uniqueSources[areaName])
                    {
                        uniqueSource.volume = area.uniqueVolume;
                    }
                }
                break;
            }
        }
    }

    private IEnumerator FadeInAudio(string areaName)
    {
        float currentTime = 0;

        foreach (var area in areas)
        {
            if (area.areaName == areaName)
            {
                if (ambientSources.ContainsKey(areaName))
                {
                    AudioSource ambientSource = ambientSources[areaName];
                    while (currentTime < fadeDuration)
                    {
                        currentTime += Time.deltaTime;
                        ambientSource.volume = Mathf.Lerp(0, area.ambientVolume, currentTime / fadeDuration);
                        yield return null;
                    }
                    ambientSource.volume = area.ambientVolume;
                }

                if (musicSources.ContainsKey(areaName))
                {
                    AudioSource musicSource = musicSources[areaName];
                    currentTime = 0;
                    while (currentTime < fadeDuration)
                    {
                        currentTime += Time.deltaTime;
                        musicSource.volume = Mathf.Lerp(0, area.musicVolume, currentTime / fadeDuration);
                        yield return null;
                    }
                    musicSource.volume = area.musicVolume;
                }

                if (uniqueSources.ContainsKey(areaName))
                {
                    AudioSource[] uniqueSourceArray = uniqueSources[areaName];
                    foreach (var uniqueSource in uniqueSourceArray)
                    {
                        currentTime = 0;
                        while (currentTime < fadeDuration)
                        {
                            currentTime += Time.deltaTime;
                            uniqueSource.volume = Mathf.Lerp(0, area.uniqueVolume, currentTime / fadeDuration);
                            yield return null;
                        }
                        uniqueSource.volume = area.uniqueVolume;
                    }
                }
                break;
            }
        }
    }

    private IEnumerator FadeOutAudio(string areaName)
    {
        float currentTime = 0;

        foreach (var area in areas)
        {
            if (area.areaName == areaName)
            {
                if (ambientSources.ContainsKey(areaName))
                {
                    AudioSource ambientSource = ambientSources[areaName];
                    while (currentTime < fadeDuration)
                    {
                        currentTime += Time.deltaTime;
                        ambientSource.volume = Mathf.Lerp(area.ambientVolume, 0, currentTime / fadeDuration);
                        yield return null;
                    }
                    ambientSource.volume = 0;
                }

                if (musicSources.ContainsKey(areaName))
                {
                    AudioSource musicSource = musicSources[areaName];
                    currentTime = 0;
                    while (currentTime < fadeDuration)
                    {
                        currentTime += Time.deltaTime;
                        musicSource.volume = Mathf.Lerp(area.musicVolume, 0, currentTime / fadeDuration);
                        yield return null;
                    }
                    musicSource.volume = 0;
                }

                if (uniqueSources.ContainsKey(areaName))
                {
                    AudioSource[] uniqueSourceArray = uniqueSources[areaName];
                    foreach (var uniqueSource in uniqueSourceArray)
                    {
                        currentTime = 0;
                        while (currentTime < fadeDuration)
                        {
                            currentTime += Time.deltaTime;
                            uniqueSource.volume = Mathf.Lerp(area.uniqueVolume, 0, currentTime / fadeDuration);
                            yield return null;
                        }
                        uniqueSource.volume = 0;
                    }
                }
                break;
            }
        }
    }
}
