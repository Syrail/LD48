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
    public float cameraShakeDuration = 0.3f;

    private int health;
    public int MaxHealth;
    public int index;
    public GameObject healthListener;
    public GameObject aliveNode;
    public GameObject deadNode;

    Shake cameraShake = null;

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
        cameraShake = Camera.main.GetComponent<Shake>();

    }

    private void updateHealthDisplay()
    {
        Vector3 oldScale = healthListener.transform.localScale;
        float healthScaler = (1.0f * health) / (1.0f * MaxHealth);
        healthListener.transform.localScale = new Vector3(6f*healthScaler, oldScale.y, oldScale.z);
        healthListener.transform.localPosition = new Vector3(-3f*(1f - healthScaler), healthListener.transform.localPosition.y, healthListener.transform.localPosition.z);
    }

    public void PlayerEntered()
    {
        foreach (Interactable interactable in GetComponentsInChildren<Interactable>(true))
        {
            interactable.gameObject.SetActive(true);
        }
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
        
        updateHealthDisplay();
        if (damageDealt > health && health > 0)
        {
            health = 0;
            aliveNode.SetActive(false);
            deadNode.SetActive(true);
            src.PlayOneShot(warningSound);
        }
        else if( health > 0)
        {
            health -= damageDealt;
        }
        if (cameraShake != null)
        {
            //camera shake and duration could be proportional to how hard the impact was?
            StartCoroutine(cameraShake.StartShake(cameraShakeDuration));
        }
    }

}
