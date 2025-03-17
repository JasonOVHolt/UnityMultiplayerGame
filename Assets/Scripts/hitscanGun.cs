using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitscanGun : MonoBehaviour
{
    [SerializeField] private float hitscanRange = 100f;
    [SerializeField] private LayerMask hitscanLayers;
    [SerializeField] private bool useHitscan = true;
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireHitscan();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * hitscanRange);
    }

    private void fireHitscan()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, hitscanLayers))
        {
            Debug.Log(hit.collider.gameObject.name);
            Destroy(hit.transform.gameObject);
        }
    }
    private float desiredX;

}
