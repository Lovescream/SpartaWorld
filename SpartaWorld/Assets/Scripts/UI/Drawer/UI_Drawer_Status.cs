using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Drawer_Status : UI_Drawer {

    #region Enums

    enum Objects {
        Status,
    }
    enum Buttons {
        btnBack,
    }

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.btnBack).gameObject.BindEvent(OnBtnBack);

        GetObject((int)Objects.Status).DestroyChilds();
        for (int i = 0; i < (int)StatType.COUNT; i++) {
            UI_StatInfo statInfo = Main.UI.CreateSubItem<UI_StatInfo>(GetObject((int)Objects.Status).transform);
            statInfo.SetInfo((StatType)i);
        }

        return true;
    }

    #region OnButtons

    private void OnBtnBack() {
        CloseDrawer();
        // TODO:: Find 지우기.
        (FindObjectOfType<MainScene>().UI as UI_MainScene).ShowButtons();
    }

    #endregion
}