using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

// 무기 착용 관련 기능 매니저
public class WeaponManager
{
    // 인벤토리에서 들고있는 _weapon은 스텟 계산용
    // Weapon매니저에서 들고있는 _weapon은 실제로 들고 있는 무기 게임오브젝트
    // 두 변수를 통합할지는 추후 고민
    public GameObject _weapon = null;

    // 무기 착용 함수 // 무기 방향이 돌아가는 버그가 있음 추후 해결
/*    public void EquipWeapon(string weaponName, Transform equipWeaponPos)
    {
        // 우선은 쉽게 만들고 추후 오브젝트 풀링 구현 고민
        GameObject temWeapon = GameManager.Resource.GetEquipItem(weaponName);
        // 무기 방향이 돌아가는 버그 해결용
        Quaternion temRotate = temWeapon.transform.localRotation;
        if(temWeapon != null)
        {
            // 착용하고 있는 무기가 없다면(웨폰매니저 무기변수기준)
            if(_weapon == null)
            { 
                // 무기 착용
                _weapon = GameObject.Instantiate<GameObject>(temWeapon, equipWeaponPos.position, Quaternion.identity);
                // 찾은 위치에 자식으로 넣어줌
                _weapon.transform.SetParent(equipWeaponPos);
                // 무기 방향이 돌아가는 버그 해결용
                _weapon.transform.rotation = temRotate;
            }
            // 착용하고 있는 무기가 있다면(웨폰매니저 무기변수기준)
            else
            {
                // 기존 무기를 찾아서 파괴
                string unEquipWeapon = _weapon.name;
                Transform unEquipWeaponPos = Util.FindChild(unEquipWeapon, GameManager.Obj._playerController.transform);
                GameObject.Destroy(unEquipWeaponPos.gameObject);
                // 무기 착용
                _weapon = GameObject.Instantiate<GameObject>(temWeapon, equipWeaponPos.position, Quaternion.identity);
                // 찾은 위치에 자식으로 넣어줌
                _weapon.transform.SetParent(equipWeaponPos);
                // 무기 방향이 돌아가는 버그 해결용
                _weapon.transform.rotation = temRotate;
            }
        }
    }*/

    // 무기 착용 함수 임시 (캐릭터 상태창 무기도 여기서 해결)
    public void TempEquipWeapon(string weaponName, Transform playerTransform)
    {
        // 무기 위치 찾고
        Transform findPos = Util.FindChild(weaponName, playerTransform);
        // 캐릭터 상태창 무기 위치 찾기
        Transform findTemp = Util.FindChild(weaponName, GameManager.Ui._statePlayerObj.transform);

        if (findPos == null)
        {
            // 무기를 착용할 수 없습니다 UI구현
            Debug.Log("착용할수 없는 무기입니다.");
            return;
        }
        // 웨폰 무기 변수가 널이면 무기가 없는것이므로
        if(_weapon == null)
        {
            // 무기 생성
            findPos.gameObject.SetActive(true);
            _weapon = findPos.gameObject;

            // 캐릭터 상태창에도 무기 생성
            findTemp.gameObject.SetActive(true);
        }
        // 무기 착용 중이면
        else
        {
            // 착용중인 무기를 찾아서 비활성화
            string unEquipWeapon = _weapon.name;
            Transform unEquipWeaponPos = Util.FindChild(unEquipWeapon, playerTransform);
            // 캐릭터 상태창 무기 위치 찾기
            Transform unEquipWeaponTemp = Util.FindChild(unEquipWeapon, GameManager.Ui._statePlayerObj.transform);

            // 무기 비활성화
            unEquipWeaponPos.gameObject.SetActive(false);
            // 캐릭터 상태창 무기 비활성화
            unEquipWeaponTemp.gameObject.SetActive(false);
            _weapon = null;

            // 무기 생성
            findPos.gameObject.SetActive(true);
            // 캐릭터 상태창 무기 생성
            findTemp.gameObject.SetActive(true);
            _weapon = findPos.gameObject;
        }
    }

    // 무기 해제 함수
    public void TempUnEquipWeapon(string weaponName, Transform playerTransform)
    {
        // 무기 위치 찾고
        Transform findPos = Util.FindChild(weaponName, playerTransform);
        // 캐릭터 상태창 무기 위치 찾기
        Transform findTemp = Util.FindChild(weaponName, GameManager.Ui._statePlayerObj.transform);

        // 무기 비활성화
        findPos.gameObject.SetActive(false);
        // 캐릭터 상태창 무기 비활성화
        findTemp.gameObject.SetActive(false);
        _weapon = null;
    }
}



