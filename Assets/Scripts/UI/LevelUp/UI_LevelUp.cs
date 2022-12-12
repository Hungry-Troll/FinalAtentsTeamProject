using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 레벨업 UI
public class UI_LevelUp : MonoBehaviour
{
    public Text _beforeHp;
    public Text _afterHp;
    public Text _beforeATK;
    public Text _afterATK;


    // Start is called before the first frame update
    public void Init()
    {
        // 데이터에 연결할 텍스트를 변수에 대입
        // UI 매니저 >> LevelUpController에서 데이터 대입
        _beforeHp = Util.FindChild("BeforeHp", transform).GetComponent<Text>();
        _afterHp = Util.FindChild("AfterHp", transform).GetComponent<Text>();
        _beforeATK = Util.FindChild("BeforeATK", transform).GetComponent<Text>();
        _afterATK = Util.FindChild("AfterATK", transform).GetComponent<Text>();
    }
}
