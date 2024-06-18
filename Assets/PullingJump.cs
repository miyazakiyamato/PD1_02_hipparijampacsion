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
        Debug.Log("衝突した");
    }
    private void OnCollisionStay(Collision collision)
    {
        //衝突している点の情報が複数格納されている
        ContactPoint[] contacts = collision.contacts;
        //0番目の衝突情報から、衝突している点の法線を取得
        Vector3 otherNormal = contacts[0].normal;
        //上方向を示すベクトル。
        Vector3 upVector = new Vector3(0,1,0);
        //上方向と法線の内積。二つのベクトルはともに長さが１なので、cos0の結果がdotUN変数に格納
        float dotUN = Vector3.Dot(upVector, otherNormal);
        //内積値に逆三角形arccosを掛けて角度を算出。それを度数法へ変換する
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        //二つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする
        if(dotDeg <= 45)
        {
            isCanJump = true;
        }
        
        //Debug.Log("接触中");
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("離脱した");
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
            //クリックした座標と離した座標の差分を取得
            Vector3 dist = clickPosition - Input.mousePosition;
            //クリックとリリースが同じ座標ならば無視
            if(dist.sqrMagnitude == 0) { return; }
            //差分を標準化し、jumpPowerをかけ合わせた値を移動量とする。
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
