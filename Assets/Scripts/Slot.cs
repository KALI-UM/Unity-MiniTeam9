using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private SpriteRenderer circleArea;

    [SerializeField]
    private SpriteRenderer effectRenderer;

    protected SlotManager slotManager;
    public SlotManager SlotManager
    {
        get => slotManager;
    }

    private List<Tower> towers = new();

    public int SlotIndex
    {
        get;
        private set;
    }

    public Action onClicked;

    public TowerGroup TowerGroup
    {
        get;
        private set;
    }

    //private Sequence slotAnimation;

    //private void Awake()
    //{
    //    slotAnimation= DOTween.Sequence().SetAutoKill(false);
    //    slotAnimation.OnStart(() => slotAnimation.Pause());        
    //}
    //private void Start()
    //{
    //    slotAnimation.OnPlay(()=>effectRenderer.gameObject.SetActive(true));
    //    slotAnimation.Append(effectRenderer.DOFade(0, 2f).SetEase(Ease.InSine));
    //    slotAnimation.OnComplete(()=>effectRenderer.gameObject.SetActive(false));
    //}

    public void Initialize(SlotManager manager, TowerGroup group, int index)
    {
        slotManager = manager;
        this.TowerGroup = group;
        this.TowerGroup.towerManager = SlotManager.GameManager.TowerManager;
        this.TowerGroup.enemyManager = SlotManager.GameManager.EnemyManager;
        group.transform.position = transform.position;

        SlotIndex = index;
    }

    public bool IsNotEmptyAndPossibleToAdd(eTower towerId)
    {
        if (TowerGroup.IsEmpty)
        {
            return false;
        }

        return (!TowerGroup.IsFull) && (TowerGroup.TowerId == towerId);
    }

    public void AddTower(Tower tower)
    {
        TowerGroup.AddTower(tower);
        //slotAnimation.Restart();
    }

    public void FusionTower()
    {
        GameObject newTower = SlotManager.GameManager.TowerManager.GetRandomTower(TowerGroup.Data.grade + 1);
        RemoveAllTower();
        SlotManager.AddTower(newTower.GetComponent<Tower>(), SlotIndex);
    }

    public void RemoveTower()
    {
        TowerGroup.RemoveTower();
        slotManager.UpdateTowerSort();
    }

    public void RemoveAllTower()
    {
        while (!TowerGroup.IsEmpty)
        {
            TowerGroup.RemoveTower();
        }
    }

    public void ChangeTowerGroup(TowerGroup towerGroup)
    {
        TowerGroup = towerGroup;
        TowerGroup.MoveTo(transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClicked?.Invoke();
    }

    public void OnSelected()
    {
        circleArea.gameObject.SetActive(true);
        var newScale = Vector3.one;
        newScale *= 2 * TowerGroup.Tower.AttackRange;
        circleArea.transform.localScale = newScale;
    }

    public void OnDeselected()
    {
        circleArea.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SlotManager.GameManager.UIManager.Close(FocusWindows.SlotInteraction);
        SlotManager.SelectedSlot.OnDeselected();

        if (!TowerGroup.IsEmpty)
        {
            slotManager.OnBeginDragSlot(transform.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!TowerGroup.IsEmpty)
        {
            var slot = eventData.pointerEnter.GetComponent<Slot>();
            if (slot != null)
            {
                slotManager.OnDragSlot(eventData.pointerEnter.transform.position);
            }
            else
            {
                eventData.dragging = false;
                slotManager.OnEndDragSlot();
            }
        }
    }

    //�巡�� ������ ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        var slot = eventData.pointerEnter.GetComponent<Slot>();
        if (slot != null && slot != this)
        {
            TowerGroup temp = TowerGroup;

            ChangeTowerGroup(slot.TowerGroup);
            slot.ChangeTowerGroup(temp);
        }
        
        slotManager.OnEndDragSlot();
    }

}
