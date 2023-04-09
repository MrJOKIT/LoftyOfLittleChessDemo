using System.Collections;
using System.Collections.Generic;
using Micosmo.SensorToolkit;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float speedMagnet;
    [SerializeField] private Sensor expSensor;

    // Update is called once per frame
    void Update()
    {
        ExpPick();
    }

    void ExpPick()
    {
        foreach (var exp in expSensor.Detections)
        {
            var distance = (exp.transform.position - transform.position).magnitude;
            if (distance >= 0.2f)
            {
                exp.transform.position = Vector2.Lerp(exp.transform.position, transform.position, Time.deltaTime * speedMagnet);
            }
            else
            {
                Destroy(exp.gameObject);
            }
        }
    }
}
