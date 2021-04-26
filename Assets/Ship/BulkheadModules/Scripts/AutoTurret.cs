using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    private bool readyToFire;
    public float fireCooldown;
    public float currentCooldown;
    public AudioClip fireSound;
    public GameObject ShotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        readyToFire = true;
    }

    public bool NotifyTargetInRange(GameObject target)
    {
        if(readyToFire)
        {
            TurretShot shot = Instantiate(ShotPrefab, transform).GetComponent<TurretShot>();
            shot.SetTarget(target);
            shot.SetOrigin(gameObject);
            StartCoroutine(FireAt(target));
            return true;
        }
        return false;
    }

    public IEnumerator FireAt(GameObject target)
    {   
        readyToFire = false;
        GetComponent<AudioSource>().PlayOneShot(fireSound);
        currentCooldown = fireCooldown;
        while(currentCooldown > 0.0f)
        {
            currentCooldown -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        readyToFire = true;
    }

}
