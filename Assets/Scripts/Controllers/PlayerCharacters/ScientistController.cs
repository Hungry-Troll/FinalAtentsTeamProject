using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ��Ʈ�ѷ� ��ӹ��� ���̾�Ƽ��Ʈ ��Ʈ�ѷ�

// �θ��� PlayerController�� MonoBehaviour(Awake, Start, Udate ��)�� ����ϰ� �ʹٸ� 
// -> �θ��� �Լ��� virtual Ű���� �߰��ϰ� �ڽ��� �Լ��� override Ű���� �ٿ��� ������ �ϸ� �ȴ�.
// -> ����� �θ� �Լ� ȣ���� base.�Լ���(); ((ex) base.Awake();)
// �ڼ��� ����) https://dragontory.tistory.com/307

public class ScientistController : PlayerController
{
    // ���̺��� ��Ÿ�� �ʵ�
    protected GameObject _skillGround;
    public override void JobStart()
    {
        //��Ÿ�� ��ų ���� ����
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
