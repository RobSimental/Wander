package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import networking.response.ResponseSpawn;
import utility.DataReader;
import utility.Log;
/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestSpawn extends GameRequest {

    // Data
    private int id;

    // Responses
    private ResponseSpawn responseSpawn;

    public RequestSpawn() {
        responses.add(responseSpawn = new ResponseSpawn());
    }

    @Override
    public void parse() throws IOException {
        id = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        responseSpawn.setStatus((short) 0);
        responseSpawn.setId(id);
        for(GameClient player : GameServer.getInstance().getActiveThreads().values()){
            if(player != this.client){
                ResponseSpawn otherPlayerInfo = new ResponseSpawn();
                otherPlayerInfo.setStatus((short) 0);
                otherPlayerInfo.setId(player.getUserID());
                Log.printf(player.getID());
                client.addResponseForUpdate(otherPlayerInfo);
            }
        }
        for(GameClient player : GameServer.getInstance().getActiveThreads().values()){
            if(player != this.client){
                player.addResponseForUpdate(responseSpawn);
            }
        }
    }
}
