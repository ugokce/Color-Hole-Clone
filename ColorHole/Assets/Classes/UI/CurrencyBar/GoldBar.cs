using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour , CurrencyBar
{
    public TextMeshProUGUI goldLabel;
    public Image goldImage;

    public void OnLevelCompleted(int levelNumber)
    {
        UpdateCurrency(levelNumber * 50);
    }

    public void PlayCurrencyAnimation()
    {
        goldImage.transform.DORotate(new Vector3(0, 180f, 0), .2f).SetLoops(10, LoopType.Incremental);
    }

    public void UpdateCurrency(int amount)
    {
        goldLabel.text = amount.ToString();
        PlayCurrencyAnimation();
    }

    private void Start()
    {
        EventManager.getInstance().playerEvents.onLevelCompleted.AddListener(OnLevelCompleted);
        UpdateCurrency(CurrencyController.GetCurrentGold());
    }
}
