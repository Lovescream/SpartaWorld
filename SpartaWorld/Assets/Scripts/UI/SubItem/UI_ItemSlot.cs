using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemSlot : UI_Base {

    #region Enums

    enum Images {
        imgItem,
    }
    enum Objects {
        Equip,
    }

    #endregion

    #region Properties

    public Inventory Inventory { get; private set; }
    public Item Item { get; private set; }

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }
    void OnDestroy() {
        if (Inventory is PlayerInventory playerInventory)
            playerInventory.OnEquipChanged -= RefreshEquip;
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindImage(typeof(Images));
        BindObject(typeof(Objects));

        GetImage((int)Images.imgItem).gameObject.BindEvent(OnClickItemSlot);

        return true;
    }

    public void SetInfo(Inventory inventory, Item item) {
        if (Inventory is PlayerInventory pI && Item != null) {
            pI.OnEquipChanged -= RefreshEquip;
        }
        this.Inventory = inventory;
        this.Item = item;
        GetImage((int)Images.imgItem).sprite = Main.Resource.Load<Sprite>($"{item.Key}.sprite");

        if (Inventory is PlayerInventory playerInventory) {
            playerInventory.OnEquipChanged += RefreshEquip;
            RefreshEquip(item);
        }
    }

    public void RefreshEquip(Item item) {
        if (Inventory == null || item == null) return;
        if (Inventory is not PlayerInventory playerInventory) return;
        if (Item == item)
            GetObject((int)Objects.Equip).SetActive(playerInventory.IsEquip(item));
    }
    
    private void OnClickItemSlot() {
        if (Inventory is PlayerInventory) {
            UI_Popup_EquipConfirm popup = Main.UI.ShowPopupUI<UI_Popup_EquipConfirm>();
            popup.SetInfo(Inventory as PlayerInventory, Item);
        }
    }
}