package networking.response;

// Other Imports
import metadata.Constants;
import utility.GamePacket;

/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseMovement extends GameResponse {

    private String locationX;
    private String locationY;
    private String username;

    public ResponseMovement() {
        responseCode = Constants.SMSG_MOVE;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addString(username);
        packet.addString(locationX);
        packet.addString(locationY);
        return packet.getBytes();
    }

    public void setLocationX (String locationX) {
        this.locationX = locationX;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public void setLocationY (String locationY) {
        this.locationY = locationY;
    }
}