using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarSelect : MonoBehaviour
{
    private int current_car;

    [SerializeField] GameObject[] cars;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        current_car = 0;
        PlayerPrefs.SetInt("SelectedCar", current_car);

    }

    public void NextButton()
    {

        if (current_car == cars.Length - 1)
        {
            cars[current_car].SetActive(false);
            current_car = 0;
            cars[current_car].SetActive(true);
           
        }
        else
        {
            cars[current_car].SetActive(false);
            current_car++;
            cars[current_car].SetActive(true);
           
        }

        PlayerPrefs.SetInt("SelectedCar", current_car);
    }

    public void PrevButton()
    {
        if (current_car == 0)
        {
            cars[current_car].SetActive(false);
            current_car = cars.Length - 1;
            cars[current_car].SetActive(true);
            Debug.Log(current_car);
        }
        else
        {
            cars[current_car].SetActive(false);
            current_car--;
            cars[current_car].SetActive(true);
            Debug.Log(current_car);
        }
        PlayerPrefs.SetInt("SelectedCar", current_car);
    }
    
}
