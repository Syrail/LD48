using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullModule : MonoBehaviour
{ 
    public enum DamageType
    {
        Impact =0,
        Energy =1,
    };

    public List<AudioClip> DamageSounds;
    public AudioClip warningSound;

    private int health;
    public int MaxHealth;
    public int index;
    public GameObject healthListener;
    
    private static int nextIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        updateHealthDisplay();
        index = HullModule.nextIndex;
        HullModule.nextIndex++;
        //assign this Bulkhead's index to the "Slot" parameter for all child interactables
        foreach(Interactable interactable in GetComponentsInChildren<Interactable>(true))
        {
            interactable.slot = index;
        }
    }

    private void updateHealthDisplay()
    {
        Vector3 oldScale = healthListener.transform.localScale;
        float healthScaler = (1.0f * health) / (1.0f * MaxHealth);
        healthListener.transform.localScale = new Vector3(3f*healthScaler, oldScale.y, oldScale.z);
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

    public void Damage(int damageDealt, DamageType type)
    {
        AudioSource src = GetComponent<AudioSource>();
        src.PlayOneShot(DamageSounds[(int)type]);
        health -= damageDealt;
        updateHealthDisplay();
        if(health <= 0)
        {
            health = 0;
            //bad stuff!
            //src.PlayOneShot(warningSound);
        }
    }

}
