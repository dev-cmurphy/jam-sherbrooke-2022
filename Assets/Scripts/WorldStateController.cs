using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class WorldStateController : MonoBehaviour
{
    public enum WORLDSTATE
    {
        STATE_SANE,
        STATE_MAD
    }

    public static WORLDSTATE WorldState { get; private set; }
    

    public static UnityEvent OnSwitchToSane = new UnityEvent();
    public static UnityEvent OnSwitchToMad = new UnityEvent();

    [SerializeField] private UnityEvent OnInstanceSwitchToSane;
    [SerializeField] private UnityEvent OnInstanceSwitchToMad;
    [SerializeField] private UnityEvent On2fSwitchToSane;
    [SerializeField] private UnityEvent On2fSwitchToMad;


    [Min(0)]
    [SerializeField] private float m_saneTime = 10f;
    [Min(0)]
    [SerializeField] private float m_madTime = 30f;

    private void Start()
    {
        StartCoroutine(StateSwitchCoroutine());
    }

    private IEnumerator StateSwitchCoroutine()
    {
        while(gameObject.activeSelf)
        {
            if (WorldState == WORLDSTATE.STATE_MAD)
            {
                On2fSwitchToSane?.Invoke();
            }
            else if (WorldState == WORLDSTATE.STATE_SANE)
            {
                On2fSwitchToMad?.Invoke();
            }

            float waitTime = 0f;
            yield return new WaitForSeconds(3f);
            if(WorldState == WORLDSTATE.STATE_MAD)
            {
                WorldState = WORLDSTATE.STATE_SANE;
                OnSwitchToSane?.Invoke();
                OnInstanceSwitchToSane?.Invoke();
                waitTime = m_saneTime;
                Debug.Log("Switched to sane.");
            }
            else if (WorldState == WORLDSTATE.STATE_SANE)
            {
                WorldState = WORLDSTATE.STATE_MAD;
                OnSwitchToMad?.Invoke();
                OnInstanceSwitchToMad?.Invoke();
                waitTime = m_madTime;
                Debug.Log("Switched to mad.");
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
