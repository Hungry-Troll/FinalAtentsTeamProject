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
    public GameObject[] _startPosMonster;
    public Vector3[] _startPosMob;


    // Start is called before the first frame update
    void Awake()
    {
        // �� ���� �´� Awake �Լ� ȣ��
        SceneCheck();
    }

    // �� ���� �´� Awake �Լ� ȣ��� �Լ�
    private void SceneCheck()
    {
        switch (GameManager.Scene._sceneNameEnum)
        {
            case Define.SceneName.Tutorial:
                TutorialAwake();
                break;
            case Define.SceneName.Village02:
                NextSceneAwake();
                // ���� �ȳ� ȭ��ǥ ���� / �ӽ� ��ġ, �Ŀ� Ʃ�丮�� ���� Ŭ�������� ó��
                // �����ִ� ȭ��ǥ ���� �ø� ���, ���� ���ֱ�
                GameManager.Ui._directionArrowController.OffAllArrows();
                GameManager.Ui._directionArrowController.OnArrow("VillageToDungeon");
                break;
            case Define.SceneName.DunGeon:
                DungeonSceneAwake();
                // ���� ���� �ȳ� ȭ��ǥ ���ֱ� / ���� -> �ʿ� -> ȭ��
                // �����ִ� ȭ��ǥ ���� �ø� ���, ���� ���ֱ�
                GameManager.Ui._directionArrowController.OffAllArrows();
                GameManager.Ui._directionArrowController.OnArrow("DungeonCourse");
                break;
        }
    }

    private void TutorialAwake()
    {
        // ����� �ؽ�Ʈ �Ŵ���
        GameManager.DamText.Init();
        // ���ӸŴ������� Ui�Ŵ��� Init(Awake �Լ� ��ü)
        // ������Ʈ �Ŵ��� Init();
        GameManager.Obj.Init();
        // Ui �ҷ���
        GameManager.Ui.Init();
        // Select �Ŵ������� � ĳ���Ͷ� ���� �����ߴ��� Ȯ��
        GameManager.Select.Init();
        // ���� �Ŵ������� ���� ������ �ҷ���
        GameManager.Stat.Init();
        // ��ų �Ŵ������� ��ų ������ �ҷ���
        GameManager.Skill.Init();
        // ī�޶� ����
        GameManager.Cam.Init();
        // ��ƼŬ ����
        GameManager.Effect.Init();
        // Parse �Ŵ���
        GameManager.Parse.Init();

        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;

        //�÷��̾� ����
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());

        // ���ҽ��Ŵ������� ã�Ƽ� �÷��̾� ������ �κ��丮�� �Ѱ� �־���
        GameManager.Create.CreateInventoryItem("sword1");

        // ������ ����Ʈ ����
        GameManager.Ui._skillViewController.LevelUp();

        // ���� ���� ��ġ
        _startPosMob[0] = _startPosMonster[0].transform.position;
        // ���� ���� �ڵ�
        for (int i = 0; i < 1 ; i++)
        {
            // ����Ʈ ���� ����
            MonsterControllerEX monster = GameManager.Create.CreateQuestMonster(_startPosMob[0], "Velociraptor");
            // ������ ���ڸ� �Ѿ��� (���� ������)
            monster.gameObject.name = monster.gameObject.name + i+1;
        }

        // ����Ʈ ����� ���� ����
        GameManager.Create.CreateQuestDoor(transform.position, "TutorialDoor");

        // �� ���� �ڵ�
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());

        // ������ ������ �׽�Ʈ �ڵ�
        //for (int i = 0; i < GameManager.Resource._fieldItem.Count; i++)
        //{
        //    Vector3 tempPos = new Vector3(Random.Range(i, i + 3), Random.Range(i, i + 3), Random.Range(i, i + 3));
        //    GameManager.Create.CreateFieldItem(_startPos + tempPos, GameManager.Resource._fieldItem[i].name);
        //}

        // BGM ����
        GameManager.Sound.BGMPlay("Sketch 3 - Against All Odds 1");
    }

    private void NextSceneAwake()
    {
        // ����� �ؽ�Ʈ �Ŵ���
        GameManager.DamText.Init();
        // ������Ʈ �Ŵ��� Init();
        GameManager.Obj.Init();
        // ������Ʈ �Ŵ������� ���� ���� ����Ʈ �ʱ�ȭ
        GameManager.Obj.RemoveAllMobList();
        // Ui �ҷ���
        GameManager.Ui.Init();
        // ������ �ε�
        GameManager.Data.LoadData(false);
        // Select �Ŵ������� � ĳ���Ͷ� ���� �����ߴ��� Ȯ��
        GameManager.Select.Init();
        // ���� �Ŵ������� ���� ������ �ҷ���
        GameManager.Stat.Init();
        // ��ų �Ŵ������� ��ų ������ �ҷ���
        GameManager.Skill.Init();
        // ī�޶� ����
        GameManager.Cam.Init();
        // ��ƼŬ ����
        GameManager.Effect.Init();
        // Parse �Ŵ���
        GameManager.Parse.Init();

        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;
        //�÷��̾� ����
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());
        // ������ �ε�
        //GameManager.Data.LoadData(false);
        // ���� ����
        GameManager.Data.EquipWeaponLoad();

        // �� ���� �ڵ�
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());
    }

    private void DungeonSceneAwake()
    {
        // ����� �ؽ�Ʈ �Ŵ���
        GameManager.DamText.Init();
        // ������Ʈ �Ŵ��� Init();
        GameManager.Obj.Init();
        // ������Ʈ �Ŵ������� ���� ���� ����Ʈ �ʱ�ȭ
        GameManager.Obj.RemoveAllMobList();
        // Ui �ҷ���
        GameManager.Ui.Init();
        // ������ �ε�
        GameManager.Data.LoadData(false);
        // Select �Ŵ������� � ĳ���Ͷ� ���� �����ߴ��� Ȯ��
        GameManager.Select.Init();
        // ���� �Ŵ������� ���� ������ �ҷ���
        GameManager.Stat.Init();
        // ��ų �Ŵ������� ��ų ������ �ҷ���
        GameManager.Skill.Init();
        // ī�޶� ����
        GameManager.Cam.Init();
        // ��ƼŬ ����
        GameManager.Effect.Init();
        // Parse �Ŵ���
        GameManager.Parse.Init();

        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;
        //�÷��̾� ����
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());
        // ������ �ε�
        //GameManager.Data.LoadData(false);
        // ���� ����
        GameManager.Data.EquipWeaponLoad();

        // �� ���� �ڵ�
        Vector3 temPos = new Vector3(Random.Range(3, 5), Random.Range(3, 5), Random.Range(3, 5));
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPos + temPos, GameManager.Select._pet.ToString());

        // ���� 7���� ����
        for (int i = 0; i < 7; i++)
        {
            // ���� ���� ��ġ
            _startPosMob[i] = _startPosMonster[i].transform.position;
            // ����Ʈ ���� ����(�ӽ�)
            MonsterControllerEX monster = GameManager.Create.CreateQuestMonster(_startPosMob[i], "Velociraptor");
            // ������ ���ڸ� �Ѿ��� (���� ������)
            monster.gameObject.name = monster.gameObject.name + i+1;
        }
    }
}
