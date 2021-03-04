package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import core.GameClient;
import core.GameServer;
import metadata.Constants;
import model.Player;
import utility.DataReader;
import utility.Log;
import java.sql.*;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;

import networking.response.ResponseLobbies;

/**
 * The RequestLogin class authenticates the user information to log in. Other
 * tasks as part of the login process lies here as well.
 */

public class RequestLobbies extends GameRequest {


    private ResponseLobbies responseLobbies;

    public RequestLobbies() {
        responses.add(responseLobbies = new ResponseLobbies());
    }

    @Override
    public void parse() throws IOException {

    }

    @Override
    public void doBusiness() throws Exception {
        Log.printf("Getting all lobbies...");
        try {
            Class.forName("com.mysql.jdbc.Driver");
        } catch (ClassNotFoundException e) {
            System.out.println("Where is your MySQL JDBC Driver?");
            e.printStackTrace();
            return;
        }
        Connection connection = null;
        try {
            connection = DriverManager.getConnection("jdbc:mysql://" + "wanderdb.c4p7z07xl4sc.us-east-1.rds.amazonaws.com" + ":" + "3306" + "/" + "wander", "root", "awesomeganbold");
        } catch (SQLException e) {
            System.out.println("Connection Failed!:\n" + e.getMessage());
        }
        String query = "SELECT * FROM Lobbies";
        Statement st = connection.createStatement();
        ResultSet rs = st.executeQuery(query);
        JsonArray array = new JsonArray();
        while(rs.next()){
            JsonObject object = new JsonObject();
            String lobbyID = rs.getString("id");
            String lobbyName = rs.getString("name");
            int passwordRequired = rs.getInt("passwordRequired");
            String playersId = rs.getString("playersid");
            object.addProperty("lobbyID", lobbyID);
            object.addProperty("lobbyName", lobbyName);
            object.addProperty("passwordRequired", passwordRequired);
            object.addProperty("playersId", playersId);
            array.add(object);
        }
        st.close();
        JsonObject result = new JsonObject();
        result.add("LobbyInformation", array);
        responseLobbies.setStatus((short) 0);
        responseLobbies.setJson(result.toString());
    }



}
