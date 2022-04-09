using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoamingController : MonoBehaviour
{
    private List<Transform> m_roamingPoints;
    
    private Dictionary<GameObject, Transform> m_pointForUsers;
    private HashSet<Transform> m_occupiedPoints;

    [SerializeField] private bool m_refresh;
    [SerializeField] private bool m_isEnemyInstance;

    private void Awake()
    {
        m_pointForUsers = new Dictionary<GameObject, Transform>();
        m_occupiedPoints = new HashSet<Transform>();
        if (m_isEnemyInstance)
        {
            EnemyInstance = this;
        }
        else
        {
            NPCInstance = this;
        }
    }

    private void Start()
    {
        UpdatePoints();
    }

    static public RoamingController NPCInstance { get; private set; }
    static public RoamingController EnemyInstance { get; private set; }

    public Vector3 AvailableRoamingPoint(GameObject owner)
    {
        if (m_pointForUsers.ContainsKey(owner))
        {
            m_occupiedPoints.Remove(m_pointForUsers[owner]);
            m_pointForUsers.Remove(owner);
        }

        Transform p = null;
        while(p == null)
        {
            int index = Random.Range(0, m_roamingPoints.Count);
            if (!m_occupiedPoints.Contains(m_roamingPoints[index]))
            {
                p = m_roamingPoints[index];
            }
        }

        m_pointForUsers[owner] = p;
        m_occupiedPoints.Add(p);
        return p.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (m_roamingPoints == null)
            return;

        Color c = m_isEnemyInstance ? Color.red : Color.cyan;
        Gizmos.color = c;
        foreach(var p in m_roamingPoints)
        {
            Gizmos.DrawSphere(p.position, 0.35f);
        }
    }
    
    private void OnValidate()
    {
        UpdatePoints();
    }
#endif

    private void UpdatePoints()
    {
        m_roamingPoints.Clear();
        foreach (Transform t in transform)
        {
            m_roamingPoints.Add(t);
        }
    }
}
