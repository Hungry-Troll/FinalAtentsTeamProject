using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class FieldManager : MonoBehaviour
{
    //���� ���۽� Awake , Start, Update ��� �뵵 �Ŵ���

    // �÷��̾� �� ������ ���� ������ ���� ���� ����
    // ���� �� ������ �����ؼ� GameManager.Obj �� ���� ������Ʈ ����
    PlayerController _player;
    PetController _pet;
    ItemController _item;
    ItemStatEX _itemStatEX;
    MonsterController _monster;
    MonsterStat _monsterStat;

    // �÷��̾� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosObject;
    public Vector3 _startPos;

    // �ӽ� ���� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosMonster;
    public Vector3 _startPosMob;

    // Start is called before the first frame update
    void Awake()
    {
        // ���ӸŴ������� Ui�Ŵ��� Init(Awake �Լ� ��ü)
        // Ui �ҷ���
        GameManager.Ui.Init();
        // Select �Ŵ������� � ĳ���Ͷ� ���� �����ߴ��� Ȯ��
        GameManager.Select.Init();
        // ���� �Ŵ������� ���� ������ �ҷ���
        GameManager.Stat.Init();
        // ī�޶� ����
        GameManager.Cam.Init();

        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;

        //�÷��̾� ����
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());

        // ���ҽ��Ŵ������� ã�Ƽ� �÷��̾� ������ �κ��丮�� �Ѱ� �־���
        GameObject tmp = GameManager.Resource.GetfieldItem("sword1");
        GameManager.Item.InventoryItemAdd(tmp, false);

        // ���� ���� ��ġ
        _startPosMob = _startPosMonster.transform.position;
        // ���� ������ �׽�Ʈ �ڵ�
        for (int i = 0; i < GameManager.Resource._monster.Count-1; i++)
        {
            GameManager.Create.CreateMonster(_startPosMob, GameManager.Resource._monster[i].name);
        }

        // �� ���� �׽�Ʈ �ڵ�
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3,5), Random.Range(3, 5));
        GameManager.Obj._petController= GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());

        // ������ ������ �׽�Ʈ �ڵ�
        for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        {
            Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
            GameManager.Create.CreateFieldItem(_startPos + tempPos, GameManager.Resource._fieldItem[i].name);
        }

        // BGM ����
        GameManager.Sound.BGMPlay("-kpop_release-");


    }
}
