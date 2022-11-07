using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EffectManagerEX
{
    // ������ ��ƼŬ
    public ParticleSystem _levelUpPar;
    // ��ƼŬ ����� ���� ĵ���� UI ǥ�鿡 ���
    GameObject _particleCanvas;

    public ParticleSystem _missEffect;
    public ParticleSystem _NormalHitEffect;
    public ParticleSystem _criticalHitEffect;

    public ParticleSystem _roarEffect;
    public ParticleSystem _scratchEffect;
    public ParticleSystem _biteEffect;

    public void Init()
    {
        // ��ƼŬ ���� ĵ���� ����
        _particleCanvas = GameManager.Create.CreateUi("UI_ParticleCanvas", GameManager.Ui.go);
        Canvas canvas = _particleCanvas.GetComponent<Canvas>();
        // ĵ������ ī�޶� �ڵ�� ������ >> ī�޶� �Ŵ��� Init() �Լ� ���� �� ����Ʈ �Ŵ����� ���� �Ǿ� ��
        canvas.worldCamera = GameManager.Cam._uiParticleCamera.GetComponent<Camera>();
        // ��ƼŬ ���� >> ũ������Ʈ �Ŵ����� ���� ���� 
        ParticleSystem tmp = GameManager.Resource.GetParticle("PortraitParticle");
        _levelUpPar = GameObject.Instantiate<ParticleSystem>(tmp);
        // ��Ȱ��ȭ
        _levelUpPar.gameObject.SetActive(false);
    }

    // �ʵ忡�� ��ų�� �����غ���
    //_pos = ����Ʈ ��ġ, _dir = ����Ʈ ����, _parent = �����̴� ���� �θ�� ��� ����Ʈ�� ���󰡾� �ȴ�, 
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

        //����Ʈ ���� >> �Ŵ����� MonoBehaviour �����ؼ� Instantiate �� �ȵǳ�...��... >> ���� ��Ʈ�ѷ��� ���� �ڵ带 �ű�� �׽�Ʈ
        //var effect = Instantiate(_playerPrefab, _pos, Quaternion.LookRotation(_dir));

        //�θ� ������ �θ� transform���� ����
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

        //����Ʈ ���� >> �Ŵ����� MonoBehaviour �����ؼ� Instantiate �� �ȵǳ�...��... >> ���� ��Ʈ�ѷ��� ���� �ڵ带 �ű�� �׽�Ʈ
        //var effect = Instantiate(_monsterPrefab, _pos, Quaternion.LookRotation(_dir));

        //�θ� ������ �θ� transform���� ����
        if (_parent != null)
        {
            //effect.transform.SetParent(_parent);
        }
        //effect.Play();
    }

    // ������ ����Ʈ �ѱ�
    public void LevelUpPortraitEffectOn()
    {
        _levelUpPar.gameObject.SetActive(true);
    }
    // ������ ����Ʈ ����
    public void LevelUpPortraitEffectOff()
    {
        _levelUpPar.gameObject.SetActive(false);
    }
}
