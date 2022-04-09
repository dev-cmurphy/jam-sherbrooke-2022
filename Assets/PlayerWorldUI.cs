using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldUI : MonoBehaviour
{
    [SerializeField] private GameObject m_interactionUI;

    [SerializeField] private PlayerInteraction m_interaction;

    private void Awake()
    {
        m_interactionUI.SetActive(false);
    }

    public void ShowInteractionUI()
    {
        m_interactionUI.SetActive(true);
    }

    public void HideInteractionUI()
    {
        m_interactionUI.SetActive(false);
    }
}
