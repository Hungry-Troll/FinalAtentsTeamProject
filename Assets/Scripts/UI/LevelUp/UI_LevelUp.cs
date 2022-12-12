using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ UI
public class UI_LevelUp : MonoBehaviour
{
    public Text _beforeHp;
    public Text _afterHp;
    public Text _beforeATK;
    public Text _afterATK;


    // Start is called before the first frame update
    public void Init()
    {
        // �����Ϳ� ������ �ؽ�Ʈ�� ������ ����
        // UI �Ŵ��� >> LevelUpController���� ������ ����
        _beforeHp = Util.FindChild("BeforeHp", transform).GetComponent<Text>();
        _afterHp = Util.FindChild("AfterHp", transform).GetComponent<Text>();
        _beforeATK = Util.FindChild("BeforeATK", transform).GetComponent<Text>();
        _afterATK = Util.FindChild("AfterATK", transform).GetComponent<Text>();
    }
}
