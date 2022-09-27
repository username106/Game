using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class characterStat : MonoBehaviour
{


    GameObject Dedtext;
    float lerpSpeed;

    float maxHealth = 100;
    float health;
    bool once_a_time;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        Dedtext = GameObject.Find("death");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!once_a_time) {
            Dedtext.SetActive(false);
            once_a_time=true;
        }
        
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage(20);
        }
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }
    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
        if(health <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Dedtext.SetActive(true);
        }
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
   
}

