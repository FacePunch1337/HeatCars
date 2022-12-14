using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TMP : MonoBehaviour
{
   public TMP_Text playersCountText;
   public TMP_Text readyCountText;
   public TMP_Text FPS;
   private float avgFrameRate;

    private void Start()
    {
        
    }
    private void Update()
    {
        playersCountText.text = $"Players: {PhotonNetwork.PlayerList.Length.ToString()}";

       if(!GameObject.Find("Trigger").TryGetComponent(out Trigger trigger)) return;
       else readyCountText.text = $"Ready: {trigger.readyCount.ToString()}";





        avgFrameRate = Time.frameCount / Time.time;
        FPS.text = $"FPS: {avgFrameRate.ToString()}";

    }
    
}
