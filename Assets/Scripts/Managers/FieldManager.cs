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

    // �� ��ġ ������ / ����ִ� ���ӿ�����Ʈ�� �÷��̾� ��ġ ����
    public GameObject _startPosPetObj;
    public Vector3 _startPosPet;

    // ���� ��ȯ���� ��ġ ������
    public GameObject[] _startPosSumBossMonster;
    public Vector3[] _startPosSumBossMob;

    // Start is called before the first frame update
    void Awake()
    {
        // �ʵ�Ŵ��� ������ ������Ʈ�Ŵ������� ����
        GameManager.Obj._fieldManager = GetComponent<FieldManager>();
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
                // ���� ���� �ȳ� UI �ڵ带 Ui_NpcController���� ó��
                //
                //GameManager.Ui._directionArrowController.OffAllArrows();
                //GameManager.Ui._directionArrowController.OnArrow("VillageToDungeon");
                break;
            case Define.SceneName.DunGeon:
                DungeonSceneAwake();
                // ���� ���� �ȳ� UI �ڵ带 Ui_NpcController���� ó��
                //
                //GameManager.Ui._directionArrowController.OffAllArrows();
                //GameManager.Ui._directionArrowController.OnArrow("DungeonCourse");
                break;
        }
    }

    private void TutorialAwake()
    {
        // �ߺ� ���� 
        ManagerInit();

        // �÷��̾� ����
        CreatePlayer();

        // ���ҽ��Ŵ������� ã�Ƽ� �÷��̾� ������ �κ��丮�� �Ѱ� �־���
        switch(GameManager.Select._job)
        {
            case Job.Superhuman:
                GameManager.Create.CreateInventoryItem("sword1");
                break;
            case Job.Cyborg:
                GameManager.Create.CreateInventoryItem("gun1");
                break;
            case Job.Scientist:
                GameManager.Create.CreateInventoryItem("book1");
                break;
        }

        // ������ ����Ʈ ����
        GameManager.Ui._skillViewController.LevelUp();

        // ���� ����
        int questMonsterCnt = 1;
        CreateQuestMonster("Velociraptor", questMonsterCnt);

        // ����Ʈ ����� ���� ����
        GameManager.Create.CreateQuestDoor(transform.position, "TutorialDoor");

        // �� ����
        CreatePet();

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
        // �ߺ� ���� 
        ManagerInit();

        CreatePlayer();

        // ������ �ε�
        //GameManager.Data.LoadData(false);
        // ���� ����
        GameManager.Data.EquipWeaponLoad();
        // �� ����
        GameManager.Data.EquipArmourLoad();
        // �� ����
        CreatePet();
    }

    private void DungeonSceneAwake()
    {
        // �ߺ� ����
        ManagerInit();

        CreatePlayer();

        // ���� ����
        GameManager.Data.EquipWeaponLoad();
        // �� ����
        GameManager.Data.EquipArmourLoad();
        // �� ����
        CreatePet();

        int quest1MonsterCnt = 7;
        // ����Ʈ ���� 7���� ����
        CreateQuestMonster("Velociraptor", quest1MonsterCnt);

        // BGM ����
        GameManager.Sound.BGMPlay("Sketch 3 - Against All Odds 1");

    }

    // �ߺ� �ڵ� ��ſ�
    public void ManagerInit()
    {
        // ����� �ؽ�Ʈ �Ŵ���
        GameManager.DamText.Init();
        // ������Ʈ �Ŵ��� Init();
        GameManager.Obj.Init();
        // ������Ʈ �Ŵ������� ���� ���� ����Ʈ �ʱ�ȭ
        GameManager.Obj.RemoveAllMobList();
        // Ui �ҷ���
        GameManager.Ui.Init();
        // Parse �Ŵ���
        GameManager.Parse.Init();

        // ���丮����� �ƴϸ� ������ �ε�
        if (GameManager.Scene._sceneNameEnum != SceneName.Tutorial)
        {
            // ������ �ε�
            GameManager.Data.LoadData_1(false);
        }

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

    }

    public void CreatePlayer()
    {
        // ������ġ�� �ʸ��� �ٸ��� �ؾ� ��
        _startPos = _startPosObject.transform.position;
        //�÷��̾� ����
        GameManager.Obj._playerController = GameManager.Create.CreatePlayerCharacter(_startPos, GameManager.Select._job.ToString());
        // ������ �ε�
        //GameManager.Data.LoadData(false);
        // �÷��̾� ���� �ε�
        GameManager.Data.UpdatePlayerStat();
        // ���� ����
        GameManager.Data.EquipWeaponLoad();
        // �� ����
        GameManager.Data.EquipArmourLoad();
    }

    public void CreateQuestMonster(string name, int count)
    {
        // ���� ���� �ڵ�
        for (int i = 0; i < count; i++)
        {
            // ���� ���� ��ġ
            _startPosMob[i] = _startPosMonster[i].transform.position;
            // ����Ʈ ���� ����
            MonsterControllerEX monster = GameManager.Create.CreateQuestMonster(_startPosMob[i], name);
            // ������ ���ڸ� �Ѿ��� (���� ������)
            monster.gameObject.name = monster.gameObject.name + "_" + i;
        }
    }

    public void CreatePet()
    {
        // �� ���� ��ġ
        _startPosPet = _startPosPetObj.transform.position;
        // �� ���� �ڵ�
        GameManager.Obj._petController = GameManager.Create.CreatePet(_startPosPet, GameManager.Select._pet.ToString());
    }
}
