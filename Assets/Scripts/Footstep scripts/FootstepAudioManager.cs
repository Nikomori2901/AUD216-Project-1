using UnityEngine;
using System.Collections.Generic;

public class FootstepAudioManager : MonoBehaviour
{
    public List<SoundClip> dirtFootsteps;
    public List<SoundClip> grassFootsteps;
    public List<SoundClip> rockFootsteps;
    public List<SoundClip> sandFootsteps;
    public List<SoundClip> snowFootsteps;
    public List<SoundClip> swampFootsteps;
    public List<SoundClip> waterFootsteps;
    public List<SoundClip> woodFootsteps;

    public List<SoundClip> jumpSounds;
    public List<SoundClip> landSounds;

    private AudioSource audioSource;

    private SoundClip lastPlayedFootstep;
    private SoundClip lastPlayedJumpSound;
    private SoundClip lastPlayedLandSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    public void PlayFootstep(string surfaceType)
    {
        List<SoundClip> clips = null;
        switch (surfaceType)
        {
            
            case "dirt":
                clips = dirtFootsteps;
                break;
            case "grass":
                clips = grassFootsteps;
                break;
            case "sand":
                clips = sandFootsteps;
                break;
            case "rock":
                clips = rockFootsteps;
                break;
            case "snow":
                clips = snowFootsteps;
                break;
            case "swamp":
                clips = swampFootsteps;
                break;
            case "water":
                clips = waterFootsteps;
                break;
            case "wood":
                clips = woodFootsteps;
                break;
        }

        if (clips != null && clips.Count > 0)
        {
            PlayRandomSound(clips, ref lastPlayedFootstep);
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSounds != null && jumpSounds.Count > 0)
        {
            PlayRandomSound(jumpSounds, ref lastPlayedJumpSound);
        }
    }

    public void PlayLandSound()
    {
        if (landSounds != null && landSounds.Count > 0)
        {
            PlayRandomSound(landSounds, ref lastPlayedLandSound);
        }
    }

    private void PlayRandomSound(List<SoundClip> clips, ref SoundClip lastPlayed)
    {
        if (clips.Count == 1)
        {
            PlaySound(clips[0]);
            lastPlayed = clips[0];
            return;
        }

        SoundClip soundClip;
        do
        {
            soundClip = clips[Random.Range(0, clips.Count)];
        } while (soundClip == lastPlayed);

        PlaySound(soundClip);
        lastPlayed = soundClip;
    }

    private void PlaySound(SoundClip soundClip)
    {
        audioSource.clip = soundClip.clip;
        audioSource.volume = Random.Range(soundClip.volumeRange.x, soundClip.volumeRange.y);
        audioSource.pitch = Random.Range(soundClip.pitchRange.x, soundClip.pitchRange.y);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
