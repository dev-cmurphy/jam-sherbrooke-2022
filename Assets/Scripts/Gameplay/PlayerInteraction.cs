using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerInteraction : MonoBehaviour
{
    private FriendlyNPC m_currentlyTargetedNPC;

    [SerializeField] private UnityEvent m_onInteractionAvailable;
    [SerializeField] private UnityEvent m_onInteractionUnavailable;

    public bool LockedInInteraction;

    private void Awake()
    {
        LockedInInteraction = false;
    }

    private void Update()
    {
        if(m_currentlyTargetedNPC != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (m_currentlyTargetedNPC.Interact(this))
                {
                    // sound bon ?
                }
                else
                {
                    // sound bad ?
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var npc = other.GetComponent<FriendlyNPC>();
        if(npc != null && WorldStateController.WorldState == WorldStateController.WORLDSTATE.STATE_SANE)
        {
            m_currentlyTargetedNPC = npc;
            m_onInteractionAvailable?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var npc = other.GetComponent<FriendlyNPC>();
        if (npc == m_currentlyTargetedNPC)
        {
            m_currentlyTargetedNPC = null;
            m_onInteractionUnavailable?.Invoke();
        }
    }
}
