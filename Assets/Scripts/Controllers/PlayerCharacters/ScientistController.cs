using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 컨트롤러 상속받은 사이언티스트 컨트롤러

// 부모인 PlayerController의 MonoBehaviour(Awake, Start, Udate 등)를 사용하고 싶다면 
// -> 부모의 함수에 virtual 키워드 추가하고 자식의 함수에 override 키워드 붙여서 재정의 하면 된다.
// -> 참고로 부모 함수 호출은 base.함수명(); ((ex) base.Awake();)
// 자세한 내용) https://dragontory.tistory.com/307

public class ScientistController : PlayerController
{
    // 사이보그 논타겟 필드
    protected GameObject _skillGround;
    public override void JobStart()
    {
        //논타겟 스킬 범위 변수
        _skillGround = Util.FindChild("SkillGround", transform).gameObject;
        _scientistSkill1 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill1"));
        _poison = FindEffect("Poison", _scientistSkill1.transform);
        _explosion = FindEffect("Explosion", _scientistSkill1.transform);
        _scientistSkill2 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill2"));
        _powerDraw = FindEffect("PowerDraw", _scientistSkill2.transform);
        _electricity = FindEffect("Electricity", _scientistSkill2.transform);
        _scientistSkill3 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particle_Prefab/ScientistSkill3"));
        _powerBeam = FindEffect("PowerBeam", _scientistSkill3.transform);
        _poisonPortion = Util.FindChild("potion", transform);
        _portionPosition = _poisonPortion.localPosition;
    }
}
