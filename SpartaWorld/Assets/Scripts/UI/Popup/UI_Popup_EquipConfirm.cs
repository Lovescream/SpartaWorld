using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_EquipConfirm : UI_Popup {

    #region Enums

    enum Objects {
        Stat,
    }
    enum Texts {
        txtItemName,
        txtItemDescription,
        txtStatType,
        txtStatValue,
        txtMessage,
    }
    enum Images {
        imgItem,
        imgStat,
    }
    enum Buttons {
        btnCancel,
        btnConfirm,
    }

    #endregion

    #region Properties

    public Item Item { get; private set; }

    #endregion

    #region Fields

    private PlayerInventory _playerInventory;
    private bool _isEquip;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindObject(typeof(Objects));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.btnCancel).gameObject.BindEvent(OnBtnCancel);
        GetButton((int)Buttons.btnConfirm).gameObject.BindEvent(OnBtnConfirm);

        return true;
    }

    public void SetInfo(PlayerInventory inventory, Item item) {
        Initialize();
        this._playerInventory = inventory;
        this.Item = item;

        GetImage((int)Images.imgItem).sprite = Main.Resource.Load<Sprite>($"{Item.Key}.sprite");
        GetText((int)Texts.txtItemName).text = Item.Name;
        GetText((int)Texts.txtItemDescription).text = Item.Description;
        if (Item.Modifiers.Count > 0) {
            GetObject((int)Objects.Stat).SetActive(true);
            StatModifier modifier = Item.Modifiers[0];
            GetImage((int)Images.imgStat).sprite = Main.Resource.Load<Sprite>($"Icon_{modifier.Stat}.sprite");
            GetText((int)Texts.txtStatType).text = $"{modifier.Stat}";
            GetText((int)Texts.txtStatValue).text = $"{modifier.Value:+#;-#}";
        }
        else {
            GetObject((int)Objects.Stat).SetActive(false);
        }

        _isEquip = _playerInventory.IsEquip(item);
        GetText((int)Texts.txtMessage).text = _isEquip ? "장착 해제 하시겠습니까?" : "장착 하시겠습니까?";
    }

    #region OnButtons

    private void OnBtnCancel() {
        Main.UI.ClosePopup(this);
    }
    private void OnBtnConfirm() {
        if (_isEquip) _playerInventory.UnEquip(Item.Type);
        else _playerInventory.Equip(Item);
        Main.UI.ClosePopup(this);
    }

    #endregion
}