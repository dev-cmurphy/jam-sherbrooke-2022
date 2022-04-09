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
    public EnemyRoamState(Hallucination owner, NavMeshAgent ai, EnemySettings settings) : base(owner, ai, settings)
    {
    }

    public override EnemyState Update(Vector3 playerPos, float deltaTime)
    {
        if (Time.frameCount % 4 != 0)
            return this;
        
        if (SeesPlayer(playerPos))
        {
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

        return new EnemyRoamState(m_owner, m_ai, m_settings);
    }
}

