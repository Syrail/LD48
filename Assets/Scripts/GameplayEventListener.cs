using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactable;
public class GameplayEventListener : MonoBehaviour
{
    public ShipMotion shipMotion;

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
                    Debug.Log("Upgrade slot " + interaction.slot);
                    break;
                case InteractionType.Repair:
                    HullModule mod;
                    if(interaction.transform.parent.TryGetComponent(out mod))
                    {
                        mod.Repair();
                    }
                    Debug.Log("Repair slot " + interaction.slot);
                    break;
            }
        }

        ButtonInteract animateButton;
        if (targetObj.TryGetComponent<ButtonInteract>(out animateButton))
        {
            animateButton.StartAnimation();
        }
    }


}
