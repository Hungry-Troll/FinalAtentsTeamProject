using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 컨트롤러 상속받은 사이보그 컨트롤러

// 부모인 PlayerController의 MonoBehaviour(Awake, Start, Udate 등)를 사용하고 싶다면 
// -> 부모의 함수에 virtual 키워드 추가하고 자식의 함수에 override 키워드 붙여서 재정의 하면 된다.
// -> 참고로 부모 함수 호출은 base.함수명(); ((ex) base.Awake();)
// 자세한 내용) https://dragontory.tistory.com/307

public class CyborgController : PlayerController
{
    /*
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    */
}
