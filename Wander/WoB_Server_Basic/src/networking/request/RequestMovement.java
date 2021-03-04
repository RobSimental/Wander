package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import networking.response.ResponseMovement;
import utility.DataReader;
import utility.Log;


public class RequestMovement extends GameRequest {

    private String locationX;
    private String locationY;
    private String username;
    private ResponseMovement responseMovement;

    public RequestMovement(){
        responses.add(responseMovement = new ResponseMovement());
    }
    @Override
    public void parse() throws IOException {
        username = DataReader.readString(dataInput).trim();
        locationX = DataReader.readString(dataInput).trim();
        locationY = DataReader.readString(dataInput).trim();
    }

    @Override
    public void doBusiness() throws Exception {
        // Log.printf("Username '%s' moved to the X: '%s' Y: '%s'",username, locationX, locationY);
        responseMovement.setUsername(username);
        responseMovement.setLocationX(locationX);
        responseMovement.setLocationY(locationY);
        // for multiplayer send the information to all the clients?
    }


}