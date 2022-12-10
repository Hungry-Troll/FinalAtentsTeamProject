using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class BossChange : MonoBehaviour
{
    public Animator _ani;
    [SerializeField] GameObject MobPrefab = null;
    //public GameObject hp;

    public Canvas _canvas;
    public Image _hpBarImage;
    public GameObject _hpBarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _ani = GetComponent<Animator>();
        dead();
        Invoke("spwan", 13);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void dead()
    {
        _ani.SetInteger("state", 1);
    }

    public void spwan()
    {
        gameObject.SetActive(false);
        GameObject mob = Instantiate(MobPrefab, new Vector3(0, 0, 150), Quaternion.identity);
        //Instantiate(hp, createPoint, Quaternion.identity, GameObject.Find("Canvas").transform);
        _canvas = GameObject.Find("Ui Canvas").GetComponent<Canvas>();
        GameObject _hpbar = Instantiate<GameObject>(_hpBarPrefab, _canvas.transform);
        _hpBarImage = _hpbar.GetComponentsInChildren<Image>()[1];
    }

    public void smokeSpwan()
    {
        EffectManager.Instance.MonsterEffect(this.transform.position, Vector3.zero, null, EffectType.Smoke);
    }
}
