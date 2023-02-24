using Photon.Chat.UtilityScripts;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ModManagerModule : MonoBehaviourPun
{

    GameModeManager gameModeManager;
    // Перечисление возможных игровых режимов
    public enum GameMode
    {
        None,
        KOB
        
    }

    // Интерфейс для объектов управления режимом игры

    


    public interface IGameModeManager
    {
        void SendStartMod();
        void StartGame();
        void SendStopMode();
        void EndGame();


    }

    [SerializeField] public GameObject[] spawnPointsKOB;
    [SerializeField] private GameObject timerGameObject;
    [SerializeField] private GameObject winnerBoard;
    [SerializeField] private GameObject leaveModeButton;



    private SpawnPlayers _spawner;
    private GameModeManager _modManager;
    private Trigger _trigger;
    public GameObject modPanel;
    public GameObject buttonStart;
    private bool _startMod;
    private bool _stopMod;
    private bool _teleportToMod;
    private bool _teleportToSpawn;
    public bool startMod { get { return _startMod; } set { _startMod = value; } }
    public bool stopMod { get { return _stopMod; } set { _stopMod = value; } }
    public bool teleportToMod { get { return _teleportToMod; } set { _teleportToMod = value; } }
    public bool teleportToSpawn { get { return _teleportToSpawn; } set { _teleportToSpawn = value; } }

    public SpawnPlayers spawner { get { return _spawner; } set { _spawner = value; } }
    public GameModeManager modManager { get { return _modManager; } set { _modManager = value; } }
    public Trigger trigger { get { return _trigger; } set { _trigger = value; } }

    private void Start()
    {
        gameModeManager.Start();
    }
    public void SendSetGameMode()
    {
        gameObject.GetPhotonView().RPC("SetGameMode", RpcTarget.All, gameModeManager.currentGameMode);
    }

    public void SendEndGameMode()
    {


        PhotonNetwork.CurrentRoom.IsVisible = true;
        leaveModeButton.SetActive(false);
        gameObject.GetPhotonView().RPC("EndGameMode", RpcTarget.All);

    }

    public class GameModeManager : MonoBehaviour
    {
        ModManagerModule modManagerModule;
        // Начальный режим игры
        [SerializeField] private GameMode startingGameMode = GameMode.None;

        // Словарь для хранения объектов управления режимами игры
        [SerializeField] private Dictionary<GameMode, IGameModeManager> gameModeManagers = new Dictionary<GameMode, IGameModeManager>();

        // Текущий режим игры
        private GameMode _currentGameMode;

        public GameMode currentGameMode { get { return _currentGameMode; } set { _currentGameMode = value; } }



        // Инициализация словаря и выбор начального режима игры
        public void Start()
        {
            gameObject.TryGetComponent(out GameModeManager _modManager);
            modManagerModule.modManager = _modManager;
            GameObject.Find("Trigger").TryGetComponent(out Trigger _trigger);
            modManagerModule.modManager = _modManager;
            modManagerModule.trigger = _trigger;
            modManagerModule.spawner = gameObject.GetComponent<SpawnPlayers>();
            modManagerModule.leaveModeButton.SetActive(false);
            modManagerModule.timerGameObject.SetActive(false);
            modManagerModule.winnerBoard.SetActive(false);
            modManagerModule.modPanel.SetActive(false);
            modManagerModule.buttonStart.SetActive(false);
            modManagerModule.buttonStart.SetActive(false);

            
            // Заполнение словаря объектами управления для каждого режима игры
            gameModeManagers.Add(GameMode.KOB, new KingOfTheBridgeMod());
            

            // Выбор начального режима игры
            SetGameMode(startingGameMode);
        }
       
        // Установка текущего режима игры

        [PunRPC]
        public void SetGameMode(GameMode mode)
        {
            // Проверка наличия объекта управления для выбранного режима игры
            if (gameModeManagers.ContainsKey(mode))
            {
                // Остановка текущего режима игры
                if (currentGameMode != GameMode.None && gameModeManagers.ContainsKey(currentGameMode))
                {
                    gameModeManagers[currentGameMode].EndGame();
                }

                // Установка нового текущего режима игры
                currentGameMode = mode;

                // Запуск нового текущего режима игры
                gameModeManagers[currentGameMode].StartGame();
            }
            else
            {
                Debug.LogError("Game mode manager not found for mode " + mode);
            }
        }


      

        [PunRPC]
        public void EndGameMode()
        {
            modManagerModule.startMod = false;
            modManagerModule.teleportToSpawn = true;
            modManagerModule.timerGameObject.SetActive(false);
            modManagerModule.winnerBoard.SetActive(false);
            //PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.RemoveBufferedRPCs();


        }

        // Объект управления режимом игры "Mode1"
        private class KingOfTheBridgeMod : IGameModeManager
        {
            
            private Trigger _trigger;
            private GameModeManager gameModeManager;
            private KingTriggerScript kingTrigger;
            

            public void SendStartMod()
            {
                _trigger = gameModeManager.modManagerModule.trigger;
                if (_trigger.readyCount == PhotonNetwork.PlayerList.Length)
                {
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    gameModeManager.modManagerModule.startMod = true;
                    gameModeManager.modManagerModule.teleportToMod = true;
                    bool[] _params = { gameModeManager.modManagerModule.startMod, gameModeManager.modManagerModule.teleportToMod };
                    gameModeManager.gameObject.GetPhotonView().RPC("StartGame", RpcTarget.All, _params);


                }
            }

            [PunRPC]
            public void StartGame()
            {
                gameModeManager.modManagerModule.timerGameObject.GetComponent<Timer>().StartTimer();
                gameModeManager.modManagerModule.startMod = true;
                gameModeManager.modManagerModule.teleportToMod = true;
                gameModeManager.modManagerModule.timerGameObject.SetActive(true);
                kingTrigger.kingName.text = string.Empty;
                Debug.Log("Starting game in mode KOB");
            }

            public void SendStopMode()
            {
                gameModeManager.gameObject.GetPhotonView().RPC("EndGame", RpcTarget.All);
            }

            [PunRPC]
            public void EndGame()
            {
                if (gameModeManager.gameObject.GetPhotonView().Owner.IsMasterClient && gameModeManager.gameObject.GetPhotonView().AmOwner)
                {
                    gameModeManager.modManagerModule.leaveModeButton.SetActive(true);
                }

                GameObject.Find("KingTrigger").TryGetComponent(out KingTriggerScript _kingTrigger);
                kingTrigger = _kingTrigger;
                if (kingTrigger.kingName.text != string.Empty && kingTrigger.kingName.text != "?")
                {

                    gameModeManager.modManagerModule.winnerBoard.GetComponent<TMP_Text>().text = $"{kingTrigger.kingName.text}" + " " + "won";

                    // if (car.GetComponent<PhotonView>().Owner.IsMasterClient && car.GetComponent<PhotonView>().AmOwner)
                    // {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    //  }

                    gameModeManager.modManagerModule.winnerBoard.SetActive(true);


                }
                else if (kingTrigger.kingName.text == "?")
                {
                    gameModeManager.modManagerModule.winnerBoard.SetActive(true);
                    gameModeManager.modManagerModule.winnerBoard.GetComponent<TMP_Text>().text = $"{kingTrigger.kingName.text}" + " " + "won";        
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
          
                }
                else if (kingTrigger.kingName.text == string.Empty)
                {
                    gameModeManager.modManagerModule.winnerBoard.GetComponent<TMP_Text>().text = "LOL";
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    gameModeManager.modManagerModule.winnerBoard.SetActive(true);

                }
                Debug.Log("Ending game in mode 1");
            }
        }
    }
}