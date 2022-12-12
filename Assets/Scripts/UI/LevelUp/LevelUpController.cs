using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ��Ʈ�ѷ����� GetComponent�� �̿��ؼ� ��� ���� ����
public class LevelUpController : MonoBehaviour
{
    float _effectTime;
    float _uiTime;
    ParticleSystem _levelUp1;
    ParticleSystem _levelUp2;
    Coroutine _coLevel;
    int _beforeHp;
    int _beforeAtk;
    // �ӽ� Ŭ����
    TempStatEX _tempStatEX;
    public int ExpAmount
    {
        get
        {
            return GameManager.Obj._playerStat.Exp;
        }
        set
        {
            GameManager.Obj._playerStat.Exp += value;
            LevelUpCheck();
        }
    }
    // Init�Լ��� �÷��̾���Ʈ�ѷ����� ����
    public void Start()
    {
        // ����Ʈ ����
        _levelUp1 = Util.FindChild("LevelUpEffect1", transform).GetComponent<ParticleSystem>();
        _levelUp2 = Util.FindChild("LevelUpEffect2", transform).GetComponent<ParticleSystem>();
        // ����Ʈ ������
        LeveUpEffectOnOff(false);
        // �ð� �ʱ�ȭ
        _effectTime = 0.0f;
        _uiTime = 0.0f;
        // ������ �ڷ�ƾ �ʱ�ȭ
        _coLevel = null;
    }
    // ����ġ üũ �Լ�
    public void LevelUpCheck()
    {
        if(GameManager.Obj._playerStat.Exp >= GameManager.Obj._playerStat.Lv_Exp)
        {
            // ������
            LevelUp(GameManager.Obj._playerStat.Lv);
        }
    }

    public void LevelUp(int Lv)
    {
        Define.Job job;
        // ���� ���� Ȯ��
        job = GameManager.Select.SelectJobCheck();

        // �������� �������� �ӽ� ����
        _tempStatEX = GameManager.Parse.FindPlayerObjData2(Lv, job);
        _beforeHp = _tempStatEX.Max_Hp;
        _beforeAtk = GameManager.Obj._playerStat.Atk;

        // ������ �´� ������ �ε�
        GameManager.Parse.FindPlayerObjData(++Lv, job);
        // ����Ʈ �� UI ����
        _coLevel = StartCoroutine(LevelUpEffect());
        // ������ ���� ���
        GameManager.Sound.SFXPlay("LevelUp");
        // ��ų���� ����
        GameManager.Ui._skillViewController.LevelUp();
        // ��Ʈ����Ʈ HP�� ���� ����
        GameManager.Ui._playerHpBarController._Lv.text = "Lv " + Lv.ToString();
    }
    // ������ ����Ʈ ���� Ű��
    public void LeveUpEffectOnOff(bool value)
    {
        switch(value)
        {
            case true:
                _levelUp1.Play();
                _levelUp2.Play();
                break;
            case false:
                _levelUp1.Stop();
                _levelUp2.Stop();
                break;
        }
    }
    
    IEnumerator LevelUpEffect()
    {
        // ����Ʈ ����
        LeveUpEffectOnOff(true);

        // UI ����
        LevelUpUIOn();
        while (true)
        {
            yield return null;
            // Ÿ�� ���
            _effectTime += Time.deltaTime;
            _uiTime += Time.deltaTime;
            if (_effectTime > 1.0f)
            {
                LeveUpEffectOnOff(false);
                _coLevel = null;
                _effectTime -= _effectTime;
            }
            if (_uiTime > 3.0f)
            {
                LevelUpUIOff();
            }
        }
    }
    // ������ UI ����
    public void LevelUpUIOn()
    {
        GameManager.Ui._uiLevelUpObj.SetActive(true);
        // ���� ���� �־��ֱ� 
        GameManager.Ui._uiLevelUp._beforeHp.text = _beforeHp.ToString();
        GameManager.Ui._uiLevelUp._beforeATK.text = _beforeAtk.ToString();
        // ���� ���� �־��ֱ�
        GameManager.Ui._uiLevelUp._afterHp.text = GameManager.Obj._playerStat.Max_Hp.ToString();
        GameManager.Ui._uiLevelUp._afterATK.text = GameManager.Obj._playerStat.Atk.ToString();
        // �κ��丮�� ���̴� ���� ������Ʈ
        GameManager.Ui.InventoryStatUpdate();
    }
    // �ݱ�
    public void LevelUpUIOff()
    {
        GameManager.Ui._uiLevelUpObj.SetActive(false);
    }
}
