using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class BossChangeEX : MonsterControllerEX
{
    private Animator _animator;
    private ParticleSystem _smokeEffect;

    // Start is called before the first frame update
    public override void Awake()
    {
        _smokeEffect = Util.FindChild("Smoke", transform).GetComponent<ParticleSystem>();
        smokeOff();
    }
    public override void Start()
    {
        _animator = GetComponent<Animator>();
        dead();
        //Invoke("spwan", 13);
        StartCoroutine(SpawnBoss());
        transform.LookAt(GameManager.Obj._playerController.gameObject.transform);
        GameManager.Sound.SFXPlay("BossText");
    }

    public void dead()
    {
        _animator.SetInteger("state", 1);
    }

    IEnumerator SpawnBoss()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if(time > 17.0f)
            {
                spwan();
            }
            if(time > 19.5f)
            {
                smokeOff();
                //StopCoroutine(SpawnBoss());
                yield break;
            }
            yield return null;
        }
    }

    public void spwan()
    {
        gameObject.SetActive(false);
        GameManager.Create.CreateBoss(gameObject.transform.position, "Boss");
    }
    // �ִϸ��̼� Ŭ������ 7.5�� ������ ����� >> ��ġŷ�� ����԰� �������� Ÿ�̹�
    public void smokeOn()
    {
        _smokeEffect.Play();
        //EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, null, EffectType.Smoke);
    }

    public void smokeOff()
    {
        _smokeEffect.Stop();
        //EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, null, EffectType.Smoke);
    }


    //
    // ���� �� ������
    public override void Update() { }
    public override void UpdateState() { }
    public override void UpdateIdle() { }
    public override void UpdateMoving() { }
    public override void UpdateAttack() { }
    public override void UpdateDead() { }
    public override void UpdateSkill() { }
    public override IEnumerator AttackDelay(float _delay) { yield return null; }
    public override IEnumerator DeadDelay(float _delay) { yield return null; }
    public override void OnDamaged(int playerAtk, int SkillDamagePercent) { }
}
