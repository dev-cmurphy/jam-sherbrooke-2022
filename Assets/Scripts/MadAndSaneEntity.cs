using UnityEngine;
using System.Collections;

public class MadAndSaneEntity : DualEntity
{
    [SerializeField] protected GameObject m_saneObject;
    [SerializeField] protected GameObject m_madObject;

    protected override void OnSwitchToSane()
    {
        m_saneObject.SetActive(true);
        m_madObject.SetActive(false);
    }

    protected override void OnSwitchToMad()
    {
        m_saneObject.SetActive(false);
        m_madObject.SetActive(true);
    }
}
