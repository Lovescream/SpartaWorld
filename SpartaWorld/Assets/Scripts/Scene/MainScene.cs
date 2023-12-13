using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene {
    
    public Player Player { get; private set; }

    protected override bool Initialize() {
        if (!base.Initialize()) return false;

        Player = new("Player", Main.Data.Characters["Carrot"]);

        UI = Main.UI.ShowSceneUI<UI_MainScene>();

        return true;
    }

}