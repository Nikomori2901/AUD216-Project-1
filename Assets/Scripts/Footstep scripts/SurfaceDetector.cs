using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public LayerMask layerMask; // Add a layer mask to exclude specific layers
    public bool DebugFootstepSurface; // Checkbox to toggle debug information

    public string GetSurfaceType(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, 20f, ~layerMask))
        {
            if (DebugFootstepSurface)
            {
                Debug.Log("Raycast hit: " + hit.collider.tag);
            }

            if (hit.collider.CompareTag("Wood"))
            {
                return "wood";
            }
            else if (hit.collider.CompareTag("Rock"))
            {
                return "rock";
            }
            else if (hit.collider.CompareTag("Water"))
            {
                return "water";
            }
            else if (hit.collider.CompareTag("Swamp"))
            {
                return "swamp";
            }

            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                TerrainTextureDetector terrainTextureDetector = terrain.GetComponent<TerrainTextureDetector>();
                if (terrainTextureDetector != null)
                {
                    int textureIndex = terrainTextureDetector.GetTextureAtPoint(hit.point);
                    string textureType = terrainTextureDetector.MapTextureToCategory(textureIndex);
                    if (DebugFootstepSurface)
                    {
                        Debug.Log("Terrain texture detected: " + textureType);
                    }
                    return textureType;
                }
                else
                {
                    if (DebugFootstepSurface)
                    {
                        Debug.Log("No TerrainTextureDetector found on terrain.");
                    }
                }
            }
            else
            {
                if (DebugFootstepSurface)
                {
                    Debug.Log("Raycast did not hit a terrain.");
                }
            }
        }
        return "default"; // Fallback surface type
    }
}
