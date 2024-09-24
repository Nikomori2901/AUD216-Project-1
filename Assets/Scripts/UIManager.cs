using UnityEngine;
using TMPro;
using DG.Tweening; // Ensure DOTween is referenced

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance;

    [Header("Interaction Prompt UI")]
    [SerializeField]
    private GameObject interactionPromptPanel; // Assign in Inspector

    [SerializeField]
    private TextMeshProUGUI interactionPromptText; // Assign in Inspector

    [SerializeField]
    private CanvasGroup interactionCanvasGroup; // Assign in Inspector

    [Header("Fading Settings")]
    [SerializeField]
    private float fadeDuration = 0.5f; // Duration for fade in/out

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Initially hide the interaction prompt
            if (interactionCanvasGroup != null)
            {
                interactionCanvasGroup.alpha = 0f;
            }
            else
            {
                Debug.LogWarning("CanvasGroup is not assigned in UIManager.");
            }
            interactionPromptPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Displays the interaction prompt with the given text using a fade-in effect.
    /// </summary>
    public void DisplayPrompt(string promptText)
    {
        if (interactionPromptPanel != null && interactionPromptText != null && interactionCanvasGroup != null)
        {
            interactionPromptText.text = promptText;
            interactionPromptPanel.SetActive(true);
            // Reset alpha to 0 before fading in
            interactionCanvasGroup.alpha = 0f;
            // Fade in
            interactionCanvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
            Debug.Log("DisplayPrompt called: " + promptText);
        }
        else
        {
            Debug.LogWarning("Interaction Prompt UI elements are not assigned in UIManager.");
        }
    }

    /// <summary>
    /// Hides the interaction prompt using a fade-out effect.
    /// </summary>
    public void HidePrompt()
    {
        if (interactionPromptPanel != null && interactionCanvasGroup != null)
        {
            // Fade out
            interactionCanvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    interactionPromptPanel.SetActive(false);
                });
            Debug.Log("HidePrompt called.");
        }
        else
        {
            Debug.LogWarning("Interaction Prompt Panel or CanvasGroup is not assigned in UIManager.");
        }
    }
}
