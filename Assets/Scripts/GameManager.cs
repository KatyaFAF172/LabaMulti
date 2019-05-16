using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MatchSettings matchSettings;

    void Awake()
    {
        if (instance)
            Debug.LogError("More that one Game Manager in scene");
        else
            instance = this;

    }

    #region  Player tracking

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();   //create dictionary where string is key and player is a value

    public static void RegisterPlayer(string netID, Player player)  //adds players to the game and registers their names
    {
        string playerID = PLAYER_ID_PREFIX + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnRegisterPlayer(string playerID)    //remove player from the game and free the name in dictionary
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }

    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string playerID in players.Keys)
    //    {
    //        GUILayout.Label(playerID + " - " + players[playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();

    //}

    #endregion
}
