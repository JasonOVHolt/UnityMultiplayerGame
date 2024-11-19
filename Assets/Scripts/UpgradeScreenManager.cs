using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField] GameObject minionTab, baseTab, playerTab;



    [SerializeField] BaseManager baseManager;
    [SerializeField] TMP_Text minionAttackTierText, minionAttackSpawnRateText, minionAttackAttackSpeedText, minionAttackHealthText, minionHarvestTierText, minionHarvestHarvestSpeedText, minionHarvestMovementSpeedText;
    [SerializeField] TMP_Text minionAttackWoodUpgradeText, minionAttackStoneUpgradeText, minionHarvestWoodUpgradeText, minionHarvestStoneUpgradeText;
    [SerializeField] TMP_Text playerHarvestWoodUpgradeText, playerHarvestStoneUpgradeText, playerWeaponWoodUpgradeText, playerWeaponStoneUpgradeText, playerArmorWoodUpgradeText, playerArmorStoneUpgradeText;
    [SerializeField] TMP_Text baseWoodUpgradeText, baseStoneUpgradeText;
    [SerializeField] TMP_Text averageMinionTierText, averagePlayerTierText;

    [SerializeField] TMP_Text playerHarvestSpeedText, playerHarvestAmountText, playerWeaponTierText, playerArmorTierText, playerArmorMaxText;

    [Header("Attack Minion Upgrade Costs")]
    [SerializeField] Vector2 upgradeAttackMinion1, upgradeAttackMinion2, upgradeAttackMinion3, upgradeAttackMinion4, upgradeAttackMinion5, upgradeAttackMinion6, upgradeAttackMinion7, upgradeAttackMinion8, upgradeAttackMinion9, upgradeAttackMinion10;

    [Header("Harvest Minion Upgrade Costs")]
    [SerializeField] Vector2 upgradeHarvestMinion1, upgradeHarvestMinion2, upgradeHarvestMinion3, upgradeHarvestMinion4, upgradeHarvestMinion5, upgradeHarvestMinion6, upgradeHarvestMinion7, upgradeHarvestMinion8, upgradeHarvestMinion9, upgradeHarvestMinion10;

    [Header("Base Upgrade Costs")]
    [SerializeField] Vector2 upgradeBase1, upgradeBase2;

    [Header("Harvest Tools Upgrade Costs")]
    [SerializeField] Vector2 upgradeHarvestTool1, upgradeHarvestTool2, upgradeHarvestTool3, upgradeHarvestTool4, upgradeHarvestTool5;

    [Header("Weapon Upgrade Costs")]
    [SerializeField] Vector2 upgradeWeapon1, upgradeWeapon2, upgradeWeapon3, upgradeWeapon4, upgradeWeapon5;

    [Header("Armor Upgrade Costs")]
    [SerializeField] Vector2 upgradeArmor1, upgradeArmor2, upgradeArmor3, upgradeArmor4, upgradeArmor5, upgradeArmor6, upgradeArmor7, upgradeArmor8, upgradeArmor9, upgradeArmor10;


    public void UpgradeAttackMinion()
    {
        if (baseManager.attackerLVL == 1 && (baseManager.wood == upgradeAttackMinion1.x && baseManager.stone == upgradeAttackMinion1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase Stats and level & subtract resources

            baseManager.wood -= ((int)upgradeAttackMinion1.x);
            baseManager.stone -= ((int)upgradeAttackMinion1.y);


        }
        else if (baseManager.attackerLVL == 2 && (baseManager.wood == upgradeAttackMinion2.x && baseManager.stone == upgradeAttackMinion2.y) && baseManager.currentBaseTier > 0)
        {
            baseManager.wood -= ((int)upgradeAttackMinion2.x);
            baseManager.stone -= ((int)upgradeAttackMinion2.y);
        }
    }

    public void UpgradeHarvestMinion()
    {
        if(baseManager.harvestorLVL == 1 && (baseManager.wood == upgradeHarvestMinion1.x && baseManager.stone == upgradeHarvestMinion1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
            baseManager.wood -= ((int)upgradeHarvestMinion1.x);
            baseManager.stone -= ((int)upgradeHarvestMinion1.y);

        }
    }

    public void UpgradeBase()
    {
        if((Mathf.Sqrt((baseManager.attackerLVL * baseManager.attackerLVL)+(baseManager.harvestorLVL* baseManager.harvestorLVL)) >= upgradeBase1.x) &&(Mathf.Sqrt((baseManager.playerArmorLVL* baseManager.playerArmorLVL) +(baseManager.playerHarvestLVL* baseManager.playerHarvestLVL) +(baseManager.playerWeaponLVL* baseManager.playerWeaponLVL)))> upgradeBase1.y)
        {
            baseManager.UpgradeBase();
        }
    }

    public void UpgradeHarvestTools()
    {
        if (baseManager.playerHarvestLVL == 1 && (baseManager.wood == upgradeHarvestTool1.x && baseManager.stone == upgradeHarvestTool1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
            baseManager.wood -= ((int)upgradeHarvestTool1.x);
            baseManager.stone -= ((int)upgradeHarvestTool1.y);
        }
    }

    public void UpgradeWeapon()
    {
        if (baseManager.playerWeaponLVL == 1 && (baseManager.wood == upgradeWeapon1.x && baseManager.stone == upgradeWeapon1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
            baseManager.wood -= ((int)upgradeWeapon1.x);
            baseManager.stone -= ((int)upgradeWeapon1.y);
        }
    }

    public void UpgradeArmor()
    {
        if (baseManager.playerArmorLVL == 1 && (baseManager.wood == upgradeArmor1.x && baseManager.stone == upgradeArmor1.y) && baseManager.currentBaseTier > 0)
        {
            //Increase stats and level & subtract resources
            baseManager.wood -= ((int)upgradeArmor1.x);
            baseManager.stone -= ((int)upgradeArmor1.y);

            //Increase Player Armor
            baseManager.playerController.armor = 10;

            baseManager.playerArmorLVL++;

            //Update Screen
        }
    }



}
