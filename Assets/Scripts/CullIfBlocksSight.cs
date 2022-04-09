using UnityEngine;
using System.Collections;

public class CullIfBlocksSight : MonoBehaviour
{
    private Camera m_camera;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        /*if(Time.frameCount % 5 == 0)
        {
            Physics.Raycast(transform.position, (PlayerMovement.PlayerPosition - transform.position), out RaycastHit hit);
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Buildings"))
            {
                StartCoroutine(CullObjectTemp(hit.collider.gameObject, 1.0f));
            }
        }*/
    }
    
    private IEnumerator CullObjectTemp(GameObject render, float time)
    {
        render.SetActive(false);
        yield return new WaitForSeconds(time);
        render.SetActive(true);
    }
}
