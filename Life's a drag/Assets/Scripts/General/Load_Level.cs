using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Load_Level : MonoBehaviour
{
    public GameObject loadingScreenPanel;
    public Slider slider;
    public TextMeshProUGUI progressText;
  public void LoadLevel(string sceneName)
  {
      StartCoroutine(loadingScreen(sceneName));
  }

  IEnumerator loadingScreen(string sceneName)
  {
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

      loadingScreenPanel.SetActive(true);

      while(!operation.isDone)
      {
          float progress = Mathf.Clamp01(operation.progress / .9f);
         

          slider.value = progress;
          progressText.text = progress * 100f + "%";

          yield return null;
      }
  }
}
