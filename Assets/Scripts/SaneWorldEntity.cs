using UnityEngine;
using System.Collections;

public class SaneWorldEntity : DualEntity
{
    [SerializeField] protected GameObject m_saneObject;

    private void Awake()
    {
    }

    protected override void OnSwitchToSane()
    {
        m_saneObject.SetActive(true);
    }

    protected override void OnSwitchToMad()
    {
        m_saneObject.SetActive(false);
    }
}
