using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SelectManager
{
    // 플레이어 이름
    public string _playerName;
    // 직업 이름 펫 이름 변수
    public string _jobName;
    public string _petName;
    // 그에 맞게 사용할 디파인 변수
    public Define.Job _job;
    public Define.Pet _pet;

    // string으로 안하고 굳이 디파인으로 하는 이유는 오타를 줄이기 위함
    // 추후 게임 시작시 셀렉매니저에서 아래 함수들을 돌려서 어떤 캐릭터와 펫을 선택했는지 체크 가능

    // 플레이어 체크
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

    // 펫 체크
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
