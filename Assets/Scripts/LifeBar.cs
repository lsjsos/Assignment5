using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    Image image;
    public Life targetLife;
    private float maxAmount;

    private void Awake()
    {
        image = this.GetComponent<Image>();
        maxAmount = targetLife.amount;
    }

    private void Update()
    {
        image.fillAmount = targetLife.amount / maxAmount;
    }

}
