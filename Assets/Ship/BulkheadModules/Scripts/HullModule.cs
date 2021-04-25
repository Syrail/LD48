using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullModule : MonoBehaviour
{ 
    private int health;
    public int MaxHealth;
    public GameObject healthListener;

    private bool uiActive = false;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        updateHealthDisplay();
    }

    private void updateHealthDisplay()
    {
        Vector3 oldScale = healthListener.transform.localScale;
        float healthScaler = (1.0f * health) / (1.0f * MaxHealth);
        healthListener.transform.localScale = new Vector3(3f*healthScaler, oldScale.y, oldScale.z);
        MeshRenderer mr;
        if (healthListener.TryGetComponent<MeshRenderer>(out mr))
        {
            mr.material.SetColor("_EmissionColor", Color.Lerp(Color.red, Color.green, healthScaler));
        }
    }

    public void PlayerEntered()
    {
        foreach (Interactable interactable in GetComponentsInChildren<Interactable>(true))
        {
            interactable.gameObject.SetActive(true);
        }
        health = (int)(Random.value * MaxHealth);
        updateHealthDisplay();
    }

    public void PlayerLeft()
    {
            foreach (Interactable interactable in GetComponentsInChildren<Interactable>(true))
            {
                interactable.gameObject.SetActive(false);
            }
    }

    public void Damage(int damageDealt)
    {
        health -= damageDealt;
        if(health <= 0)
        {
            //bad stuff!
        }
    }

}
