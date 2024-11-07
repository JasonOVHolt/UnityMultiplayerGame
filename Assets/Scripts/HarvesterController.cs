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
    private BaseManager baseManager;
    string side;
    GameObject myBase;

    private void Awake()
    {
        if(transform.position.x < 0)
        {
            isWood = true;
        }
        else
        {
            isWood = false;
        }

        if(transform.position.z < 0)
        {
            side = "Red";
            
        }
        else
        {
            side = "Blue";
        }

        if (isWood)
        {
            harvestTarget = GameObject.Find(side + "TreePoint").transform;
            baseTarget = GameObject.Find(side + "WoodPoint").transform;
        }
        else
        {
            harvestTarget = GameObject.Find(side + "RockPoint").transform;
            baseTarget = GameObject.Find(side + "StonePoint").transform;
        }
            

        baseManager = GameObject.Find(side + "Base").GetComponent<BaseManager>();

    }



    private void Start()
    {
        timer = actionCooldown;
        currentTarget = harvestTarget;
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
                if (isWood) 
                {
                    baseManager.wood++;
                }
                else
                {
                    baseManager.stone++;
                }

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

        transform.LookAt(currentTarget.transform);
    }


}
