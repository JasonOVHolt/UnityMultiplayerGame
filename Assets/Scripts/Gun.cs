using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    //[SerializeField]
    //private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    //[SerializeField]
    //private ParticleSystem ImpactParticleSystem;
    //[SerializeField]
    //private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    public GameObject minion;
    //private Animator Animator;

    private float LastShootTime;

    private void Awake()
    {
        //Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        OnShoot();
    }


    private void OnShoot()
    {
    if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            
        }
    }
    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
           // Animator.SetBool("IsShooting", true);
            //ShootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                //TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                //StartCoroutine(SpawnTrail(trail, hit));

                LastShootTime = Time.time;

                Object.Destroy(minion);

                Debug.Log("it works");
            }
        }
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
                );
            direction.Normalize();
        }
        return direction;
    }

   /* private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Animator.SetBool("IsShooting", false);
        Trail.transform.position = Hit.point;
        Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    } */
}
