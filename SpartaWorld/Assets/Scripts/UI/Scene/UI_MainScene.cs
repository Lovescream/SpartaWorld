using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainScene : UI_Scene {

    #region Enums

    enum Texts {
        txtGold,
        txtName,
        txtClass,
        txtLevel,
        txtExp,
        txtDescription,
        txtStatus,
        txtInventory,
        txtShop,
    }
    enum Images {
        imgCharacter
    }
    enum Buttons {
        btnStatus,
        btnInventory,
        btnShop,
    }
    enum Objects {
        sliderExp,
        Buttons,
    }

    #endregion

    #region Fields

    private Player _player;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetButton((int)Buttons.btnStatus).gameObject.BindEvent(OnBtnStatus);
        GetButton((int)Buttons.btnInventory).gameObject.BindEvent(OnBtnInventory);
        GetButton((int)Buttons.btnShop).gameObject.BindEvent(OnBtnShop);

        // TODO:: FindObjectOfType 지우기
        _player = FindObjectOfType<MainScene>().Player;
        _player.Inventory.OnGoldChanged += SetGold;
        _player.OnPlayerDataUpdated += SetPlayerData;

        SetGold(_player.Inventory.Gold);
        SetPlayerData();

        return true;
    }

    private void SetGold(float gold) {
        GetText((int)Texts.txtGold).text = $"{gold:F0}";
    }
    private void SetPlayerData() {
        GetText((int)Texts.txtName).text = _player.UserName;
        GetText((int)Texts.txtClass).text = _player.ClassName;
        GetText((int)Texts.txtLevel).text = _player.Level.ToString();
        GetText((int)Texts.txtExp).text = $"{_player.CurrentExp:F0} / {_player.RequiredExp:F0}";
        GetObject((int)Objects.sliderExp).GetComponent<Slider>().value = _player.ExpRatio;
        GetText((int)Texts.txtDescription).text = _player.ClassDescription;
    }

    public void ShowButtons() {
        GetButton((int)Buttons.btnStatus).gameObject.SetActive(true);
        GetButton((int)Buttons.btnInventory).gameObject.SetActive(true);
        GetButton((int)Buttons.btnShop).gameObject.SetActive(true);
        GetButton((int)Buttons.btnStatus).image.DOFade(1, 0.25f);
        GetText((int)Texts.txtStatus).DOFade(1, 0.25f);
        GetButton((int)Buttons.btnInventory).image.DOFade(1, 0.25f);
        GetText((int)Texts.txtInventory).DOFade(1, 0.25f);
        GetButton((int)Buttons.btnShop).image.DOFade(1, 0.25f);
        GetText((int)Texts.txtShop).DOFade(1, 0.25f);
    }
    public void HideButtons() {
        GetButton((int)Buttons.btnStatus).image.DOFade(0, 0.25f).OnComplete(() => GetButton((int)Buttons.btnStatus).gameObject.SetActive(false));
        GetText((int)Texts.txtStatus).DOFade(0, 0.25f);
        GetButton((int)Buttons.btnInventory).image.DOFade(0, 0.25f).OnComplete(() => GetButton((int)Buttons.btnInventory).gameObject.SetActive(false));
        GetText((int)Texts.txtInventory).DOFade(0, 0.25f);
        GetButton((int)Buttons.btnShop).image.DOFade(0, 0.25f).OnComplete(() => GetButton((int)Buttons.btnShop).gameObject.SetActive(false));
        GetText((int)Texts.txtShop).DOFade(0, 0.25f);
    }

    #region OnButtons

    private void OnBtnStatus() {
        Main.UI.ShowDrawer<UI_Drawer_Status>();
        HideButtons();
    }
    private void OnBtnInventory() {
        Main.UI.ShowDrawer<UI_Drawer_Inventory>();
        HideButtons();
    }
    private void OnBtnShop() {

    }

    #endregion
}