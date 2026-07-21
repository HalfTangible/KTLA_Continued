using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Exact name of your playable game scene")]
    public string firstSceneName = "SampleScene";
    public Image fadeOverlay;
    public float fadeDuration = 1.0f;

    public void PlayGame()
    {
        Debug.Log("Loading game scene...");
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        // Fade out
        float elapsed = 0f;
        Color color = fadeOverlay.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }

        // Load the scene
        SceneManager.LoadScene(firstSceneName);
    }

    public void OptionsMenu()
    {
        Debug.Log("Options menu incomplete");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting application...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}