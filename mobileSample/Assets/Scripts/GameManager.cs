using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Circle lastCircle;
    [SerializeField] GameObject circlePrefab; //������ ������
    [SerializeField] Transform circlegroup;

    void Awake()
    {
        Application.targetFrameRate = 60; // ������ ����
    }

    void Start()
    {
        NextCircle(); // Circle ���� �ʱ�ȭ
    }

    Circle GetCircle() // ������ Circle ��ũ��Ʈ�� �ҷ�����, ������ġ ����
    {
        GameObject instant = Instantiate(circlePrefab,circlegroup);
        Circle instantCircle = instant.GetComponent<Circle>();
        return instantCircle;
    }
    void NextCircle() // ���ο� Circle�� �ҷ��� Circle ����
    {
        Circle newCircle = GetCircle();
        lastCircle = newCircle;
        lastCircle.level = Random.Range(0, 8);
        lastCircle.gameObject.SetActive(true); // ������Ʈ Ȱ��ȭ
        StartCoroutine(WaitNext());
    }

    // �ڷ�ƾ : ���ϴ� ������ �Լ��� �����ϴ� ���� �����ϵ��� ��
    // ���콺�� ���� ���� ���ο� Circle �����ϱ�
    IEnumerator WaitNext() 
    {
        while(lastCircle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
        //yield return null;�� ������ ����


        NextCircle();
    }

    public void TouchDown()
    {

        if (lastCircle == null)
            return; // Circle�� ������ ���� ->Start�������� Circle ������
        lastCircle.Drag();
    }
    public void TouchUp()
    {
        if (lastCircle == null)
            return; // Circle�� ������ ���� ->Start�������� Circle ������
        lastCircle.Drop();
        lastCircle=null; // ���� Circle�� ���� ���� ��Ʈ���ϴ� Circle �ڸ� ����α�
    }
}
