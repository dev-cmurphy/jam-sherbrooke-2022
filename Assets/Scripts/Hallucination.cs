using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Hallucination : MonoBehaviour
{
    [SerializeField] private EnemySettings m_settings;
    private NavMeshAgent m_ai;
    private EnemyState m_state;

    private void Awake()
    {
        m_ai = GetComponent<NavMeshAgent>();
        m_state = new EnemyRoamState(this, m_ai, m_settings);
    }

    private void FixedUpdate()
    {
        m_state = m_state.Update(PlayerMovement.PlayerPosition, Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponent<PlayerPointsAndLife>();

        if(p != null)
        {
            p.Damage(Random.Range(4, 9));
        }
    }
}

