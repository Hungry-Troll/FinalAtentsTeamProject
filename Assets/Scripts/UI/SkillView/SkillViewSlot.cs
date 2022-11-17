using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillViewSlot : MonoBehaviour
{
    public Image _uiImage;
    public RectTransform _rcTransfrom;
    Rect _rc;

    public Rect Rc
    {
        get
        {
            _rc.x = _rcTransfrom.position.x - _rcTransfrom.rect.width * 0.5f;
            _rc.y = _rcTransfrom.position.y + _rcTransfrom.rect.height * 0.5f;
            return _rc;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // x, y�� �»������ �ϴ� Rect ����ü
        _rc.x = _rcTransfrom.position.x - _rcTransfrom.rect.width * 0.5f;
        _rc.y = _rcTransfrom.position.y + _rcTransfrom.rect.height * 0.5f;
        _rc.xMin = _rc.x;
        _rc.yMin = _rc.y;
        _rc.xMax = _rc.x + _rcTransfrom.rect.width;
        _rc.yMax = _rc.y + _rcTransfrom.rect.height;
        _rc.width = _rcTransfrom.rect.width;
        _rc.height = _rcTransfrom.rect.height;
    }

    // �Ű������� ���޵� _pos�� rc�� ���ԵǴ��� �˻�
    public bool IsInRect(Vector2 _pos)
    {
        if (_pos.x >= Rc.x &&
            _pos.x <= Rc.x + Rc.width &&
            _pos.y >= Rc.y - Rc.height &&
            _pos.y <= Rc.y)
            return true;
        return false;
    }
}
