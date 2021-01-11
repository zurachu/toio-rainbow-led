using System.Collections.Generic;
using UnityEngine;
using toio;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private LicenseView licenseViewPrefab;

    private CubeManager cubeManager;

    private void Start()
    {
        cubeManager = new CubeManager();
    }

    public async void OnClickConnect()
    {
        var cube = await cubeManager.SingleConnect();
        if (cube == null)
        {
            return;
        }

        var colors = new List<Color>();
        colors.AddRange(Gradation(Color.red, Color.yellow, 4));
        colors.AddRange(Gradation(Color.yellow, Color.green, 4));
        colors.AddRange(Gradation(Color.green, Color.cyan, 4));
        colors.AddRange(Gradation(Color.cyan, Color.blue, 4));
        colors.AddRange(Gradation(Color.blue, Color.magenta, 4));
        colors.AddRange(Gradation(Color.magenta, Color.red, 4));

        cube.TurnOnLightWithScenario(0, colors.ConvertAll(_color => ToioLedUtility.LightOperationOf(_color, 100)).ToArray());
    }

    public void OnClickLicense()
    {
        Instantiate(licenseViewPrefab, canvas.transform);
    }

    private List<Color> Gradation(Color fromColor, Color toColor, int division)
    {
        var colors = new List<Color>();
        for (var i = 0; i < division; i++)
        {
            colors.Add(Color.Lerp(fromColor, toColor, (float)i / division));
        }

        return colors;
    }
}
