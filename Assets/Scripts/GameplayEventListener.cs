using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactable;
public class GameplayEventListener : MonoBehaviour
{
    public ShipMotion shipMotion;
    public float resources;
    public float maxResources;
    public float energy;
    public float maxEnergy;
    public float baseEnergyDrain;
    public float repairCost;
    private int brokenParts;

    public GameObject resourceGauge;
    public GameObject energyGauge;

    private int energyDrain;

    private void Start()
    {
        brokenParts = 0;
        updateResourceGauge(resourceGauge, resources, maxResources);
        updateResourceGauge(energyGauge, energy, maxEnergy);
    }


    /*
     *  Handle player interaction with objects
     */
    public void OnInteract(RaycastHit target) {
        if(target.collider is null)
        {
            return;
        }
        
        GameObject targetObj = target.collider.gameObject;
        Interactable interaction; 
        if(targetObj.TryGetComponent<Interactable>(out interaction)) {
            switch (interaction.interaction)
            {
                case InteractionType.Thrusters:
                    shipMotion.EngageThruster(interaction.slot);
                    break;
                case InteractionType.Boost:
                    break;
                case InteractionType.Upgrade:
                    break;
                case InteractionType.Repair:
                    HullModule mod;
                    if (resources > repairCost && interaction.transform.parent.TryGetComponent(out mod) && mod.health < mod.MaxHealth)
                    {
                        resources -= repairCost;
                        updateResourceGauge(resourceGauge, resources, maxResources );
                        mod.Repair();
                    }
                    break;
            }
        }

        ButtonInteract animateButton;
        if (targetObj.TryGetComponent<ButtonInteract>(out animateButton))
        {
            animateButton.StartAnimation();
        }
    }

    public void addEnergy(float amt)
    {
        energy = Mathf.Min(energy + amt, maxEnergy);
        updateResourceGauge(energyGauge, energy, maxEnergy);
    }

    public void addResources(float amt)
    {
        resources = Mathf.Min(resources + amt, maxResources);
        updateResourceGauge(resourceGauge, resources, maxResources);
    }
    
    private void Update()
    {
        energy -= Time.deltaTime*baseEnergyDrain*(1 + brokenParts);
        if(energy < 0)
        {
            //game over, man!
            energy = 0;
        }
        updateResourceGauge(energyGauge, energy, maxEnergy);
    }

    public static void updateResourceGauge(GameObject gauge, float current, float max)
    {
        Vector3 oldScale = gauge.transform.localScale;
        float resourceScaler = (1.0f * current) / (1.0f * max);
        gauge.transform.localScale = new Vector3(2.2f * resourceScaler, oldScale.y, oldScale.z);
        gauge.transform.localPosition = new Vector3(-1.1f * (1f - resourceScaler), gauge.transform.localPosition.y, gauge.transform.localPosition.z);
    }



}
