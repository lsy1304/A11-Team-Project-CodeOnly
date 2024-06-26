using System;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : Singleton<Inventory>
{
    protected override void Awake()
    {
        base.Awake();
        nowArmorEquiped = new Dictionary<ArmorType, ItemSO> { };
        nowArmorEquiped.Add(ArmorType.Helmet, null);
        nowArmorEquiped.Add(ArmorType.Boots, null);
        nowArmorEquiped.Add(ArmorType.Chest, null);
    }

    public ItemSO nowWeaponEquiped;
    public Dictionary<ArmorType, ItemSO> nowArmorEquiped;
    public event Action onEquipChange;
    public bool isEquip(ItemSO itemSO)
    {
        if (nowWeaponEquiped == itemSO || nowArmorEquiped[itemSO.armorType] == itemSO)
        {
            return true;
        }
        return false;
    }
    
    public void Equip(ItemSO itemSO)
    {
        switch (itemSO.itemType)
        {
            case ItemType.Weapon:
                if (nowWeaponEquiped != null)
                {
                    UnWeaponEquip();
                }
                nowWeaponEquiped = itemSO;
                break;
            case ItemType.Armor:
                if (nowArmorEquiped[itemSO.armorType] != null)
                {
                    UnArmorEquiped(itemSO.armorType);
                }
                nowArmorEquiped[itemSO.armorType] = itemSO;
                break;
        }
        onEquipChange?.Invoke();
    }
    public void UnArmorEquiped(ArmorType armorType)
    {
        nowArmorEquiped[armorType] = null;
        onEquipChange?.Invoke();
    }

    public void UnWeaponEquip()
    {
        nowWeaponEquiped = null;
        onEquipChange?.Invoke();
    }

    [SerializeField] List<InventoryWeaponSlot> weaponSlot = new List<InventoryWeaponSlot>();
    [SerializeField] List<InventoryItemSlot> itemSlot;
    [SerializeField] GameObject inventoryWeaponSlot;
    [SerializeField] Transform weaponContent;
    [SerializeField] GameObject inventoryCanvas;

    [HideInInspector] public InvenPlayerStat invenPlayerStat;
    [HideInInspector] public  PlayerGold playerGold;
    [HideInInspector] public Player player;
    PlayerStateMachine playerStateMachine;
    public List<ItemSO> itemSOs;
    private void Start()
    {
        foreach (var item in itemSOs)
        {
            AddItem(item);
        }
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (playerStateMachine == null)
        {
            playerStateMachine = player.GetPlayerStateMachine();
        }

        playerGold = GetComponent<PlayerGold>();
        playerGold.Gold += 10000;
        invenPlayerStat = GetComponent<InvenPlayerStat>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            if (Cursor.lockState == CursorLockMode.None)
            {
                playerStateMachine.Player.VirtualCamera.enabled = true;
                playerStateMachine.IsInteracted = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                playerStateMachine.Player.VirtualCamera.enabled = false;
                playerStateMachine.IsInteracted = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void AddItem(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                AddInventory(item);
                break;
            case ItemType.Armor:
                AddInventory(item);
                break;
            case ItemType.Portion:
                SetItemSlot(item);
                break;
        }
    }
    void AddInventory(ItemSO item)
    {
        InventoryWeaponSlot w_Slot = Instantiate(inventoryWeaponSlot, weaponContent).GetComponent<InventoryWeaponSlot>();
        weaponSlot.Add(w_Slot);
        w_Slot.SetSlot(item);
    }
    void SetItemSlot(ItemSO itemSO)
    {
        for (int i = 0;  i< itemSlot.Count; i++)
        {
            if (itemSlot[i].itemSO == itemSO)
            {
                if (itemSlot[i].stack < itemSO.maxStack)
                {
                    itemSlot[i].SetSlot(itemSO);
                    return;
                }
            }

            if (itemSlot[i].itemSO == null)
            {
                itemSlot[i].SetSlot(itemSO);
                return;
            }
        }
    }
}
