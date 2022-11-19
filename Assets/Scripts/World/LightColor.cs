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
            time -= 24f;
        else if (time < 0)
            time += 24f;

        foreach (GameObject light in lightObjects)
        {
            float intensity = (Mathf.Sin((time / 24 + 5) * 6)) / 2 + 0.5f;

            Color dayColorAdjusted = new Color(dayColor.r, dayColor.g, dayColor.b, dayColor.a) * intensity;
            Color nightColorAdjusted = new Color(nightColor.r, nightColor.g, nightColor.b, nightColor.a) * (1 - intensity);
            
            Color colorAvg = new Color((dayColorAdjusted.r + nightColorAdjusted.r) / 2, (dayColorAdjusted.g + nightColorAdjusted.g) / 2, (dayColorAdjusted.b + nightColorAdjusted.b) / 2, 1);

            light.GetComponent<Light2D>().color = colorAvg;

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
