using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;

    [Range(0, 1f)]
    public float currentValue = 0;

    public void SetValue(float amount)
    {
        currentValue = amount;

        DOTween.To(AnimateBar, progressBar.fillAmount, currentValue, 0.1f);
    }

    private void AnimateBar(float amount)
    {
        progressBar.fillAmount = amount;
    }
}
