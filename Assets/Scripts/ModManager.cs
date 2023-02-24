using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ModManager : MonoBehaviourPun
{
    // Start is called before the first frame update

    [SerializeField] public GameObject[] spawnPointsKOB;
    [SerializeField] public GameObject[] spawnPointsRace;
    

    private bool _startMod;
    private bool _stopMod;
    private bool _teleportToMod;
    private bool _teleportToSpawn;
    

    private SpawnPlayers _spawner;
    private ModManager _modManager;
    private Trigger _trigger;
    private KingTriggerScript kingTrigger;
    public GameObject modPanel;
    public GameObject buttonStart;
    public Mod mod;
    public enum Mod { KOB, Race, None};
   


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
        
        GameObject.Find("Trigger").TryGetComponent(out Trigger _trigger);
        modManager = _modManager;
        trigger = _trigger;
        spawner = gameObject.GetComponent<SpawnPlayers>();
        leaveModeButton.SetActive(false);
        timerGameObject.SetActive(false);
        winnerBoard.SetActive(false);
        modPanel.SetActive(false);
        buttonStart.SetActive(false);
        modManager.buttonStart.SetActive(false);
        mod = Mod.None;

    }
    public void SendStartMod()
    {
       
        if (trigger.readyCount == PhotonNetwork.PlayerList.Length)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            modManager.startMod = true;
            modManager.teleportToMod = true;
            bool[] _params = { modManager.startMod, modManager.teleportToMod };
            photonView.RPC("StartModeKOB", RpcTarget.All, _params);
            

        }
    }

    [PunRPC]
    public void StartModeKOB(bool[] _params)
    {
       
        timerGameObject.GetComponent<Timer>().StartTimer();
        modManager.startMod = _params[0];
        modManager.teleportToMod = _params[1];
        timerGameObject.SetActive(true);
       
        // PhotonNetwork.CurrentRoom.IsOpen = false;



    }

    

    public void SendStopModeKOB()
    {
        
        photonView.RPC("StopModeKOB", RpcTarget.All);






    }

    [PunRPC]

    public void StopModeKOB()
    {
        if (photonView.Owner.IsMasterClient && photonView.AmOwner)
        {
            leaveModeButton.SetActive(true);
        }
        
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
            

        }
        else if(kingTrigger.kingName.text == "?")
        {
            bool flag = false;
            IEnumerator WaitForVariableChange()
            {
                StartCoroutine(WaitForVariableChange());
                // ∆дем изменени€ состо€ни€ переменной
                while (!flag)
                {
                    if(kingTrigger.kingName.text != "?")
                    {
                        winnerBoard.GetComponent<TMP_Text>().text = $"{kingTrigger.kingName.text}" + " " + "won";

                        // if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
                        // {
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;

                        //  }

                        winnerBoard.SetActive(true);
                        break;
                    }

                    yield return null;
                }
               
                // ѕосле изменени€ состо€ни€ переменной продолжаем выполнение метода
                Debug.Log("Variable has been changed");
            }

          
        }
        else if (kingTrigger.kingName.text == string.Empty)
        {
            winnerBoard.GetComponent<TMP_Text>().text = "LOL";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            winnerBoard.SetActive(true);
            
        }
    }


    public void SendEndGameMode()
    {


        PhotonNetwork.CurrentRoom.IsVisible = true;
        modManager.leaveModeButton.SetActive(false);
        //bool[] _params = {modManager.startMod, modManager.teleportToSpawn };
        photonView.RPC("EndGameMode", RpcTarget.All);
        




    }

    [PunRPC]
    public void EndGameMode()
    {
        modManager.startMod = false;
        modManager.teleportToSpawn = true;
        kingTrigger.kingName.text = string.Empty;
        modManager.timerGameObject.SetActive(false);
        modManager.winnerBoard.SetActive(false);
        //PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.RemoveBufferedRPCs();
       

    }


}
