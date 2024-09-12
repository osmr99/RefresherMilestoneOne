using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] PlayerStats currentHealth;
    [SerializeField] PlayerStats maxHealth;
    public int damageAmount;
    public int damageMultiplier;
    public float animationTime;
    bool regen = false;

    // Update is called once per frame
    void Update()
    {
        if (currentHealth.amount <= 0)
            regen = true;

    }

    private void FixedUpdate()
    {
        if(regen)
        {
            if(currentHealth.amount < 100)
                currentHealth.amount++;
            else
                regen = false;
        }
    }

    public void enteredZone()
    {
        StartCoroutine(TakeDamage());
    }

    public void exitedZone()
    {
        StopAllCoroutines();
        //Debug.Log("Exited");
    }

    IEnumerator TakeDamage()
    {
        //Debug.Log("Entered");
        for (int i = 0; i < damageMultiplier; i++)
        {
            currentHealth.amount -= damageAmount;
            yield return new WaitForSeconds(animationTime);

        }
        yield return new WaitForSeconds(2.0f);
        if(currentHealth.amount > 0)
            StartCoroutine(stayingDamage());
        else
            StopCoroutine(stayingDamage());

    }

    IEnumerator stayingDamage()
    {
        //Debug.Log("Staying");
        for (int i = 0; i < damageMultiplier; i++)
        {
            currentHealth.amount -= damageAmount;
            yield return new WaitForSeconds(animationTime);
        }
        yield return new WaitForSeconds(2.0f);
        if (currentHealth.amount > 0)
            StartCoroutine(stayingDamage());
        else
            StopCoroutine(stayingDamage());
    }

}
