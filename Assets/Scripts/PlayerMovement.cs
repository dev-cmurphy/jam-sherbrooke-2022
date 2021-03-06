using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_slowSpeed;

    [SerializeField] private GameObject m_mesh;

    private bool m_isSlowed;

    private Vector3 m_inputDirection;
    private Rigidbody m_rb;

    private static PlayerMovement m_instance;

    private PlayerInteraction m_playerInteraction;

    public void SlowDown()
    {
        m_isSlowed = true;
    }

    private void Awake()
    {
        m_instance = this;
        m_rb = GetComponent<Rigidbody>();
        m_playerInteraction = GetComponent<PlayerInteraction>();
    }

    public void RemoveSlow()
    {
        m_isSlowed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inputDirection.sqrMagnitude > 0)
        {
            float rotation = Mathf.Atan2(m_inputDirection.x, m_inputDirection.y);
            m_mesh.transform.rotation = Quaternion.Lerp(m_mesh.transform.rotation, Quaternion.Euler(0, 180 + (rotation * Mathf.Rad2Deg), 0), Time.deltaTime * 5f);
        }

        if (m_playerInteraction.LockedInInteraction)
        {
            m_inputDirection = Vector3.zero;
            return;
        }

        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir += -Vector2.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            dir += -Vector2.up;
        }

        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
        }

        dir.Normalize();
        m_inputDirection = dir;
    }

    private void FixedUpdate()
    {
        Vector3 delta = m_inputDirection * (m_isSlowed ? m_slowSpeed : m_speed);
        delta.z = delta.y;
        delta.y = 0;
        delta *= Time.fixedDeltaTime;
        m_rb.MovePosition(transform.position + delta);
    }

    static public Vector3 PlayerPosition
    {
        get
        {
            return m_instance.transform.position;
        }
    }
}
