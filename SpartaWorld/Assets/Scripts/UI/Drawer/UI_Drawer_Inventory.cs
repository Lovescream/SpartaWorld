using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Drawer_Inventory : UI_Drawer {

    #region Enums

    enum Objects {
        Content,
    }
    enum Buttons {
        btnBack,
    }
    enum Texts {
        txtInventory,
    }

    #endregion

    #region Fields

    private PlayerInventory _playerInventory;

    private List<UI_ItemSlot> _slots = new();

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }
    void OnDestroy() {
        _playerInventory.OnChanged -= Refresh;
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        // TODO:: Find 없애기.
        _playerInventory = FindObjectOfType<MainScene>().Player.Inventory;
        GetObject((int)Objects.Content).DestroyChilds();

        _playerInventory.OnChanged += Refresh;
        Refresh();

        GetButton((int)Buttons.btnBack).gameObject.BindEvent(OnBtnBack);

        return true;
    }

    public void Refresh() {
        GetText((int)Texts.txtInventory).text = $"<color=red>{_playerInventory.Count}</color> / {_playerInventory.MaxCount}";
        for (int i = 0; i < _playerInventory.Count; i++) {
            UI_ItemSlot slot;
            if (i >= _slots.Count) {
                slot = Main.UI.CreateSubItem<UI_ItemSlot>(GetObject((int)Objects.Content).transform);
                _slots.Add(slot);
            }
            else slot = _slots[i];

            slot.SetInfo(_playerInventory, _playerInventory[i]);
        }
        if (_slots.Count > _playerInventory.Count) {
            for (int i = _slots.Count-1; i>=_playerInventory.Count;i--) {
                Main.Resource.Destroy(_slots[i].gameObject);
            }
        }
    }

    #region OnButtons

    private void OnBtnBack() {
        CloseDrawer();
        // TODO:: Find 지우기.
        (FindObjectOfType<MainScene>().UI as UI_MainScene).ShowButtons();
    }

    #endregion
}