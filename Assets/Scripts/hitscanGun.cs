using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitscanGun : MonoBehaviour
{
    [SerializeField] private float hitscanRange = 100f;
    [SerializeField] private LayerMask hitscanLayers;
    [SerializeField] private bool useHitscan = true;
    [SerializeField] GameObject camPos;
    private Vector3 camOffset = new Vector3(0, 0.7f, 0);
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireHitscan();
        }
    }


    private void fireHitscan()
    {
        if(Physics.Raycast(camPos.transform.position+camOffset, camPos.transform.forward, out RaycastHit hit, hitscanRange))
        {
            
            if (hit.collider.gameObject.layer == Mathf.Log(hitscanLayers.value, 2))
            {
                Debug.Log(hit.collider.name);
                Destroy(hit.transform.gameObject);
            }
            
        }
    }
    private float desiredX;

}
