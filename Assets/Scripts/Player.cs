using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Objecto classe player
    public int idPlayer;
    public int idTable;
    public int servants, influence, level, handCount;
    public string ip;
    public char roomCreator;

    public List<Card> hand = new List<Card>();
    public Stack<Card> playStack = new Stack<Card>();

    public Text serv, inf;

    public Player(int idPlayer, int servants, int influence, int level, int handCount, string ip)
    {
        this.idPlayer = idPlayer;
        this.servants = servants;
        this.influence = influence;
        this.level = level;
        this.handCount = handCount;
        this.ip = ip;
    }

    public Player() {
        
    }

    public void setServants(int s) { this.servants = s; serv.text = servants.ToString(); }
    public void setInfluence(int i) { this.influence = i; inf.text = influence.ToString(); }

    public int getServants() { return this.servants; }
    public int getInfluence() { return this.influence; }

    public int getLevel() { return this.level; }



    private void Start()
    {
        setServants(5);
        setInfluence(0);
    }

    private void Update()
    {
        if (3 > this.influence)
        {
            level = 1;
        }
        else if (2 < this.influence && 5 > this.influence)
        {
            level = 2;
        }
        else if (5 <= this.influence)
        {
            level = 3;
        }

    }

    void createGroup()
    {

    }

    void joinGroup()
    {

    }

    void makePlay()
    {

    }
}

