using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatInfo : UI_Base {

    #region Enums

    enum Images {
        imgIcon,
    }

    enum Texts {
        txtLabel,
        txtValue,
    }

    #endregion

    #region Fields

    private Stat _stat;

    #endregion

    #region MonoBehaviours

    void OnEnable() {
        Initialize();
    }
    void OnDestroy() {
        if (_stat != null)
        _stat.OnChanged -= SetValue;
    }

    #endregion

    public override bool Initialize() {
        if (!base.Initialize()) return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        return true;
    }
    
    public void SetInfo(StatType type) {
        Initialize();

        GetImage((int)Images.imgIcon).sprite = Main.Resource.Load<Sprite>($"Icon_{type}.sprite");
        GetText((int)Texts.txtLabel).text = type switch {
            StatType.Hp => "체력",
            StatType.Damage => "공격력",
            StatType.Defense => "방어력",
            StatType.Critical => "치명타",
            _ => ""
        };

        // TODO:: Find 삭제.
        _stat = FindObjectOfType<MainScene>().Player.Status[type];
        _stat.OnChanged += SetValue;
        SetValue(_stat);
    }

    private void SetValue(Stat stat) {
        float originValue = stat.OriginValue;
        float deltaValue = stat.Value - originValue;
        if (deltaValue == 0) {
            GetText((int)Texts.txtValue).text = $"{originValue}";
        }
        else {
            GetText((int)Texts.txtValue).text = $"{originValue} <color=yellow>({deltaValue:+#;-#})</color>";
        }
    }
}