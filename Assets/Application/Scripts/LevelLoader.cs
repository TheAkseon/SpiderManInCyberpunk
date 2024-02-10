using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public TMP_Text progressText;

    public void LoadLevel (int sceneBuildIndex) 
    {
        StartCoroutine(LoadAsyncronously(sceneBuildIndex));
    }

    private IEnumerator LoadAsyncronously (int sceneBuildIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
