using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public int currentBaseTier = 1;
    [SerializeField] List<GameObject> baseTiers = new List<GameObject>();
    [SerializeField] UpgradeScreenManager upgradeScreenMan;
    public int attackerLVL, harvestorLVL, playerHarvestLVL, playerWeaponLVL, playerArmorLVL;

    public int stone, wood;

    [SerializeField] GameObject harvestMinion, harvestMinionWoodSpawnPoint, harvestMinionStoneSpawnPoint;

    public float attackerSR, attackerAS, attackerH;
    public float harvestorHS, harvestorMS;

    public characterController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        baseTiers[0].SetActive(true);
        baseTiers[1].SetActive(false);
        baseTiers[2].SetActive(false);


        Instantiate(harvestMinion, harvestMinionWoodSpawnPoint.transform.position, Quaternion.identity);
        Instantiate(harvestMinion, harvestMinionStoneSpawnPoint.transform.position, Quaternion.identity);
    }


    public void UpgradeBase()
    {
        if (currentBaseTier == 1)
        {
            baseTiers[0].SetActive(false);
            baseTiers[1].SetActive(true);
            baseTiers[2].SetActive(false);
        }
        else if (currentBaseTier == 2)
        {
            baseTiers[0].SetActive(false);
            baseTiers[1].SetActive(false);
            baseTiers[2].SetActive(true);
        }
        currentBaseTier++;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
