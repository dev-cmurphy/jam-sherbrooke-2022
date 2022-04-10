using UnityEngine;
using System.Collections;
using TMPro;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private PlayerPointsAndLife m_data;
    [SerializeField] private TextMeshProUGUI m_textPoints;
    [SerializeField] private TextMeshProUGUI m_lifePoints;

    [SerializeField] private GameObject m_pointChangeAnimation;

    public void SetPoints(int points, int change)
    {
        m_textPoints.text = points.ToString();
        var g = Instantiate(m_pointChangeAnimation, m_textPoints.transform);
        g.transform.localPosition = Vector3.zero;
        g.GetComponent<TextMeshProUGUI>().text = change.ToString();
        Destroy(g, 3f);
    }

    public void SetLife(int life, int change)
    {
        m_lifePoints.text = life.ToString();
        var g = Instantiate(m_pointChangeAnimation, m_lifePoints.transform);
        g.transform.localPosition = Vector3.zero;
        g.GetComponent<TextMeshProUGUI>().text = change.ToString();
        Destroy(g, 3f);
    }
}
