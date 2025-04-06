using System.Collections;
using UnityEngine;

public class GPS : MonoBehaviour
{
    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)        // ������� ���������, �������� �� ������ ����������� �������������� � ������������
        {
            Debug.Log("GPS �� �������� ������!");            // ���������� ������������ �������� GPS
        }

        Input.location.Start();        // ��������� ������ ����� �������� ��������������

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)        // ����, ���� ������ ����������������
        {
            yield return new WaitForSeconds(1);
            maxWait--;

            Debug.Log("KU");
        }

        if (maxWait < 1)        // ������ �� ������������������ �� 20 ������
        {
            print("����-���");
        }

        if (Input.location.status == LocationServiceStatus.Failed)        // ���������� �� �������
        {
            print("�� ������� ���������� �������������� ����������");
        }
        // ������ ������������, � �������� �������������� ����� ��������
        else print("��������������: " + Input.location.lastData.latitude + ";" + Input.location.lastData.longitude + ";" + Input.location.lastData.altitude + ";" + Input.location.lastData.horizontalAccuracy + ";" + Input.location.lastData.timestamp);
        // ������������� ������, ���� ��� ������������� ���������� ����������� ���������� ��������������
        //Input.location.Stop();
    }
}
