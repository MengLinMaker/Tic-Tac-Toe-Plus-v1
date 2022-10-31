using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[Serializable] public class StringEvent : UnityEvent<String> { }



public class Preferences : MonoBehaviour
{
    public int height, width, numConsecutive, numPlayers;
    [SerializeField] StringEvent updateHeightInput;
    [SerializeField] StringEvent updateWidthInput;
    [SerializeField] StringEvent updateNumConsecutiveInput;
    [SerializeField] StringEvent updateNumPlayers;



    private void Start() {
        height = PlayerPrefs.GetInt("height", 3);
        width = PlayerPrefs.GetInt("width", 3);
        numConsecutive = PlayerPrefs.GetInt("numConsecutive", 3);
        numPlayers = PlayerPrefs.GetInt("numPlayers", 2);
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

    public void ValidateNumPlayers(String input) {
        int inputInt;
        if (int.TryParse(input, out inputInt)) {
            if (inputInt >= 2 && inputInt <= 4) {
                numPlayers = inputInt;
            }
        }
        UpdateInputs();
    }

    private void PrintAll() {
        print($"Height: {height}");
        print($"Width: {width}");
        print($"Num: {numConsecutive}");
        print($"Num: {numPlayers}");
    }

    private void UpdateInputs() {
        updateHeightInput?.Invoke($"{height}");
        updateWidthInput?.Invoke($"{width}");
        updateNumConsecutiveInput?.Invoke($"{numConsecutive}");
        updateNumPlayers?.Invoke($"{numPlayers}");

        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("numConsecutive", numConsecutive);
        PlayerPrefs.SetInt("numPlayers", numPlayers);
        PlayerPrefs.Save();
    }
}
