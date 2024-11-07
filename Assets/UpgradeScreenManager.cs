using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField] BaseManager baseManager;
    [SerializeField] TMP_Text minionAttackTierText, minionAttackSpawnRateText, minionAttackAttackSpeedText, minionAttackHealthText, minionHarvestTierText, minionHarvestHarvestSpeedText, minionHarvestMovementSpeedText;
    [SerializeField] TMP_Text minionAttackWoodUpgradeText, minionAttackStoneUpgradeText, minionHarvestWoodUpgradeText, minionHarvestStoneUpgradeText;
    
    [SerializeField] TMP_Text averageMinionTierText, averagePlayerTierText;

    [SerializeField] TMP_Text playerHarvestSpeedText, playerHarvestAmountText, playerWeaponText, playerArmorText;

    [Header("Attack Minion Upgrade Costs")]
    [SerializeField] Vector2 upgradeAttackMinion1;

    [Header("Harvest Minion Upgrade Costs")]
    [SerializeField] Vector2 upgradeHarvestMinion1;

    [Header("Base Upgrade Costs")]
    [SerializeField] Vector2 upgradeBase1;

    [Header("Harvest Tools Upgrade Costs")]
    [SerializeField] Vector2 upgradeHarvestTool1;

    [Header("Weapon Upgrade Costs")]
    [SerializeField] Vector2 upgradeWeapon1;

    [Header("Armor Upgrade Costs")]
    [SerializeField] Vector2 upgradeArmor1;


    void UpgradeAttackMinion()
    {
        if (baseManager.attackerLVL == 1 && (baseManager.wood == upgradeAttackMinion1.x && baseManager.stone == upgradeAttackMinion1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase Stats and level & subtract resources
        }
    }

    void UpgradeHarvestMinion()
    {
        if(baseManager.harvestorLVL == 1 && (baseManager.wood == upgradeHarvestMinion1.x && baseManager.stone == upgradeHarvestMinion1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
        }
    }

    void UpgradeBase()
    {
        if((Mathf.Sqrt((baseManager.attackerLVL * baseManager.attackerLVL)+(baseManager.harvestorLVL* baseManager.harvestorLVL)) >= upgradeBase1.x) &&(Mathf.Sqrt((baseManager.playerArmorLVL* baseManager.playerArmorLVL) +(baseManager.playerHarvestLVL* baseManager.playerHarvestLVL) +(baseManager.playerWeaponLVL* baseManager.playerWeaponLVL)))> upgradeBase1.y)
        {
            baseManager.UpgradeBase();
        }
    }

    void UpgradeHarvestTools()
    {
        if (baseManager.playerHarvestLVL == 1 && (baseManager.wood == upgradeHarvestTool1.x && baseManager.stone == upgradeHarvestTool1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
        }
    }

    void UpgradeWeapon()
    {
        if (baseManager.playerWeaponLVL == 1 && (baseManager.wood == upgradeWeapon1.x && baseManager.stone == upgradeWeapon1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
        }
    }

    void UpgradeArmor()
    {
        if (baseManager.playerArmorLVL == 1 && (baseManager.wood == upgradeArmor1.x && baseManager.stone == upgradeArmor1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
        }
    }



}
