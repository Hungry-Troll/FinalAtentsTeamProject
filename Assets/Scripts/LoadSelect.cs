using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSelect : MonoBehaviour
{
    //비어있는 슬롯을 선택했을 때 나오는 이름 입력란
    public GameObject creat;
    public Text[] slotText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int number)
    {
        //DataManager.instance.selectedSlot = number;
        GameManager.Data.selectedSlot = number;
        Creat();

        //DataManager.instance.LoadData();
        GameManager.Data.LoadData_1(true);
        StartGame();
    }

    //이름 입력란 활성화 함수
    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}
