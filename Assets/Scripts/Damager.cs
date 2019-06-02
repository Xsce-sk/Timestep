using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damager : MonoBehaviour
{
    [Header("Settings")]
    public bool showDebug;

    [Header("Debug")]
    [SerializeField] private int m_Damage;
    [SerializeField] private GameObject m_Shooter; // Sort of gross way to see who shot the projectile, that way you don't hit yourself

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile collided with " + other.gameObject.name);
        if (other.gameObject.name != m_Shooter.name && m_Shooter != null)
        {
            IDamageable damageableComponent = other.GetComponent<IDamageable>();
            if (damageableComponent != null)
            {
                damageableComponent.LoseHealth(m_Damage);

                if (showDebug)
                {
                    Debug.Log(other.name + " was hit and they have a damageableComponent.");
                }
                PhotonNetwork.Destroy(this.gameObject); // use to destroy objects on the network
            }

            
        }
    }

    public void SetShooter(GameObject shooter)
    {
        m_Shooter = shooter;
        Debug.Log("Projectile shooter set to " + m_Shooter.name);
    }
}
