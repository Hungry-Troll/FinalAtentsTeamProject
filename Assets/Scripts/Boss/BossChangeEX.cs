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
    public override void Start()
    {
        _smokeEffect = Util.FindChild("Smoke", transform).GetComponent<ParticleSystem>();
        _animator = GetComponent<Animator>();
        dead();
        smokeOff();
        //Invoke("spwan", 13);
        StartCoroutine(SpawnBoss());
        transform.LookAt(GameManager.Obj._playerController.gameObject.transform);
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
            if(time > 19.5f)
            {
                spwan();
                smokeOff();
                StopCoroutine(SpawnBoss());
            }
            yield return null;
        }
    }

    public void spwan()
    {
        gameObject.SetActive(false);
        GameManager.Create.CreateBoss(gameObject.transform.position, "Boss");
    }
    // 애니메이션 클립에서 7.5초 정도에 재생됨 >> 리치킹이 물약먹고 쓰러지는 타이밍
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
    // 이하 빈 껍대기용
    public override void Awake() { }
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
