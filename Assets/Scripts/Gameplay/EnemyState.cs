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

    public abstract EnemyState Update(Vector3 playerPos);
}

public class EnemyRoamState : EnemyState
{
    public EnemyRoamState(Hallucination owner, NavMeshAgent ai, EnemySettings settings) : base(owner, ai, settings)
    {
    }

    public override EnemyState Update(Vector3 playerPos)
    {
        if (Time.frameCount % 4 != 0)
            return this;

        // TODO : owner + ownerSettings => scriptableobject pour le sight ?
        if (Physics.Raycast(m_owner.transform.position, (playerPos - m_owner.transform.position), out RaycastHit hit, m_settings.Sight))
        {
            if (hit.transform.tag == "Player")
            {
                return new EnemyChaseState(m_owner, m_ai, m_settings);
            }
        }
        return this;
    }
}

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Hallucination owner, NavMeshAgent ai, EnemySettings settings) : base(owner, ai, settings)
    {
    }

    public override EnemyState Update(Vector3 playerPos)
    {
        m_ai.destination = playerPos;
        if (Vector3.Distance(m_owner.transform.position, playerPos) < 7)
        {
            return new EnemyRoamState(m_owner, m_ai, m_settings);
        }
        return this;
    }
}

