using UnityEngine;

[System.Serializable]
public class SoundClip
{
    public AudioClip clip;
    public Vector2 volumeRange = new Vector2(0.95f, 1.0f);
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
}
