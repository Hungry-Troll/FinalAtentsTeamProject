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

        //이펙트 생성
        var effect = Instantiate(_playerPrefab, _pos, Quaternion.LookRotation(_dir));

        //부모가 있으면 부모 transform으로 따라감
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

        //이펙트 생성
        var effect = Instantiate(_monsterPrefab, _pos, Quaternion.LookRotation(_dir));

        //부모가 있으면 부모 transform으로 따라감
        if (_parent != null)
        {
            effect.transform.SetParent(_parent);
        }

        effect.Play();
    }
}
