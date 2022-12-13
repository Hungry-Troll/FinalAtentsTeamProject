using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SelectManager
{
    // �÷��̾� �̸�
    public string _playerName;
    // ���� �̸� �� �̸� ����
    public string _jobName;
    public string _petName;
    // �κ��丮�� ���� �ִ� ������ ����Ʈ ����
    public List<string> _itemList;
    // �׿� �°� ����� ������ ����
    public Define.Job _job;
    public Define.Pet _pet;
    // ��ų ����Ʈ ����
    public int _skillPoint;

    // string���� ���ϰ� ���� ���������� �ϴ� ������ ��Ÿ�� ���̱� ����
    // ���� ���� ���۽� �����Ŵ������� �Ʒ� �Լ����� ������ � ĳ���Ϳ� ���� �����ߴ��� üũ ����

    public void Init()
    {
        // Select�Ŵ������� � ������ �����ߴ��� Ȯ��
        _job = GameManager.Select.SelectJobCheck();

        // Select�Ŵ������� � ���� �����ߴ��� Ȯ��
        _pet = GameManager.Select.SelectPelCheck();
    }

    // �÷��̾� üũ
    public Define.Job SelectJobCheck()
    {
        if(_jobName != null)
        {
            switch(_jobName)
            {
                case "Superhuman":
                    _job = Define.Job.Superhuman;
                    break;
                case "Cyborg":
                    _job = Define.Job.Cyborg;
                    break;
                case "Scientist":
                    _job = Define.Job.Scientist;
                    break;
            }
            return _job;
        }
        return _job = Job.None;
    }

    // �� üũ
    public Define.Pet SelectPelCheck()
    {
        if (_petName != null)
        {
            switch (_petName)
            {
                case "Triceratops":
                    _pet = Define.Pet.Triceratops;
                    break;
                case "Pachycephalosaurus":
                    _pet = Define.Pet.Pachycephalosaurus;
                    break;
                case "Brachiosaurus":
                    _pet = Define.Pet.Brachiosaurus;
                    break;
            }
            return _pet;
        }
        return _pet = Define.Pet.None;
    }
}
