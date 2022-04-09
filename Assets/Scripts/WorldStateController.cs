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

    private void Start()
    {
        StartCoroutine(StateSwitchCoroutine());
    }

    private IEnumerator StateSwitchCoroutine()
    {
        while(gameObject.activeSelf)
        {
            if(WorldState == WORLDSTATE.STATE_MAD)
            {
                WorldState = WORLDSTATE.STATE_SANE;
                OnSwitchToSane?.Invoke();
            }
            else if (WorldState == WORLDSTATE.STATE_SANE)
            {
                WorldState = WORLDSTATE.STATE_MAD;
                OnSwitchToMad?.Invoke();
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
