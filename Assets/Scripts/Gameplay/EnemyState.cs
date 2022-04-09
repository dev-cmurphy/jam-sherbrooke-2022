using UnityEngine;
using System.Collections;
using UnityEngine.AI;

abstract public class EnemyState
{
    protected Hallucination m_owner;
    protected NavMeshAgent m_ai;
    protected EnemySettings m_settings;

    public EnemyState(Hallucination owner, NavMeshAgent ai, EnemySettings settings)
    {
        m_owner = owner;
        m_ai = ai;
        m_settings = settings;
    }

    protected bool SeesPlayer(Vector3 playerPos)
    {
        if (Physics.Raycast(m_owner.transform.position, (playerPos - m_owner.transform.position), out RaycastHit hit, m_settings.Sight))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public abstract EnemyState Update(Vector3 playerPos, float deltaTime);
}

public class EnemyRoamState : EnemyState
{
    private Vector3 m_currentRoamingDestination;

    public EnemyRoamState(Hallucination owner, NavMeshAgent ai, EnemySettings settings) : base(owner, ai, settings)
    {
        m_currentRoamingDestination = owner.transform.position;
    }

    public override EnemyState Update(Vector3 playerPos, float deltaTime)
    {
        if (Time.frameCount % 4 != 0)
            return this;
       
        if(m_owner.transform.position.FlatDistance(m_currentRoamingDestination) < 2f)
        {
            do
            {
                m_currentRoamingDestination = RoamingController.EnemyInstance.AvailableRoamingPoint(m_owner.gameObject);
            } while (!m_ai.SetDestination(m_currentRoamingDestination));

            Debug.Log("New destination for " + m_owner.name + " : " + m_ai.destination);
        }

        m_ai.SetDestination(m_currentRoamingDestination);

        if (SeesPlayer(playerPos))
        {
            Debug.Log(m_owner.name + " saw player!");
            return new EnemyChaseState(m_owner, m_ai, m_settings);
        }
        return this;
    }
}

public class EnemyChaseState : EnemyState
{
    private Vector3 m_lastPlayerPositionSeen;
    private float m_lostPlayerTimer = 0;

    public EnemyChaseState(Hallucination owner, NavMeshAgent ai, EnemySettings settings) : base(owner, ai, settings)
    {
        m_lostPlayerTimer = 0;
        m_lastPlayerPositionSeen = PlayerMovement.PlayerPosition;
    }

    public override EnemyState Update(Vector3 playerPos, float deltaTime)
    {
        if(SeesPlayer(playerPos))
        {
            m_lastPlayerPositionSeen = playerPos;
            m_lostPlayerTimer = 0;
        }
        else
        {
            m_lostPlayerTimer += deltaTime;
        }

        m_ai.destination = m_lastPlayerPositionSeen;
        
        if(m_lostPlayerTimer < m_settings.ChaseTime)
        {
            return this;
        }

        Debug.Log(m_owner.name + " lost player for a while. Returning to roam.");
        return new EnemyRoamState(m_owner, m_ai, m_settings);
    }
}

