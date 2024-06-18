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

    [SerializeField]
    private Camera mainCamera;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�Փ˂���");
    }
    private void OnCollisionStay(Collision collision)
    {
        //�Փ˂��Ă���_�̏�񂪕����i�[����Ă���
        ContactPoint[] contacts = collision.contacts;
        //0�Ԗڂ̏Փˏ�񂩂�A�Փ˂��Ă���_�̖@�����擾
        Vector3 otherNormal = contacts[0].normal;
        //������������x�N�g���B
        Vector3 upVector = new Vector3(0,1,0);
        //������Ɩ@���̓��ρB��̃x�N�g���͂Ƃ��ɒ������P�Ȃ̂ŁAcos0�̌��ʂ�dotUN�ϐ��Ɋi�[
        float dotUN = Vector3.Dot(upVector, otherNormal);
        //���ϒl�ɋt�O�p�`arccos���|���Ċp�x���Z�o�B�����x���@�֕ϊ�����
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        //��̃x�N�g�����Ȃ��p�x��45�x��菬������΍ĂуW�����v�\�Ƃ���
        if(dotDeg <= 45)
        {
            isCanJump = true;
        }
        
        //Debug.Log("�ڐG��");
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
        if (2 < transform.position.y - mainCamera.gameObject.GetComponent<Transform>().position.y)
        {
            float y = 1 * Time.deltaTime;
            mainCamera.gameObject.transform.Translate(new Vector3(0, y, 0));
        }
        if (-2 > transform.position.y - mainCamera.gameObject.GetComponent<Transform>().position.y)
        {
            float y = -1 * Time.deltaTime;
            mainCamera.gameObject.transform.Translate(new Vector3(0, y, 0));
        }
    }
}
