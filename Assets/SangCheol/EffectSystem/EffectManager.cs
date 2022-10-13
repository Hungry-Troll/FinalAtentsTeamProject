using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<EffectManager>();
            return _instance;
        }
    }

    public ParticleSystem _missEffect;
    public ParticleSystem _NormalHitEffect;
    public ParticleSystem _criticalHitEffect;

    public ParticleSystem _roarEffect;
    public ParticleSystem _scratchEffect;
    public ParticleSystem _biteEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

        //����Ʈ ����
        var effect = Instantiate(_playerPrefab, _pos, Quaternion.LookRotation(_dir));

        //�θ� ������ �θ� transform���� ����
        if (_parent != null)
        {
            effect.transform.SetParent(_parent);
        }

        effect.Play();
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

        //����Ʈ ����
        var effect = Instantiate(_monsterPrefab, _pos, Quaternion.LookRotation(_dir));

        //�θ� ������ �θ� transform���� ����
        if (_parent != null)
        {
            effect.transform.SetParent(_parent);
        }

        effect.Play();
    }
}
