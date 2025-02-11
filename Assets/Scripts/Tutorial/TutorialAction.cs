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
        towerSpawn.spawnButton.onClick.RemoveAllListeners();
        towerSpawn.spawnButton.onClick.AddListener(() => PressTowerSpawn());

        gameManager.SlotManager.onSlotDragEnd += () => DragTowerSlot();

        slotInteraction.fusionButton.onClick.AddListener(() => FusionTower());

        upgradeButton.onClick.AddListener(() => ClickTowerUpgrade());

        attackUpgrade.onClick.AddListener(() => UpgradeTower());

        maxFusionWindowButton.onClick.AddListener(() => ClickMaxFusion());

        maxFusionWindow.fusionButton.onClick.AddListener(() => MaxFusion());
    }

    public void InitialGoldGemSetting()
    {

    }

    [SerializeField]
    private TowerSpawn towerSpawn;
    public int towerSpawnCount = 6;
    private int currentTowerSpawnCount = 0;


    public void PressTowerSpawn()
    {
        towerSpawn.OnClickSpawn(eTower.Tower_Name_BreadBoxer);

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

    [SerializeField]
    private SlotInteraction slotInteraction;

    public void FusionTower()
    {
        TutorialEvent.Instance.Broadcast("Tutorial03_FusionTower");
    }

    [SerializeField]
    private Button upgradeButton;

    public void ClickTowerUpgrade()
    {
        TutorialEvent.Instance.Broadcast("Tutorial04_UpgradeTower-0");
    }

    public int towerUpgradeCount = 6;
    private int currentTowerUpgradeCount = 0;
    [SerializeField]
    private Button attackUpgrade;
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

    [SerializeField]
    private Button maxFusionWindowButton;


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
