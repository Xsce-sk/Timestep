using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
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

    void Update()
    {
        UpdateModifiers();
        Move();
        LimitSpeed();
        UpdateParticleEffect();
    }

    void UpdateModifiers()
    {
        // Make sure only controlling your player
        if (photonView.IsMine == false)
            return;

        horizontalModifier = Input.GetAxisRaw("Horizontal");
        verticalModifier = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        // Make sure only controlling your player
        if (photonView.IsMine == false)
            return;

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
        if(isMoving && !m_JumpParticleSystem.isPlaying)
        {
            Debug.Log("Hey we are moving!!");
            m_JumpParticleSystem.Play();
        }
        else if(!isMoving && m_JumpParticleSystem.isPlaying)
        {
            m_JumpParticleSystem.Stop();
        }
    }

    // Streams whether the local player is moving or not to the other users.
    // Enables/disables the particle system for player across all clients.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isMoving);
        }
        else
        {
            this.isMoving = (bool)stream.ReceiveNext();
        }
    }
}
