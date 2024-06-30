using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    public float 移動速度;

    [Header("按鍵綁定")]
    public KeyCode 按鍵綁定 = KeyCode.Space;

    [Header("基本設定")]
    public Transform 攝影機;   // 攝影機

    private float horizontalInput;   // 左右方向按鍵的數值(-1 <= X <= +1)
    private float verticalInput;     // 上下方向按鍵的數值(-1 <= Y <= +1)

    private Vector3 移動方向;   // 移動方向

    private Rigidbody 玩家; // 第一人稱物件(膠囊體)的剛體

    private void Start()
    {
        玩家 = GetComponent<Rigidbody>();
        玩家.freezeRotation = true;         // 鎖定第一人稱物件剛體旋轉，不讓膠囊體因為碰到物件就亂轉
    }

    private void Update()
    {
        MyInput(); 
        SpeedControl();
    }

    private void FixedUpdate()  //固定時間執行一次
    {
        MovePlayer(); 
    }

    // 方法：取得目前玩家按方向鍵上下左右的數值
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // 計算移動方向(其實就是計算X軸與Z軸兩個方向的力量)
        移動方向 = 攝影機.forward * verticalInput + 攝影機.right * horizontalInput;
        // 推動第一人稱物件
        玩家.AddForce(移動方向.normalized * 移動速度 * 10f, ForceMode.Force);
    }

    // 方法：偵測速度並減速
    private void SpeedControl()
    {
        Vector3 平面速度 = new Vector3(玩家.velocity.x, 0f, 玩家.velocity.z); // 取得僅X軸與Z軸的平面速度

        // 如果平面速度大於預設速度值，就將物件的速度限定於預設速度值
        if (平面速度.magnitude > 移動速度)
        {
            Vector3 預設速度 = 平面速度.normalized * 移動速度;
            玩家.velocity = new Vector3(預設速度.x, 玩家.velocity.y, 預設速度.z);
        }
    }
}