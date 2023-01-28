using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// ������� ������ ��������� �� ������� ��������� CharacterController
// � ������� ����� ���������� ����� ����������� ��������
[RequireComponent(typeof(CharacterController))]
public class Player2 : MonoBehaviour
{
    // �������� ����������� - 6 ������ � ������� �� ���������
    // � ��������� ����� ��������
    public float speed = 10;
    // ���������� �������� �������� 60 �������� � ������� �� ���������
    public float rotationSpeed = 160;
    // ��������� ���������� ��� �������� ������ �� ��������� CharacterController
    private CharacterController _controller;
    public UnityEvent OnEat;
    public void Start()
    {
        // �������� ��������� CharacterController � 
        // ���������� ��� � ��������� ����������
        _controller = GetComponent<CharacterController>();
        // ������� �����
        // current - ������� ���� �������� ������, �������� � ������
        Transform current = transform;
        for (int i = 0; i < 3; i++)
        {
            // ������� �������� ��� � ��������� ��� ��������� Tail
            Tail tail = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Tail>();
            // �������� "�����" �� "��������"
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            // ���������� ������ ��� ���������� �������
            tail.transform.rotation = transform.rotation;
            // ������� ������ ������ ��������� �� ��������, ������� �������� ��� ������ �� ���
            tail.target = current.transform;
            // ��������� ����� ���������� ������ - 2 �������
            tail.targetDistance = 2;
            // ������� � ������ ���������, ��� ��� �� �� �����
            //Destroy(tail.collider);
            Destroy(tail.GetComponent<Collider>());
            // ��������� �������� ����� ������������� ������� ������
            current = tail.transform;
        }
    }
    private bool _testing = false;
    public void Update()
    {
        /* 
        * ������ ������ - ������������ ���
        * Unity ����� ����� ����������������� ����, ������� ����� ������������
        * ��������� ��� ����� �������� ��� �� ���������� (������� � WSAD), ��� � �� ��������
        */
        // �������� �������� ������������ ��� �����
        /* float vertical = Input.GetAxis("Vertical"); */
        // �������� �������� �������������� ��� �����
        float horizontal = Input.GetAxis("Horizontal");

        // ������� ��������� ������ ��� Y 
        transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);
        // �������� ��������� � ������� ����������� � �������, ���� ������� ��������� ������
        // ������� ���� ���������
        _controller.Move(transform.forward * speed * Time.deltaTime /* * vertical*/);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(5);
        }
    }
    GameObject food;
    // � ������ ������� ����� ������������ ��� �������, � ��������
    // CharacterController �������� � ������������
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Food")
        {
            // ���������� ���� ��� � ������ ����� �����
            Game.points += 10;
            //��������� � ���, "�������" �� � ������� ����� � �������� ����. 
            //�� ����� ���� ���������� ��� � Random ���������
            food = GameObject.Find("Food");
            //Destroy(food);
            var pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            food.transform.position = pos;
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
        }
        else
        {
            //��������� �� � ���
            Application.LoadLevel("GameOver");
        }
    }
}