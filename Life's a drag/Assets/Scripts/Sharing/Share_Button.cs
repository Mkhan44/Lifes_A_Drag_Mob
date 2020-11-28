using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Share_Button : MonoBehaviour
{
    private string shareMessage;
    public Current_level_manager levelType;

    public void clickShareButton(string time, string levelName)
    {
        if(levelType.currentState == Current_level_manager.stageType.normalStage)
        {
            shareMessage = "Check it out, I got " + time + " on " + levelName + " in Life's a Drag! \n download on Google Play: https://play.google.com/store/apps/details?id=com.Bukugames.Lifesadrag";
            Debug.Log("Regular mode message: " + shareMessage);
        }
        else
        {
            shareMessage = "Check it out, I had " + time + " seconds left on " + levelName + " in Challenge Mode in Life's a Drag! \n download on Google Play: https://play.google.com/store/apps/details?id=com.Bukugames.Lifesadrag";
            Debug.Log("Challenge mode message: " + shareMessage);
        }
        

        StartCoroutine(TakeScreenshotAndShare());
    }
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Life's a drag").SetText(shareMessage)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
