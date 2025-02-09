using DG.Tweening.Core.Easing;
using Excellcube.EasyTutorial.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialAction : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    public void Start()
    {
        gameManager.SlotManager.onSlotDragEnd += () => DragTowerSlot();

        towerSpawn.spawnButton.onClick.RemoveAllListeners();
        towerSpawn.spawnButton.onClick.AddListener(() => TowerSpawn());
    }

    public void InitialGoldGemSetting()
    {

    }

    [SerializeField]
    private TowerSpawn towerSpawn;
    public int towerSpawnCount = 6;
    private int currentTowerSpawnCount = 0;
    public void TowerSpawn()
    {
        towerSpawn.OnClickSpawn(eTower.Tower_Name_BreadBoxer);
    }

    public void PressTowerSpawn()
    {
        currentTowerSpawnCount++;
        if (currentTowerSpawnCount == towerSpawnCount)
        {
            TutorialEvent.Instance.Broadcast("Tutorial01_SpawnTower");
        }
    }

    public void DragTowerSlot()
    {
        TutorialEvent.Instance.Broadcast("Tutorial02_DragTower");
    }

    public void SelectCanFusionSlot()
    {
        gameManager.SlotManager.FindSlot((slot) => slot.TowerGroup.CanFusion).onClicked.Invoke();
    }

    public void FusionTower()
    {
        TutorialEvent.Instance.Broadcast("Tutorial03_FusionTower");
    }

    public void ClickTowerUpgrade()
    {
        TutorialEvent.Instance.Broadcast("Tutorial04_UpgradeTower-0");
    }

    public int towerUpgradeCount = 6;
    private int currentTowerUpgradeCount = 0;
    public void UpgradeTower()
    {
        currentTowerUpgradeCount++;
        if (currentTowerUpgradeCount == towerUpgradeCount)
        {
            TutorialEvent.Instance.Broadcast("Tutorial04_UpgradeTower");
        }
    }

    public void MaxFusionIngredientsSetting()
    {
        towerSpawn.OnClickSpawn(eTower.Tower_Name_AugerWarrior);
        towerSpawn.OnClickSpawn(eTower.Tower_Name_PlagueDoctor);
        towerSpawn.OnClickSpawn(eTower.Tower_Name_BreadBoxer);
    }


    [SerializeField]
    private MaxFusionWindow maxFusionWindow;
    public void MaxFusionTargetSetting()
    {
        var targetTracker = gameManager.TowerManager.MaxFusionSystem.ProgressTrackers.Find(tracker => tracker.Data.Id == eTower.Tower_Name_DeadBear);
        maxFusionWindow.OnSelectRecipe(targetTracker);
    }

    public void ClickMaxFusion()
    {
        TutorialEvent.Instance.Broadcast("Tutorial05_MaxFusion-0");
    }

    public void MaxFusion()
    {
        TutorialEvent.Instance.Broadcast("Tutorial05_MaxFusion");
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Lobby");
    }
}
