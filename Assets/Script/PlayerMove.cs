using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("���ʳ]�w")]
    public float ���ʳt��;

    [Header("����j�w")]
    public KeyCode ����j�w = KeyCode.Space;

    [Header("�򥻳]�w")]
    public Transform ��v��;   // ��v��

    private float horizontalInput;   // ���k��V���䪺�ƭ�(-1 <= X <= +1)
    private float verticalInput;     // �W�U��V���䪺�ƭ�(-1 <= Y <= +1)

    private Vector3 ���ʤ�V;   // ���ʤ�V

    private Rigidbody ���a; // �Ĥ@�H�٪���(���n��)������

    private void Start()
    {
        ���a = GetComponent<Rigidbody>();
        ���a.freezeRotation = true;         // ��w�Ĥ@�H�٪���������A�������n��]���I�쪫��N����
    }

    private void Update()
    {
        MyInput(); 
        SpeedControl();
    }

    private void FixedUpdate()  //�T�w�ɶ�����@��
    {
        MovePlayer(); 
    }

    // ��k�G���o�ثe���a����V��W�U���k���ƭ�
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // �p�Ⲿ�ʤ�V(���N�O�p��X�b�PZ�b��Ӥ�V���O�q)
        ���ʤ�V = ��v��.forward * verticalInput + ��v��.right * horizontalInput;
        // ���ʲĤ@�H�٪���
        ���a.AddForce(���ʤ�V.normalized * ���ʳt�� * 10f, ForceMode.Force);
    }

    // ��k�G�����t�רô�t
    private void SpeedControl()
    {
        Vector3 �����t�� = new Vector3(���a.velocity.x, 0f, ���a.velocity.z); // ���o��X�b�PZ�b�������t��

        // �p�G�����t�פj��w�]�t�׭ȡA�N�N���󪺳t�׭��w��w�]�t�׭�
        if (�����t��.magnitude > ���ʳt��)
        {
            Vector3 �w�]�t�� = �����t��.normalized * ���ʳt��;
            ���a.velocity = new Vector3(�w�]�t��.x, ���a.velocity.y, �w�]�t��.z);
        }
    }
}