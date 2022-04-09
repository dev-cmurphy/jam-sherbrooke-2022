using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FriendlyNPC : MonoBehaviour
{
    private NavMeshAgent m_ai;

    private void Awake()
    {
        m_ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        m_ai.destination = PlayerMovement.PlayerPosition;
    }
}
