using UnityEngine;

public class PosterChanger : MonoBehaviour
{
    public Texture2D posterTexture;

    void Start()
    {
        if (posterTexture != null)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = posterTexture;
        }
    }
}
