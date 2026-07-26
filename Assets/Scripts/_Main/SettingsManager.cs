using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resDropdown;

    private List<Resolution> resolutions;
    private List<string> resolutionOptions;
    private string currentOption;

    private void Start()
    {
        SetupResolutionOptions();
    }

    private void SetupResolutionOptions()
    {
        resolutions = Screen.resolutions.ToList<Resolution>();
        resolutionOptions = new();

        foreach(Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            resolutionOptions.Add(option);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
            {
                currentOption = option;
            }
        }

        resDropdown.ClearOptions();
        resDropdown.AddOptions(resolutionOptions);
        resDropdown.value = resolutionOptions.IndexOf(currentOption);
        resDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}