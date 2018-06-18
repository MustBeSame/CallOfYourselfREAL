using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Objecto classe player
    public int idPlayer;
    public int servants, influence, level;

    public List<Card> hand = new List<Card>();
    Stack<int> pStack = new Stack<int>();

    public Text serv, inf;

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
        else if (5 > this.influence && 2 < this.influence)
        {
            level = 2;
        }
        else if (5 > this.influence)
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

