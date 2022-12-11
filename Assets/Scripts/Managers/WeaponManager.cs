using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

// ���� ���� ���� ��� �Ŵ���
public class WeaponManager
{
    // �κ��丮���� ����ִ� _weapon�� ���� ����
    // Weapon�Ŵ������� ����ִ� _weapon�� ������ ��� �ִ� ���� ���ӿ�����Ʈ
    // �� ������ ���������� ���� ���
    public GameObject _weapon = null;

    // ���� ���� �Լ� // ���� ������ ���ư��� ���װ� ���� ���� �ذ�
/*    public void EquipWeapon(string weaponName, Transform equipWeaponPos)
    {
        // �켱�� ���� ����� ���� ������Ʈ Ǯ�� ���� ���
        GameObject temWeapon = GameManager.Resource.GetEquipItem(weaponName);
        // ���� ������ ���ư��� ���� �ذ��
        Quaternion temRotate = temWeapon.transform.localRotation;
        if(temWeapon != null)
        {
            // �����ϰ� �ִ� ���Ⱑ ���ٸ�(�����Ŵ��� ���⺯������)
            if(_weapon == null)
            { 
                // ���� ����
                _weapon = GameObject.Instantiate<GameObject>(temWeapon, equipWeaponPos.position, Quaternion.identity);
                // ã�� ��ġ�� �ڽ����� �־���
                _weapon.transform.SetParent(equipWeaponPos);
                // ���� ������ ���ư��� ���� �ذ��
                _weapon.transform.rotation = temRotate;
            }
            // �����ϰ� �ִ� ���Ⱑ �ִٸ�(�����Ŵ��� ���⺯������)
            else
            {
                // ���� ���⸦ ã�Ƽ� �ı�
                string unEquipWeapon = _weapon.name;
                Transform unEquipWeaponPos = Util.FindChild(unEquipWeapon, GameManager.Obj._playerController.transform);
                GameObject.Destroy(unEquipWeaponPos.gameObject);
                // ���� ����
                _weapon = GameObject.Instantiate<GameObject>(temWeapon, equipWeaponPos.position, Quaternion.identity);
                // ã�� ��ġ�� �ڽ����� �־���
                _weapon.transform.SetParent(equipWeaponPos);
                // ���� ������ ���ư��� ���� �ذ��
                _weapon.transform.rotation = temRotate;
            }
        }
    }*/

    // ���� ���� �Լ� �ӽ� (ĳ���� ����â ���⵵ ���⼭ �ذ�)
    public void TempEquipWeapon(string weaponName, Transform playerTransform)
    {
        // ���� ��ġ ã��
        Transform findPos = Util.FindChild(weaponName, playerTransform);
        // ĳ���� ����â ���� ��ġ ã��
        Transform findTemp = Util.FindChild(weaponName, GameManager.Ui._statePlayerObj.transform);

        if (findPos == null)
        {
            // ���⸦ ������ �� �����ϴ� UI����
            Debug.Log("�����Ҽ� ���� �����Դϴ�.");
            return;
        }
        // ���� ���� ������ ���̸� ���Ⱑ ���°��̹Ƿ�
        if(_weapon == null)
        {
            // ���� ����
            findPos.gameObject.SetActive(true);
            _weapon = findPos.gameObject;

            // ĳ���� ����â���� ���� ����
            findTemp.gameObject.SetActive(true);
        }
        // ���� ���� ���̸�
        else
        {
            // �������� ���⸦ ã�Ƽ� ��Ȱ��ȭ
            string unEquipWeapon = _weapon.name;
            Transform unEquipWeaponPos = Util.FindChild(unEquipWeapon, playerTransform);
            // ĳ���� ����â ���� ��ġ ã��
            Transform unEquipWeaponTemp = Util.FindChild(unEquipWeapon, GameManager.Ui._statePlayerObj.transform);

            // ���� ��Ȱ��ȭ
            unEquipWeaponPos.gameObject.SetActive(false);
            // ĳ���� ����â ���� ��Ȱ��ȭ
            unEquipWeaponTemp.gameObject.SetActive(false);
            _weapon = null;

            // ���� ����
            findPos.gameObject.SetActive(true);
            // ĳ���� ����â ���� ����
            findTemp.gameObject.SetActive(true);
            _weapon = findPos.gameObject;
        }
    }

    // ���� ���� �Լ�
    public void TempUnEquipWeapon(string weaponName, Transform playerTransform)
    {
        // ���� ��ġ ã��
        Transform findPos = Util.FindChild(weaponName, playerTransform);
        // ĳ���� ����â ���� ��ġ ã��
        Transform findTemp = Util.FindChild(weaponName, GameManager.Ui._statePlayerObj.transform);

        // ���� ��Ȱ��ȭ
        findPos.gameObject.SetActive(false);
        // ĳ���� ����â ���� ��Ȱ��ȭ
        findTemp.gameObject.SetActive(false);
        _weapon = null;
    }
}



