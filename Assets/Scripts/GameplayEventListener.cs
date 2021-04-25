using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interactable;
public class GameplayEventListener : MonoBehaviour
{
    public ShipMotion shipMotion;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private const float ROTATE_ANGLE = 15f;
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
            Debug.Log("Interaction: " + interaction.interaction);
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

                    break;
            }
        } else
        {
            Debug.Log("No interaction on " + targetObj.ToString());
        }
    }


}
