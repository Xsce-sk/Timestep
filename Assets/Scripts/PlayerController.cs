using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float maxSpeed = 10;

    float horizontalModifier;
    float verticalModifier;
    bool isMoving;

    ParticleSystem m_JumpParticleSystem;
    Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_JumpParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        UpdateModifiers();
        Move();
        LimitSpeed();
        UpdateParticleEffect();
    }

    void UpdateModifiers()
    {
        horizontalModifier = Input.GetAxisRaw("Horizontal");
        verticalModifier = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        Vector3 moveForce = new Vector3(horizontalModifier * moveSpeed, verticalModifier * moveSpeed, 0);
        m_Rigidbody.AddForce(moveForce);

        if (moveForce.magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void LimitSpeed()
    {
        if (m_Rigidbody.velocity.magnitude > maxSpeed)
        {
            Vector3 clampedVelocity = m_Rigidbody.velocity;
            clampedVelocity = new Vector3(Mathf.Clamp(clampedVelocity.x, -moveSpeed, moveSpeed),
                                          Mathf.Clamp(clampedVelocity.y, -moveSpeed, moveSpeed),
                                          clampedVelocity.z);
            m_Rigidbody.velocity = clampedVelocity;
        }
    }

    void UpdateParticleEffect()
    {
        if (m_JumpParticleSystem.emission.enabled != isMoving)
        {
            var emission = m_JumpParticleSystem.emission;
            emission.enabled = isMoving;
        }
    }
}
