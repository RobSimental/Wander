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
    private string HPpotionid;
    private string playFabID;
    
    //Currently everytime authenticate user they are given 20 potions, this is for testing purposes
    //user player_title_added event to have only NEW users get potions
    public override void OnStartAuthority()
    {
        CmdSessionTicket(PlayFabLogin.sessionTicket);
        GetInventory();
    }
    [Command]
    private void CmdSessionTicket(string sTicket)
    {
        PlayFabServerAPI.AuthenticateSessionTicket(new AuthenticateSessionTicketRequest { SessionTicket = sTicket },
            result => {
                playFabID = result.UserInfo.PlayFabId;
            }, error => { Debug.Log(error.GenerateErrorReport()); });
    }
    //this function can only be accessed by the server
    //currently not being used, Playfab rule is granting starter items to new accounts
    //this function needs to work with save files after they are implemented
/*    [Command]
    public void InitInventory()
    {
        //Items for new users
        List<string> newUserItems = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            newUserItems.Add("HealthPotion");
        }
        PlayFabServerAPI.GrantItemsToUser(new GrantItemsToUserRequest { PlayFabId = playFabID, ItemIds = newUserItems, CatalogVersion = "Ingame" },
           result => {
               Debug.Log("new user items granted");
           },
           error => { Debug.Log(error.GenerateErrorReport()); });
    }*/
    private void Update()
    {
        if (Input.GetButtonDown("Consumable"))
        {
            CmdConsumePotion();
        }
    }
    void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), 
            result =>
        {
            HPpotionid = result.Inventory[0].ItemInstanceId;
            Debug.Log(result.Inventory[0].DisplayName);
            
        }
        ,error => 
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
    [Command]
    void CmdConsumePotion()
    {
        PlayFabClientAPI.ConsumeItem(new PlayFab.ClientModels.ConsumeItemRequest { ConsumeCount = 1, ItemInstanceId = HPpotionid}, result=> {
            Debug.Log(result.RemainingUses);
        }, error=> { Debug.Log(error.GenerateErrorReport()); });
    }

}
