using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class FriendlyNPC : DualEntity
{
    private NavMeshAgent m_ai;
    private Vector3 m_currentRoamingDestination;
    [SerializeField] private float m_playerAvoidanceRadius = 3f;

    public static bool IsPlayerInteracting
    {
        get; private set;
    }
    
    private void Awake()
    {
        m_ai = GetComponent<NavMeshAgent>();
        m_currentRoamingDestination = transform.position;
    }

    protected override void SaneUpdate()
    {
        if (!m_ai.enabled)
            return;

        m_ai.speed = 3.5f;

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

    bool needToFlee = false;

    protected override void MadUpdate()
    {
        if (!m_ai.enabled)
            return;
        
        if (transform.position.FlatDistance(PlayerMovement.PlayerPosition) < m_playerAvoidanceRadius)
        {
            if (!needToFlee)
            {
                Debug.Log("NEED TO FLEE!");
                // avoid player
                FindNewDestination(true);
                needToFlee = true;
                m_ai.speed = 11f;
            }
        }
        else
        {
            m_ai.speed = 5f;
            needToFlee = false;
        }

        if (transform.position.FlatDistance(m_currentRoamingDestination) < 0.5f)
        {
            FindNewDestination();
        }

        m_ai.SetDestination(m_currentRoamingDestination);
    }

    private void FindNewDestination(bool avoid = false)
    {
        do
        {
            if (avoid)
            {
                m_currentRoamingDestination = RoamingController.NPCInstance.AvailableRoamingPoint(gameObject, PlayerMovement.PlayerPosition);
            }
            else
            {
                m_currentRoamingDestination = RoamingController.NPCInstance.AvailableRoamingPoint(gameObject);
            }
        } while (!m_ai.SetDestination(m_currentRoamingDestination));

        Debug.Log("New destination for " + name + " : " + m_ai.destination);
    }

    public bool Interact(PlayerInteraction player)
    {
        if (WorldStateController.WorldState == WorldStateController.WORLDSTATE.STATE_MAD || IsPlayerInteracting)
            return false;
        StartCoroutine(InteractionCoroutine(player, 5f));
        return true;
    }

    private IEnumerator InteractionCoroutine(PlayerInteraction player, float interactionTime)
    {
        player.LockedInInteraction = true;
        IsPlayerInteracting = true;
        m_ai.enabled = false;
        Debug.Log("Player is interacting with " + name);
        float t = 0;
        for(; t < interactionTime && CanContinueInteration(); t += Time.deltaTime)
        {
            yield return null;
        }
        m_ai.enabled = true;
        IsPlayerInteracting = false;
        player.LockedInInteraction = false;
        if (t >= interactionTime)
        {
            CompleteInteraction();
        }
    }
    
    private void CompleteInteraction()
    {
        Debug.Log("Player completed interaction with " + name);
    }

    private bool CanContinueInteration()
    {
        return WorldStateController.WorldState == WorldStateController.WORLDSTATE.STATE_SANE;
    }
}
