using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PlayerInteraction : MonoBehaviour
{
    private FriendlyNPC m_currentlyTargetedNPC;

    private void OnTriggerEnter(Collider other)
    {
        var npc = other.GetComponent<FriendlyNPC>();
        if(npc != null)
        {
            m_currentlyTargetedNPC = npc;
        }
    }

    private void Update()
    {
        if(m_currentlyTargetedNPC != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                m_currentlyTargetedNPC.Interact();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var npc = other.GetComponent<FriendlyNPC>();
        if (npc == m_currentlyTargetedNPC)
        {
            m_currentlyTargetedNPC = null;
        }
    }
}
