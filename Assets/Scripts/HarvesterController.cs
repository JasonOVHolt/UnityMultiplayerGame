using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HarvesterController : MonoBehaviour
{
    public bool isWood, canAction;
    Transform currentTarget;
    [SerializeField] Transform harvestTarget, baseTarget;
    [SerializeField] float reachDistance;
    float timer;
    [SerializeField] float actionCooldown, step;

    private void Start()
    {
        timer = actionCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((this.gameObject.transform.position - currentTarget.position).magnitude) < reachDistance && canAction)
        {
            timer = actionCooldown;
            canAction = false;
            if (currentTarget == harvestTarget)
                currentTarget = baseTarget;
            else
            {
                currentTarget = harvestTarget;
                /////////////////////////////////Add resource to base
            }
                
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canAction = true;
        }

        Move();

    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, step / 100);
    }


}
