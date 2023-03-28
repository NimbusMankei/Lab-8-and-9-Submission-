using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInventoryStats))]
public class CharacterHealth : MonoBehaviour
{
    public GameObject uiPrefab;

    public Transform target;

    Transform ui;
    Image healthSlider;

    private void Start()
    {
        ui = Instantiate(uiPrefab, target).transform;
        ui.SetParent(target);
        healthSlider = ui.GetChild(0).GetComponent<Image>();

        GetComponent<PlayerInventoryStats>().OnHealthChanged += OnHealthChanged;


        Debug.Log(GetComponent<PlayerInventoryStats>().CurrentHealth);
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            float healthPercent = (float)currentHealth/maxHealth;
            healthSlider.fillAmount = healthPercent;

            if(currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
}
