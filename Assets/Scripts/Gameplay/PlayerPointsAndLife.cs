using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerPointsAndLife : MonoBehaviour
{
    [SerializeField] private int m_maxLife;

    private int m_currentLife;
    private int m_currentPoints;

    public float LifePercentage => m_currentLife / (float)m_maxLife;

    [SerializeField] private UnityEvent<int, int> m_onDamage;
    [SerializeField] private UnityEvent<int, int> m_onHeal;
    [SerializeField] private UnityEvent<int, int> m_onPoints;
    [SerializeField] private UnityEvent m_onDeath;

    private void Awake()
    {
        m_currentLife = m_maxLife;
        m_currentPoints = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Damage(3);
        }
    }

    public void Damage(int damage)
    {
        m_currentLife -= damage;
        m_currentLife = Mathf.Clamp(m_currentLife, 0, m_maxLife);
        m_onDamage?.Invoke(m_currentLife, damage);

        if(m_currentLife == 0)
        {
            m_onDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        m_currentLife += amount;
        m_currentLife = Mathf.Clamp(m_currentLife, 0, m_maxLife);
        m_onHeal?.Invoke(m_currentLife, amount);
    }

    public void GainPoints(int amount)
    {
        m_currentPoints += amount;
        m_onPoints?.Invoke(m_currentPoints, amount);
    }
}
