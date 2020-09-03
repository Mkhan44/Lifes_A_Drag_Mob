using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    public enum PurchaseType
    {
        removeAds
    }
    public PurchaseType purchaseType;

    private string noAdsKey = "noAdsKey";

    void Awake()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                {
                    if(PlayerPrefs.GetInt(noAdsKey) > 0)
                    {
                        this.GetComponent<Button>().interactable = false;
                    }
                    break;
                }
            default:
                {
                    Debug.Log("called default!");
                    break;
                }

        }
    }
    public void clickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                {
                    IAPManager.instance.buyRemoveAds();
                    if (PlayerPrefs.GetInt(noAdsKey) > 0)
                    {
                        this.GetComponent<Button>().interactable = false;
                    }
                    break;
                }       
            default:
                {
                    Debug.Log("called default!");
                    break;
                }
               
        }
    }
}
