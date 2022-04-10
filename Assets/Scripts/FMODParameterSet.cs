using UnityEngine;
using System.Collections;
using FMODUnity;

public class FMODParameterSet : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter m_emitter;

    [SerializeField] private string m_defaultParam, m_defaultGlobalParam;

    public void SetParameter(string param, int value)
    {
        m_emitter.SetParameter(param, value);
    }

    public void SetParameter(int value)
    {
        m_emitter.SetParameter(m_defaultParam, value);
    }

    public void SetParameter(int value, int _)
    {
        m_emitter.SetParameter(m_defaultParam, value);
    }

    public void SetGlobalParameter(int value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(m_defaultGlobalParam, value);
    }
}
