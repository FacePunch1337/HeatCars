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
   
    private Vector3 carModelPos;
    private Quaternion carModelRot;
    private Vector3 yPos;


    private void Start()
    {
        
        //PhotonNetwork.ConnectUsingSettings();
        current_car = 0;
        PlayerPrefs.SetInt("SelectedCar", current_car);


        carModelPos = cars[current_car].transform.position;
        carModelPos.y += 1;
        carModelRot = cars[current_car].transform.rotation;
        

      
    }

    public void NextButton()
    {
        carModelPos = cars[current_car].transform.position;
        //cars[current_car].transform.position = new Vector3(carSelectPoint.transform.position.x, carSelectPoint.transform.position.y, carSelectPoint.transform.position.z);
        cars[current_car].transform.position = new Vector3(carModelPos.x, carModelPos.y + 1, carModelPos.z);
        cars[current_car].transform.rotation = new Quaternion(carModelRot.x, carModelRot.y, carModelRot.z, carModelRot.w);


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
        carModelPos = cars[current_car].transform.position;
        cars[current_car].transform.position = new Vector3(carModelPos.x, carModelPos.y + 1, carModelPos.z);
        cars[current_car].transform.rotation = new Quaternion(carModelRot.x, carModelRot.y, carModelRot.z, carModelRot.w);

        if (current_car == 0)
        {
            cars[current_car].SetActive(false);
            current_car = cars.Length - 1;
            cars[current_car].SetActive(true);
            
        }
        else
        {
            cars[current_car].SetActive(false);
            current_car--;
            cars[current_car].SetActive(true);
            
        }
        PlayerPrefs.SetInt("SelectedCar", current_car);
    }
    
}
