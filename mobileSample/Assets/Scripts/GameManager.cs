using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Circle lastCircle;
    [SerializeField] GameObject circlePrefab; //프리펩 구조물
    [SerializeField] Transform circlegroup;

    void Awake()
    {
        Application.targetFrameRate = 60; // 프레임 설정
    }

    void Start()
    {
        NextCircle(); // Circle 생성 초기화
    }

    Circle GetCircle() // 프리펩 Circle 스크립트와 불러오기, 생성위치 설정
    {
        GameObject instant = Instantiate(circlePrefab,circlegroup);
        Circle instantCircle = instant.GetComponent<Circle>();
        return instantCircle;
    }
    void NextCircle() // 새로운 Circle에 불러온 Circle 대입
    {
        Circle newCircle = GetCircle();
        lastCircle = newCircle;
        lastCircle.level = Random.Range(0, 8);
        lastCircle.gameObject.SetActive(true); // 오브젝트 활성화
        StartCoroutine(WaitNext());
    }

    // 코루틴 : 원하는 시점에 함수를 수행하는 것이 가능하도록 함
    // 마우스를 놓는 순간 새로운 Circle 생성하기
    IEnumerator WaitNext() 
    {
        while(lastCircle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
        //yield return null;한 프레임 쉬기


        NextCircle();
    }

    public void TouchDown()
    {

        if (lastCircle == null)
            return; // Circle이 없으면 종료 ->Start과정에서 Circle 생성됨
        lastCircle.Drag();
    }
    public void TouchUp()
    {
        if (lastCircle == null)
            return; // Circle이 없으면 종료 ->Start과정에서 Circle 생성됨
        lastCircle.Drop();
        lastCircle=null; // 다음 Circle을 위해 현재 컨트롤하는 Circle 자리 비워두기
    }
}
