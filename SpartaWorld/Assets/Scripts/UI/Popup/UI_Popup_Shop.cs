using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Shop : UI_Popup {

    #region Enums

    enum Objects {
        Content,
    }
    enum Buttons {
        btnClose,
    }
    enum Texts {
        txtGold,
    }

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }
    void OnDestroy() {
        MainScene scene = FindObjectOfType<MainScene>();
        if (scene == null) return;
        scene.Player.Inventory.OnGoldChanged -= SetGold;
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.btnClose).gameObject.BindEvent(OnBtnClose);

        GetObject((int)Objects.Content).DestroyChilds();

        // TODO:: Find 삭제.
        ShopInventory inventory = FindObjectOfType<MainScene>().ShopInventory;
        for (int i = 0; i < inventory.Count; i++) {
            Main.UI.CreateSubItem<UI_ShopSlot>(GetObject((int)Objects.Content).transform).SetInfo(inventory, inventory[i]);
        }
        FindObjectOfType<MainScene>().Player.Inventory.OnGoldChanged += SetGold;
        SetGold(FindObjectOfType<MainScene>().Player.Inventory.Gold);


        return true;
    }

    private void SetGold(float Gold) => GetText((int)Texts.txtGold).text = $"{Gold:F0}";

    #region OnButtons

    private void OnBtnClose() {
        Main.UI.ClosePopup(this);
    }

    #endregion

}