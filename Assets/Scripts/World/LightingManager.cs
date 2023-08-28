using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingPreset preset;

    [SerializeField, Range(8.0f, 19.0f)]
    public float timeOfDay;
    
    public float speed;


    private void Update() {
        if (preset == null) {
            return;
        }

        if (Application.isPlaying) {
            if (timeOfDay < 18.5f) {
                speed = 0.05f;
            } else {
                speed = 0.025f;
            }
            timeOfDay += Time.deltaTime * speed;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        else {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent) {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (directionalLight != null) {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }
    }

    private void OnValidate() {
        if (directionalLight != null) {
            return;
        }
        if (RenderSettings.sun != null) {
            directionalLight = RenderSettings.sun;
        }
        else {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in  lights) {
                if (light.type == LightType.Directional) {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}