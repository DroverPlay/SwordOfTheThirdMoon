using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayTime : MonoBehaviour
{

    [SerializeField] Gradient directionalLightGradient;
    [SerializeField] Gradient ambientLightGradient;

    [SerializeField, Range(1, 3600)] float timeDayInSeconds = 60;
    [SerializeField, Range(0f, 1f)] float timeProgress;

    [SerializeField] Light dirLight;
    [SerializeField] Transform sun;

    Vector3 defaultAngles;
    Vector3 defaultSun;

    void Start() {
        defaultAngles = dirLight.transform.localEulerAngles;
        defaultSun = sun.transform.localEulerAngles;
    }

    void FixedUpdate()
    {
        if (Application.isPlaying)
            timeProgress += Time.deltaTime / timeDayInSeconds;

        if (timeProgress > 1f)
            timeProgress = 0f;

        dirLight.color = directionalLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);


        dirLight.transform.localEulerAngles = new Vector3(x:360f * timeProgress - 90, y: defaultAngles.x, defaultAngles.z);
        sun.transform.localEulerAngles = new Vector3(x: 360f * timeProgress + 90, y: defaultSun.x, defaultSun.z);
    }
}
