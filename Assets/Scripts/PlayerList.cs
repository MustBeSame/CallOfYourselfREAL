using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerList : MonoBehaviour {

    public Table tb;
 

    private void Start()
    {
        showIcons(tb.players);
    }

    void showIcons(List<Player> players)
    {
        foreach (Player player in players)
        {
         // APARECER OS STATS DOS PLAYERS
        }
    }
}
