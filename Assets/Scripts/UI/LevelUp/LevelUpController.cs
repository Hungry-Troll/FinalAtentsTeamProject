using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 컨트롤러에서 GetComponent를 이용해서 들고 있을 예정
public class LevelUpController : MonoBehaviour
{
    float _effectTime;
    float _uiTime;
    ParticleSystem _levelUp1;
    ParticleSystem _levelUp2;
    Coroutine _coLevel;
    int _beforeHp;
    int _beforeAtk;
    // 임시 클래스
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
    // Init함수는 플레이어컨트롤러에서 실행
    public void Start()
    {
        // 이펙트 연결
        _levelUp1 = Util.FindChild("LevelUpEffect1", transform).GetComponent<ParticleSystem>();
        _levelUp2 = Util.FindChild("LevelUpEffect2", transform).GetComponent<ParticleSystem>();
        // 이펙트 꺼놓기
        LeveUpEffectOnOff(false);
        // 시간 초기화
        _effectTime = 0.0f;
        _uiTime = 0.0f;
        // 레벨업 코루틴 초기화
        _coLevel = null;
    }
    // 경험치 체크 함수
    public void LevelUpCheck()
    {
        if(GameManager.Obj._playerStat.Exp >= GameManager.Obj._playerStat.Lv_Exp)
        {
            // 레벨업
            LevelUp(GameManager.Obj._playerStat.Lv);
        }
    }

    public void LevelUp(int Lv)
    {
        Define.Job job;
        // 선택 직업 확인
        job = GameManager.Select.SelectJobCheck();

        // 레벨업전 이전스텟 임시 저장
        _tempStatEX = GameManager.Parse.FindPlayerObjData2(Lv, job);
        _beforeHp = _tempStatEX.Max_Hp;
        _beforeAtk = GameManager.Obj._playerStat.Atk;

        // 레벨에 맞는 데이터 로드
        GameManager.Parse.FindPlayerObjData(++Lv, job);
        // 이펙트 및 UI 생성
        _coLevel = StartCoroutine(LevelUpEffect());
        // 레벨업 사운드 재생
        GameManager.Sound.SFXPlay("LevelUp");
        // 스킬레벨 증가
        GameManager.Ui._skillViewController.LevelUp();
        // 포트레이트 HP바 레벨 증가
        GameManager.Ui._playerHpBarController._Lv.text = "Lv " + Lv.ToString();
    }
    // 레벨업 이펙트 끄고 키기
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
        // 이펙트 시작
        LeveUpEffectOnOff(true);

        // UI 시작
        LevelUpUIOn();
        while (true)
        {
            yield return null;
            // 타임 계산
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
    // 레벨업 UI 열기
    public void LevelUpUIOn()
    {
        GameManager.Ui._uiLevelUpObj.SetActive(true);
        // 이전 스텟 넣어주기 
        GameManager.Ui._uiLevelUp._beforeHp.text = _beforeHp.ToString();
        GameManager.Ui._uiLevelUp._beforeATK.text = _beforeAtk.ToString();
        // 현재 스텟 넣어주기
        GameManager.Ui._uiLevelUp._afterHp.text = GameManager.Obj._playerStat.Max_Hp.ToString();
        GameManager.Ui._uiLevelUp._afterATK.text = GameManager.Obj._playerStat.Atk.ToString();
        // 인벤토리에 보이는 스텟 업데이트
        GameManager.Ui.InventoryStatUpdate();
    }
    // 닫기
    public void LevelUpUIOff()
    {
        GameManager.Ui._uiLevelUpObj.SetActive(false);
    }
}
