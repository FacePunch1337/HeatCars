using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Photon.Pun;
using UnityEngine.UIElements;

public class TimerSystem : MonoBehaviourPun
{
    public TMP_Text _timer_txt;
    public TMP_Text winner;
    public GameObject leaveButton;
    public GameObject winnerBoard;
    
    public float _second;
    public int _minute;
    public bool _countdown = false;
    public bool timerStop = false;
    public bool timerStart = false;
    GameManager gameManager;
    

    void Start()
    {
        if(GameObject.Find("GameManager").TryGetComponent(out GameManager _gameManager)) gameManager = _gameManager;
        
        _timer_txt.text = _second.ToString();
        winnerBoard.SetActive(false);
        leaveButton.SetActive(false);
    } 
    
    public void FixedUpdate()
    {

        if (gameManager.modManager.startMod)
        {
            timerStart = true;
            if (_countdown)
            {
                if (_second < 1 && _minute < 1)
                {
                    
                    _timer_txt.text = $"0{_minute}:00";
                    
                    

                    
                        Finish();
                        timerStop = true;
                    


                    return;
                }
                else
                {
                    StartTimer();
                    
                }
            }
            /*else
            {
                _second += Time.deltaTime;
                _timer_txt.text = Convert.ToString($"0{_minute}:0{Mathf.Round(_second)}");

                if (_second > 59)
                {
                    _minute++;
                    _second = 1;
                }
                else if (Convert.ToInt32(_second) >= 10)
                    _timer_txt.text = Convert.ToString($"0{_minute}:{Mathf.Round(_second)}");
                else if (Convert.ToInt32(_second) < 10)
                    _timer_txt.text = Convert.ToString($"0{_minute}:0{Mathf.Round(_second)}");
            }*/

            
        }
        else if (!gameManager.modManager.startMod)
        {
            
            return;
        }
            
        
        


    }
    public void StartTimer()
    {
        
        _second -= Time.deltaTime;
        _timer_txt.text = $"0{_minute}:0{Mathf.Round(_second)}";

        if (_second == 0)
        {
            _minute--;
            _second = 59;
        }
        else if (_second >= 10)
            _timer_txt.text = $"0{_minute}:{Mathf.Round(_second)}";
        else if (_second < 10)
            _timer_txt.text = $"0{_minute}:0{Mathf.Round(_second)}";
    }

    /*public void Finish()
    {
            var car = GameObject.Find("Car(Clone)");
            GameObject.Find("KingTrigger").TryGetComponent(out KingTriggerScript kingTrigger);
            if (kingTrigger.kingName.text != string.Empty && kingTrigger.kingName.text != "?")
            {
                
                kingName.text = $"{kingTrigger.kingName.text}" + " " + "won";
                winnerBoard.SetActive(true);
                if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
                {
                    leaveButton.SetActive(true);
                    Cursor.visible = true;
                }
                    
                
                
            }
            else if (kingTrigger.kingName.text == string.Empty)
            {
               
                if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
                {
                    leaveButton.SetActive(true);
                    Cursor.visible = true;
                }


            }
           
            
            return;


    }*/

    public void Finish()
    {
        

        GameObject.Find("KingTrigger").TryGetComponent(out KingTriggerScript kingTrigger);
        if (kingTrigger.kingName.text != string.Empty && kingTrigger.kingName.text != "?")
        {
            
            winner.text = $"{kingTrigger.kingName.text}" + " " + "won";
            

            winnerBoard.SetActive(true);
            // if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
            // {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            leaveButton.SetActive(true);
            //  }
            return;


        }
        else if (kingTrigger.kingName.text == string.Empty)
        {

            //   if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
            //  {
            winnerBoard.SetActive(true);
            leaveButton.SetActive(true);
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
          //  }


        }


        return;
    }
}
