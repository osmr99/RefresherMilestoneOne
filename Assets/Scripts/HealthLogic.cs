using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLogic : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerStats currentHealth;
    [SerializeField] PlayerStats maxHealth;
    [SerializeField] Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth.amount = maxHealth.amount;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)currentHealth.amount / (float)maxHealth.amount;
        if(currentHealth.amount <= 0)
        {
            player.Respawn();
        }
    }
}
