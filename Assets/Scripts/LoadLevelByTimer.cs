using UnityEngine;
using System.Collections;

public class LoadLevelByTimer : MonoBehaviour
{
    // ����� �� �������� ������
    public float delay = 3;
    // ��� ������������ ������
    public string levelName;
    // ���� IEnumerator �� ������. ���� System.Collections.
    // ��� ��������� �������� Start ��������� ����������
    public IEnumerator Start()
    {
        // �������� �� �������� ����� ������
        yield return new WaitForSeconds(delay);
        // �������� ������ � ��������� ������
        Application.LoadLevel(levelName);
    }
}