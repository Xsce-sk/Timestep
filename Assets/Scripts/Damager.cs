using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Header("Settings")]
    public bool showDebug;

    [SerializeField]
    private int damage;

    void OnTriggerEnter(Collider other)
    {
        IDamageable damageableComponent = other.GetComponent<IDamageable>();
        if(damageableComponent != null)
        {
            damageableComponent.LoseHealth(damage);

            if(showDebug)
            {
                Debug.Log(other.name + " was hit and they have a damageableComponent.");
            }
        }

        Destroy(this.gameObject);
    }
}
