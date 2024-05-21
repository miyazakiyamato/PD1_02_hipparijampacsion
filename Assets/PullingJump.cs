using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingJump : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 clickPosition;
    [SerializeField]
    private float jumpPower = 10;
    private bool isCanJump;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�Փ˂���");
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("�ڐG��");
        isCanJump = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("���E����");
        isCanJump = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 10, 0);
        }
        if(Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
        }
        if (isCanJump && Input.GetMouseButtonUp(0))
        {
            //�N���b�N�������W�Ɨ��������W�̍������擾
            Vector3 dist = clickPosition - Input.mousePosition;
            //�N���b�N�ƃ����[�X���������W�Ȃ�Ζ���
            if(dist.sqrMagnitude == 0) { return; }
            //������W�������AjumpPower���������킹���l���ړ��ʂƂ���B
            rb.velocity = dist.normalized * jumpPower;
        }
    }
}
