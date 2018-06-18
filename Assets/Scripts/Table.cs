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
        deck.shuffleDeck();
        var panel = GameObject.Find("Hand");
        var panel2 = GameObject.Find("Hand2");
        for (int i = 0; i < players.Count; i++)
        {
            while (5 > players[i].hand.Count)
            {
                
                players[i].hand.Add(deck.Draw());

                if (panel != null)  // make sure you actually found it!
                {
                    GameObject a = GameObject.Find("CardPrefab");

                    a.GetComponentInChildren<Image>().sprite = players[i].hand[players[i].hand.Count - 1].artWork;
                    a.GetComponent<CardDisplay>().card = players[i].hand[players[i].hand.Count - 1];

                    a = Instantiate(a);
                    if (i == 0)
                        a.transform.SetParent(panel.transform, false);
                    else
                        a.transform.SetParent(panel2.transform, false);
                }
            }
        }
    }

    public void SubmitPlay()
    {
        GameObject chain = GameObject.Find("Chain");
        int a = 0;
        Card temp = chain.GetComponentInChildren<CardDisplay>().card;

        //foreach (CardDisplay card in chain.GetComponentsInChildren<CardDisplay>())
        //{
        //    a++;
        //}
     
        
    }


}
