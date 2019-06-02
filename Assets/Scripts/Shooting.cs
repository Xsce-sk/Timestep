using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shooting : MonoBehaviourPunCallbacks
{
    public KeyCode shootKey;
    public GameObject projectile;
    public float projSpeed;
    public float offset;
    int layerMask = 1 << 9;

    [SerializeField]
    private Transform m_Transform;

    [SerializeField]
    private Vector3 m_MousePos;

    [SerializeField]
    private Vector3 m_ShootDir;

    [SerializeField]
    private bool debug;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }

        if (debug)
        {
            m_MousePos = Input.mousePosition;

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(m_MousePos);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            }
        }
    }

    public void Shoot()
    {
        // Make sure only controlling your player
        if (photonView.IsMine == false)
            return;

        m_MousePos = Input.mousePosition;

        //m_MousePos.z = 12;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(m_MousePos);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            m_ShootDir = hit.point;

            //m_ShootDir.z = m_Transform.position.z;

            GameObject proj = PhotonNetwork.Instantiate(projectile.name, m_Transform.position, Quaternion.identity);

            proj.transform.LookAt(m_ShootDir);

            proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * projSpeed, ForceMode.Impulse);
        }
    }
}
