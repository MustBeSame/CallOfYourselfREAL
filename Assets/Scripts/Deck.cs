using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>();

    public Text size;


    public void shuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        int a = 1;
    }

    private void Update()
    {
        size.text = deck.Count.ToString();
    }

    public Card Draw()
    {
        Card temp;
       
        temp = deck[0];

        deck.RemoveAt(0);

        return temp;
    }
}