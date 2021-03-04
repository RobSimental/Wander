package networking.response;

// Other Imports
import metadata.Constants;
import utility.GamePacket;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseLobbies extends GameResponse {

    private short status;
    private String json;

    public ResponseLobbies() {
        responseCode = Constants.SMSG_LOBBY;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addShort16(status);
        packet.addString(json);        
        return packet.getBytes();
    }

    public void setStatus(short status){
        this.status = status;
    }

    public void setJson(String json){
        this.json = json;
    }

}