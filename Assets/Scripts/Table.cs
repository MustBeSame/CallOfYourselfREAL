using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {

    public Deck deck;

    public List<Player> players = new List<Player>();

    
    //Dictionary player play

    private void Start()
    {
        Player[] p;
        p = Resources.FindObjectsOfTypeAll<Player>();

        foreach(Player player in p)
        {
            players.Add(player);
        }

        fillHand();
    }
    void fillHand()
    {
        var panel = GameObject.Find("Hand");
        for (int i = 0; i < 1; i++)
        {
            while (5 > players[i].hand.Count)
            {
                deck.shuffleDeck();
                players[i].hand.Add(deck.Draw());

                if (panel != null)  // make sure you actually found it!
                {
                    GameObject a = GameObject.Find("CardPrefab");

                    a.GetComponentInChildren<Image>().sprite = players[i].hand[players[i].hand.Count - 1].artWork;
                    a.GetComponent<CardDisplay>().card = players[i].hand[players[i].hand.Count - 1];

                    a = Instantiate(a);
                    a.transform.SetParent(panel.transform, false);
                }
            }
        }
    }
}
