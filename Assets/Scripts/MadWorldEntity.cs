using UnityEngine;
using System.Collections;

public class MadWorldEntity : DualEntity
{
    [SerializeField] protected GameObject m_madObject;

    private void Awake()
    {
    }

    protected override void OnSwitchToSane()
    {
        m_madObject.SetActive(false);
    }

    protected override void OnSwitchToMad()
    {
        m_madObject.SetActive(true);
    }
}
