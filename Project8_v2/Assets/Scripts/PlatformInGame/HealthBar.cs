using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image currentHealthBar;
    public Text ratioText;

    public void UpdateHealthBar(float health, float maxHealth)
    {
        float ratio = health / maxHealth;
        currentHealthBar.GetComponent<Image>().fillAmount = ratio;
        ratioText.text = health.ToString() + "/" + maxHealth.ToString();
    }

}
