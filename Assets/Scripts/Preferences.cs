using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[Serializable] public class StringEvent : UnityEvent<String> { }



public class Preferences : MonoBehaviour
{
    public int height, width, numConsecutive;
    [SerializeField] StringEvent updateHeightInput;
    [SerializeField] StringEvent updateWidthInput;
    [SerializeField] StringEvent updateNumConsecutiveInput;



    private void Start() {
        height = PlayerPrefs.GetInt("height", 3);
        width = PlayerPrefs.GetInt("width", 3);
        numConsecutive = PlayerPrefs.GetInt("numConsecutive", 3);
        UpdateInputs();
    }

    public void ValidateHeight(String input) {
        int inputInt;
        if (int.TryParse(input, out inputInt)) {
            if (inputInt >=3 && inputInt <= 9) {
                height = inputInt;
                if (inputInt < numConsecutive) {
                    numConsecutive = inputInt;
                }
            }
        }
        UpdateInputs();
    }

    public void ValidateWidth(String input) {
        int inputInt;
        if (int.TryParse(input, out inputInt)) {
            if (inputInt >=3 && inputInt <= 9) {
                width = inputInt;
                if (inputInt < numConsecutive) {
                    numConsecutive = inputInt;
                }
            }
        }
        UpdateInputs();
    }

    public void ValidateNumConsecutive(String input) {
        int inputInt;
        if (int.TryParse(input, out inputInt)) {
            if (inputInt >= 3 && inputInt <= height && inputInt <= width) {
                numConsecutive = inputInt;
            }
        }
        UpdateInputs();
    }

    private void PrintAll() {
        print($"Height: {height}");
        print($"Width: {width}");
        print($"Num: {numConsecutive}");
    }

    private void UpdateInputs() {
        updateHeightInput?.Invoke($"{height}");
        updateWidthInput?.Invoke($"{width}");
        updateNumConsecutiveInput?.Invoke($"{numConsecutive}");

        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("numConsecutive", numConsecutive);
        PlayerPrefs.Save();
    }
}
