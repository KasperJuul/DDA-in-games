using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public int currenthealth;
    [SerializeField] private Image healthUI;

    private void Awake()
    {
        currenthealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;
        if (healthUI)
        {
            healthUI.transform.localScale = new Vector3( (float)currenthealth / (float)maxHealth , 1f , 1f);
        }
        if(currenthealth <= 0)
        {
            if (healthUI)
            {
                healthUI.transform.localScale = new Vector3(0f, 1f, 1f);
            }
            if(gameObject.tag == "Player")
            {
                FindObjectOfType<SceneController>().Death();
            }
            Destroy(gameObject);
            
        }

    }

         

    public void Heal(int amount)
    {
        currenthealth = currenthealth + amount < maxHealth ? currenthealth + amount : maxHealth;
        if (healthUI)
        {
            healthUI.transform.localScale = new Vector3((float)currenthealth / (float)maxHealth, 1f, 1f);
        }
    }
}
