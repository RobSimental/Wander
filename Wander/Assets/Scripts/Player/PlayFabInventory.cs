using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using Mirror;


public class PlayFabInventory : NetworkBehaviour
{
    //todo potions need to heal player
    //todo set active consumable function to change based on user choice
    private string activeConsumable;
    
    
    public override void OnStartAuthority()
    {
        CmdSessionTicket(PlayFabLogin.sessionTicket);
        activeConsumable = "HealthPotion"; 
        GetInventory();
    }
    [Command]
    private void CmdSessionTicket(string sTicket)
    {
        PlayFabServerAPI.AuthenticateSessionTicket(new AuthenticateSessionTicketRequest { SessionTicket = sTicket },
            result => {
                Player.playFabID = result.UserInfo.PlayFabId;
            }, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    private void Update()
    {
        if (Input.GetButtonDown("Consumable"))
        {
            CmdConsumePotion(activeConsumable);
        }
    }
    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), 
            result =>
        {
            foreach(PlayFab.ClientModels.ItemInstance item in result.Inventory)
            {
                Debug.Log(item.ItemId);
            }
        }
        , error => 
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    [Command]
    public  void CmdConsumePotion(string activeConsumable)
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(),
            result =>
            {
                foreach(PlayFab.ClientModels.ItemInstance item in result.Inventory)
                {
                    if (item.ItemId.Equals(activeConsumable))
                    {
                        activeConsumable = item.ItemInstanceId;
                    }
                }


                PlayFabClientAPI.ConsumeItem(new PlayFab.ClientModels.ConsumeItemRequest { ConsumeCount = 1, ItemInstanceId = activeConsumable }, cresult => {
                    Debug.Log(cresult.RemainingUses);
                }, cerror => { Debug.Log(cerror.GenerateErrorReport()); });

            }
        , error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

}
