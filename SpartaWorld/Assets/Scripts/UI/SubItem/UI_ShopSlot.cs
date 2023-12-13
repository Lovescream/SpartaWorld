using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopSlot : UI_Base {

    #region Enums

    enum Texts {
        txtItemName,
        txtItemDescription,
        txtStat,
        txtGold,
    }
    enum Images {
        imgItem,
        imgStat,
    }
    enum Buttons {
        btnPurchase,
    }
    enum Objects {
        Stat,
    }

    #endregion

    #region Properties

    public Item Item { get; private set; }

    #endregion

    #region Fields

    private ShopInventory _shopInventory;
    private PlayerInventory _playerInventory;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }
    void OnDisable() {
        if (_playerInventory == null) return;
        _playerInventory.OnGoldChanged -= RefreshButton;
    }
    void OnDestroy() {
        if (_playerInventory == null) return;
        _playerInventory.OnGoldChanged -= RefreshButton;
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.btnPurchase).gameObject.BindEvent(OnBtnPurchase);

        return true;
    }

    public void SetInfo(ShopInventory inventory, Item item) {
        Initialize();
        this.Item = item;
        this._shopInventory = inventory;
        this._playerInventory = FindObjectOfType<MainScene>().Player.Inventory; // TODO:: Find 삭제.
        _playerInventory.OnGoldChanged += RefreshButton;

        GetImage((int)Images.imgItem).sprite = Main.Resource.Load<Sprite>($"{item.Key}.sprite");
        GetText((int)Texts.txtItemName).text = Item.Name;
        GetText((int)Texts.txtItemDescription).text = Item.Description;
        if (Item.Modifiers.Count > 0) {
            GetObject((int)Objects.Stat).SetActive(true);
            StatModifier modifier = Item.Modifiers[0];
            GetImage((int)Images.imgStat).sprite = Main.Resource.Load<Sprite>($"Icon_{modifier.Stat}.sprite");
            GetText((int)Texts.txtStat).text = $"{modifier.Value:+#;-#}";
        }
        else {
            GetObject((int)Objects.Stat).SetActive(false);
        }
        GetText((int)Texts.txtGold).text = $"{Item.Cost:F0}";

        RefreshButton(0);
    }

    private void RefreshButton(float gold) {
        GetButton((int)Buttons.btnPurchase).image.color = _playerInventory.Gold >= Item.Cost ?
            new Color(1.0f, 1.0f, 0.3f, 1.0f) :
            new Color(0.4f, 0.4f, 0.4f, 1.0f);
    }

    #region OnButtons

    private void OnBtnPurchase() {
        if (_playerInventory.Gold < Item.Cost) return;
        _shopInventory.Remove(Item);
        _playerInventory.Add(Item);
        _playerInventory.Gold -= Item.Cost;
        Main.UI.ShowToast("구입했습니다!");
        Main.Resource.Destroy(this.gameObject);
    }

    #endregion

}