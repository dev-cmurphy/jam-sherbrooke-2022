using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FriendlyNPC : MonoBehaviour
{
    private NavMeshAgent m_ai;
    private Vector3 m_currentRoamingDestination;

    private void Awake()
    {
        m_ai = GetComponent<NavMeshAgent>();
        m_currentRoamingDestination = transform.position;
    }

    private void Update()
    {
        if (!m_ai.enabled)
            return;

        if (transform.position.FlatDistance(m_currentRoamingDestination) < 0.5f)
        {
            do
            {
                m_currentRoamingDestination = RoamingController.NPCInstance.AvailableRoamingPoint(gameObject);
            } while (!m_ai.SetDestination(m_currentRoamingDestination));

            Debug.Log("New destination for " + name + " : " + m_ai.destination);
        }

        m_ai.SetDestination(m_currentRoamingDestination);
    }

    public void Interact()
    {
        StartCoroutine(InteractionCoroutine(5f));
    }

    private IEnumerator InteractionCoroutine(float interactionTime)
    {
        m_ai.enabled = false;
        Debug.Log("Player is interacting with " + name);
        yield return new WaitForSeconds(interactionTime);
        m_ai.enabled = true;
    }
}
