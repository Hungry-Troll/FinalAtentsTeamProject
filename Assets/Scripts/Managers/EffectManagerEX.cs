using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EffectManagerEX
{
    // 레벨업 파티클
    public ParticleSystem _levelUpPar;
    // 파티클 사용을 위한 캔버스 UI 표면에 사용
    GameObject _particleCanvas;

    public ParticleSystem _missEffect;
    public ParticleSystem _NormalHitEffect;
    public ParticleSystem _criticalHitEffect;

    public ParticleSystem _roarEffect;
    public ParticleSystem _scratchEffect;
    public ParticleSystem _biteEffect;

    public void Init()
    {
        // 파티클 전용 캔버스 설정
        _particleCanvas = GameManager.Create.CreateUi("UI_ParticleCanvas", GameManager.Ui.go);
        Canvas canvas = _particleCanvas.GetComponent<Canvas>();
        // 캔버스에 카메라를 코드로 연결함 >> 카메라 매니저 Init() 함수 실행 후 이펙트 매니저가 실행 되야 됨
        canvas.worldCamera = GameManager.Cam._uiParticleCamera.GetComponent<Camera>();
        // 파티클 생성 >> 크리에이트 매니저로 추후 변경 
        ParticleSystem tmp = GameManager.Resource.GetParticle("PortraitParticle");
        _levelUpPar = GameObject.Instantiate<ParticleSystem>(tmp);
        // 비활성화
        _levelUpPar.gameObject.SetActive(false);
    }

    // 필드에서 스킬에 적용해보기
    //_pos = 이펙트 위치, _dir = 이펙트 방향, _parent = 움직이는 적을 부모로 삼아 이펙트가 따라가야 된다, 
    public void PlayerEffect(Vector3 _pos, Vector3 _dir, Transform _parent = null, EffectType effectType = EffectType.Miss)
    {
        var _playerPrefab = _missEffect;

        if (effectType == EffectType.Normal)
        {
            _playerPrefab = _NormalHitEffect;
        }
        else if(effectType == EffectType.Critical)
        {
            _playerPrefab = _criticalHitEffect;
        }

        //이펙트 생성 >> 매니저는 MonoBehaviour 제거해서 Instantiate 이 안되냄...흠... >> 전용 컨트롤러를 만들어서 코드를 옮기고 테스트
        //var effect = Instantiate(_playerPrefab, _pos, Quaternion.LookRotation(_dir));

        //부모가 있으면 부모 transform으로 따라감
        if (_parent != null)
        {
            //effect.transform.SetParent(_parent);
        }

        //effect.Play();
    }
    public void MonsterEffect(Vector3 _pos, Vector3 _dir, Transform _parent = null, EffectType effectType = EffectType.Roar)
    {
        var _monsterPrefab = _roarEffect;

        if (effectType == EffectType.Scratch)
        {
            _monsterPrefab = _scratchEffect;
        }
        else if (effectType == EffectType.Bite)
        {
            _monsterPrefab = _biteEffect;
        }

        //이펙트 생성 >> 매니저는 MonoBehaviour 제거해서 Instantiate 이 안되냄...흠... >> 전용 컨트롤러를 만들어서 코드를 옮기고 테스트
        //var effect = Instantiate(_monsterPrefab, _pos, Quaternion.LookRotation(_dir));

        //부모가 있으면 부모 transform으로 따라감
        if (_parent != null)
        {
            //effect.transform.SetParent(_parent);
        }
        //effect.Play();
    }

    // 레벨업 이펙트 켜기
    public void LevelUpPortraitEffectOn()
    {
        _levelUpPar.gameObject.SetActive(true);
    }
    // 레벨업 이펙트 끄기
    public void LevelUpPortraitEffectOff()
    {
        _levelUpPar.gameObject.SetActive(false);
    }
}
