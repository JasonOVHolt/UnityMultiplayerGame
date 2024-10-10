using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] string target;
    bool canAttack, canMove;
    float timer;
    [SerializeField] float attackCooldown;
    [SerializeField] public int health,damage;
    [SerializeField] float step;

     GameObject doorTarget, currentTarget;

    void Start()
    {
        canAttack = true;
        canMove = true;
        if (this.gameObject.transform.position.z > 0)
        {
            doorTarget = GameObject.Find("RedMinionSpawnPoint");
            target = "RedEnemy";
            this.gameObject.tag = "BlueEnemy";
        }
        else
        {
            doorTarget = GameObject.Find("BlueMinionSpawnPoint");
            target = "BlueEnemy";
            this.gameObject.tag = "RedEnemy";
        }


        currentTarget = doorTarget;
        transform.LookAt(currentTarget.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
        if (health <= 0)
            Destroy(this.gameObject);
        if (canMove)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, step/100);
    }

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Trigger Stay");
        if ((collider.gameObject.CompareTag(target) || collider.gameObject.CompareTag("Door")) && canAttack)
        {
            Debug.Log("ATTACK");
            Attack(collider.gameObject);
            canMove = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(target))
        {
            canMove = true;
            currentTarget = doorTarget;
        }
            
    }


    void Attack(GameObject enemy) {
        
        if(enemy.CompareTag(target))
            enemy.GetComponent<MinionController>().health -= damage;
        else
            enemy.GetComponent<DoorController>().health -= damage;

        canAttack = false;
    }
}