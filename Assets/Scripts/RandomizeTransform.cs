using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTransform : MonoBehaviour
{
    Transform m_Transform;

    void Start()
    {
        m_Transform = transform;
        m_Transform.eulerAngles = new Vector3(0, Random.Range(-25, 25), 45);

        int randomSize = Random.Range(10, 15);
        m_Transform.localScale = new Vector3(randomSize, randomSize, 5);

        Vector3 currentPos = m_Transform.position;
        m_Transform.position = new Vector3(currentPos.x + Random.Range(-1, 1), currentPos.y + Random.Range(-1, 1), currentPos.z);
    }
}
