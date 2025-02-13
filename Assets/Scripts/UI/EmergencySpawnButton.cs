
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EmergencySpawnButton : UIElement
{
    //private EmergencySpawn emergencySpawnWindow;

    [SerializeField]
    private TextMeshProUGUI gemCostText;

    private Button spwanButton;

    public int spawnGrade;

    [SerializeField]
    private int gemCost;


    private void Awake()
    {
        spwanButton= GetComponent<Button>();
        gemCostText.text = gemCost.ToString();
        spwanButton.onClick.AddListener(() => OnClickSpawnGrade(spawnGrade, gemCost));
    }

    //public void Initialize(EmergencySpawn window)
    //{
    //    emergencySpawnWindow= window;
    //}

    public void OnClickSpawnGrade(int grade, int gemCost)
    {
        if (UIManager.GameManager.TowerManager.IsMaxTowrCount || !UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        if (!UIManager.GameManager.goldGemSystem.TryPayGem(gemCost))
        {
            return;
        }

        GameObject tower = UIManager.GameManager.TowerManager.GetRandomTower(grade);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        SoundManager.Instance.PlaySFX("BattleEffect_01_Call");
    }
}
