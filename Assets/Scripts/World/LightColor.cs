using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColor : MonoBehaviour
{
    IList<GameObject> lightObjects = new List<GameObject>();
    private GameObject globalLight;

    public Color dayColor;
    public Color nightColor;

    public float time = 12f;

    // Start is called before the first frame update
    void Start()
    {
        lightObjects = GameObject.FindGameObjectsWithTag("CycleLight");
        globalLight = GameObject.FindGameObjectWithTag("GlobalCycleLight");
    }

    // Update is called once per frame
    void Update()
    {
        time = (float)TimeManager.GetCurrentGameTime().hour + ((float)TimeManager.GetCurrentGameTime().minute / 60f);

        if (time > 24f)
        {
            time  -= (Mathf.Floor(time / 24) * 24);
        }

        foreach (GameObject light in lightObjects)
        {
            float intensity = (Mathf.Sin((time / 24 + 5) * 6)) / 2 + 0.5f;

            Color dayColorAdjusted = new Color(dayColor.r, dayColor.g, dayColor.b, dayColor.a) * intensity;
            Color nightColorAdjusted = new Color(nightColor.r, nightColor.g, nightColor.b, nightColor.a) * (1 - intensity);
            
            Color colorAvg = new Color((dayColorAdjusted.r + nightColorAdjusted.r) / 2, (dayColorAdjusted.g + nightColorAdjusted.g) / 2, (dayColorAdjusted.b + nightColorAdjusted.b) / 2, 1);

            light.GetComponent<Light2D>().color = colorAvg;

            //Vector3 colorRange = new Vector3(Mathf.Abs(dayColor.r - nightColor.r), Mathf.Abs(dayColor.g - nightColor.g), Mathf.Abs(dayColor.b - nightColor.b));
            Vector3 colorRange = new Vector3((dayColor.r - nightColor.r/2), (dayColor.g - nightColor.g/2), (dayColor.b - nightColor.b/2));

            Camera.main.backgroundColor = new Color(nightColor.r/2 + colorRange.x * intensity, nightColor.g/2 + colorRange.y * intensity, nightColor.b/2 + colorRange.z * intensity);

            light.GetComponent<Light2D>().intensity = 1.25f;

            //light.GetComponent<Light2D>().intensity = (Mathf.Sin((time/24 + 5) * 6))/2 + 0.5f;
        }
        //foreach (GameObject light in nightLightObjects)
        //{
        //    light.GetComponent<Light2D>().intensity = (-Mathf.Sin((time / 24 + 5) * 6)) / 2 + 0.5f;
        //}

        globalLight.GetComponent<Light2D>().intensity = Mathf.Clamp((Mathf.Sin((time / 24 + 5) * 6)) / 2 + 0.5f, 0.25f, 0.75f); 
    }
}
