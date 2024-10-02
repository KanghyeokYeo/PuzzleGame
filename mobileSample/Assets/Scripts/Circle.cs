using System.Collections;
using UnityEngine;

public class Circle : MonoBehaviour
{

    [SerializeField] bool isDrag;
    [SerializeField] Rigidbody2D rigid;
    public int level;
    [SerializeField] Animator anima;
    [SerializeField] bool isMerge;
    [SerializeField] CircleCollider2D circle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnEnable() // ��ũ��Ʈ Ȱ��ȭ�Ǿ��� �� ����Ǵ� �Լ�
    {
        anima.SetInteger("Level", level);
    }
    private void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //x�� ��輳��
            float leftBorder = -5.5f + transform.localScale.x;
            float rightBorder = +5.5f - transform.localScale.x;

            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.y = 8;
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }
    }

    public void Drag()
    {
        isDrag = true;
    }
    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    private void OnCollisionStay2D(Collision2D collision) //�浹�� �߻��ϴ� �Լ�
    {
        if (collision.gameObject.tag == "Circle") // ��Ŭ�� �浹���� ��
        {
            Circle other = collision.gameObject.GetComponent<Circle>(); // �浹�ϴ� ��� Circle

            if (level == other.level && !isMerge && !other.isMerge && level<7) // ���δٸ� Circle �浹�ϸ�, �պ������� ���� ��
            {
                // ���� ��� ��ġ ��������
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                if ((meY <= otherY) && meX > otherX)
                {
                    other.Hide(transform.position);
                }

            }
        }
    }

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }
    
    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;
        while (frameCount<20)
        {
            frameCount++;
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            yield return null;
        }
        isMerge = false;
        gameObject.SetActive(false);
    }

    void LevelUp()
    {
        isMerge = true ;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        anima.SetInteger("Level", level + 1);

        yield return new WaitForSeconds(0.3f);

        isMerge = false;
    }
}
