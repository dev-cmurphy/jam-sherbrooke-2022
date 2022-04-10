using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Hallucination : MonoBehaviour
{
    [SerializeField] private EnemySettings m_settings;
    private NavMeshAgent m_ai;
    private EnemyState m_state;

    static HashSet<Hallucination> cluster = new HashSet<Hallucination>();

    public FMODParameterSet ParamSon;

    private void Awake()
    {
        m_ai = GetComponent<NavMeshAgent>();
        m_state = new EnemyRoamState(this, m_ai, m_settings);
    }

    private void FixedUpdate()
    {
        if (transform.position.FlatDistance(PlayerMovement.PlayerPosition) < 8 && !cluster.Contains(this))
        {
            cluster.Add(this);
        }

        if(cluster.Count > 0)
        {

            ParamSon.SetVolume(0.5f);
            ParamSon.SetGlobalParameter(1);
        }
        else
        {
            ParamSon.SetVolume(1f);
            ParamSon.SetGlobalParameter(0);
        }

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

