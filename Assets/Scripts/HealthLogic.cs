using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLogic : MonoBehaviour
{
    [SerializeField] PlayerStats currentHealth;
    [SerializeField] PlayerStats maxHealth;
    [SerializeField] Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)currentHealth.amount / maxHealth.amount;
    }
}
