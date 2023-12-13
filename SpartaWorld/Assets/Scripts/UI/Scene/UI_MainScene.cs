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

    #region OnButtons

    private void OnBtnStatus() {

    }
    private void OnBtnInventory() { 

    }
    private void OnBtnShop() {

    }

    #endregion
}