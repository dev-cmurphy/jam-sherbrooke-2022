using UnityEngine;
using System.Collections;

public class DualEntity : MonoBehaviour
{
    private void Start()
    {
        WorldStateController.OnSwitchToMad.AddListener(OnSwitchToMad);
        WorldStateController.OnSwitchToSane.AddListener(OnSwitchToSane);
    }

    // Update is called once per frame
    private void Update()
    {
        if (WorldStateController.WorldState.Equals(WorldStateController.WORLDSTATE.STATE_SANE))
        {
            SaneUpdate();
        }
        else
        {
            MadUpdate();
        }
    }

    protected virtual void SaneUpdate()
    {

    }

    protected virtual void MadUpdate()
    {

    }
    
    protected virtual void OnSwitchToMad()
    {
        Debug.Log(gameObject.name + " switched to mad");
    }

    protected virtual void OnSwitchToSane()
    {
        Debug.Log(gameObject.name + " switched to sane");
    }
}
