using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Hellmade.Sound;
public class Load_Level : MonoBehaviour
{
    public GameObject loadingScreenPanel;
    public Slider slider;
    public TextMeshProUGUI progressText;
  public void LoadLevel(string sceneName)
  {
      EazySoundManager.StopAll();
      StartCoroutine(loadingScreen(sceneName));
  }

  IEnumerator loadingScreen(string sceneName)
  {
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

      loadingScreenPanel.SetActive(true);

      while(!operation.isDone)
      {
          float progress = Mathf.Clamp01(operation.progress / .9f);
         
          //Round the answer!!!!

          slider.value = progress;
          progressText.text = Mathf.Round(progress * 100f) + "%";

          yield return null;
      }
  }
}
