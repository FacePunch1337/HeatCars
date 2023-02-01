using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ModManager : MonoBehaviourPun
{
    // Start is called before the first frame update

    [SerializeField] public GameObject[] spawnPoints;

    private bool _startMod;
    private bool _stopMod;
    private bool _teleportToMod;
    private bool _teleportToSpawn;
    

    private SpawnPlayers _spawner;
    private ModManager _modManager;
    private Trigger _trigger;
    private KingTriggerScript kingTrigger;

    public bool startMod { get { return _startMod; } set { _startMod = value; } }
    public bool stopMod { get { return _stopMod; } set { _stopMod = value; } }
    public bool teleportToMod { get { return _teleportToMod; } set { _teleportToMod = value; } }
    public bool teleportToSpawn { get { return _teleportToSpawn; } set { _teleportToSpawn = value; } }

    public SpawnPlayers spawner { get { return _spawner; } set { _spawner = value; } }
    public ModManager modManager { get { return _modManager; } set { _modManager = value; } }
    public Trigger trigger { get { return _trigger; } set { _trigger = value; } }


    [SerializeField] private GameObject timerGameObject;
    [SerializeField] private GameObject winnerBoard;
    [SerializeField] private GameObject leaveModeButton;
   

    //public bool modStop = false;
    private void Start()
    {
        gameObject.TryGetComponent(out ModManager _modManager);
        gameObject.TryGetComponent(out TimerSystem _timerSystem);
        GameObject.Find("Trigger").TryGetComponent(out Trigger _trigger);
        modManager = _modManager;
        trigger = _trigger;
        spawner = gameObject.GetComponent<SpawnPlayers>();
        leaveModeButton.SetActive(false);
        timerGameObject.SetActive(false);
        winnerBoard.SetActive(false);


    }
    public void SendStartMod()
    {

        if (trigger.readyCount == PhotonNetwork.PlayerList.Length)
        {

            modManager.startMod = true;
            modManager.teleportToMod = true;
            bool[] _params = { modManager.startMod, modManager.teleportToMod };
            photonView.RPC("StartModeKOB", RpcTarget.All, _params);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    [PunRPC]
    public void StartModeKOB(bool[] _params)
    {
        
        timerGameObject.GetComponent<Timer>().StartTimer();
        modManager.startMod = _params[0];
        modManager.teleportToMod = _params[1];
        timerGameObject.SetActive(true);
        


    }

    public void StopMode()
    {
        
            GameObject.Find("KingTrigger").TryGetComponent(out KingTriggerScript _kingTrigger);
            kingTrigger = _kingTrigger;
            if (kingTrigger.kingName.text != string.Empty && kingTrigger.kingName.text != "?")
            {

                winnerBoard.GetComponent<TMP_Text>().text = $"{kingTrigger.kingName.text}" + " " + "won";
                
                // if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
                // {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            //  }

                winnerBoard.SetActive(true);
                leaveModeButton.SetActive(true);

            }
            else if(kingTrigger.kingName.text == string.Empty)
            {
                winnerBoard.GetComponent<TMP_Text>().text = "LOL";
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            
                winnerBoard.SetActive(true);
                leaveModeButton.SetActive(true);
            }
            
       
       
    }
    public void SendEndGameMode()
    {

      
        modManager.startMod = false;
        modManager.teleportToSpawn = true;
        bool[] _params = {modManager.startMod, modManager.teleportToSpawn };
        photonView.RPC("EndGameMode", RpcTarget.All, _params);
        leaveModeButton.SetActive(false);

        PhotonNetwork.CurrentRoom.IsOpen = true;
        
       
    }

    [PunRPC]
    public void EndGameMode(bool[] _params)
    {
        kingTrigger.kingName.text = string.Empty;
        timerGameObject.SetActive(false);
        winnerBoard.SetActive(false);
        modManager.startMod = _params[0];
        modManager.teleportToSpawn = _params[1];
    }
}
