using System.Collections;
using UnityEngine;

public class GPS : MonoBehaviour
{
    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)        // Сначала проверяем, включена ли служба определения местоположения у пользователя
        {
            Debug.Log("GPS не включено блеать!");            // Напоминаем пользователю включить GPS
        }

        Input.location.Start();        // Запускаем службу перед запросом местоположения

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)        // Ждем, пока служба инициализируется
        {
            yield return new WaitForSeconds(1);
            maxWait--;

            Debug.Log("KU");
        }

        if (maxWait < 1)        // Служба не инициализировалась за 20 секунд
        {
            print("Тайм-аут");
        }

        if (Input.location.status == LocationServiceStatus.Failed)        // Соединение не удалось
        {
            print("Не удалось определить местоположение устройства");
        }
        // Доступ предоставлен, и значение местоположения можно получить
        else print("Местоположение: " + Input.location.lastData.latitude + ";" + Input.location.lastData.longitude + ";" + Input.location.lastData.altitude + ";" + Input.location.lastData.horizontalAccuracy + ";" + Input.location.lastData.timestamp);
        // Останавливаем службу, если нет необходимости непрерывно запрашивать обновления местоположения
        //Input.location.Stop();
    }
}
