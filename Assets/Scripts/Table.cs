using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{

    public Deck deck;
    public int id;


    public List<Player> players = new List<Player>();


    //Dictionary player play

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Player[] p;
        p = Resources.FindObjectsOfTypeAll<Player>();

        foreach (Player player in p)
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

    public void SubmitPlay(Player player)
    {
        GameObject chain = GameObject.Find("Chain");

        Debug.Log(chain.name);
        
        foreach (CardDisplay cardD in chain.GetComponentsInChildren<CardDisplay>())
        {
            player.playStack.Push(cardD.card);
        }
        foreach (Card card in player.playStack)
        {         

            switch ((int)card.cn)
            {
                case 0:
                    for (int i = 0; i < player.getInfluence(); i++)
                    {
                        player.hand.Add(deck.Draw());
                    }

                    break;

                case 1:

                    player.hand.Add(deck.Draw());
                    player.hand.Add(deck.Draw());

                    player.setInfluence(player.getInfluence() + 1);

                    break;
                case 2:
                    // PENSAR EM COMO IMPLEMENTAR
                    break;
                case 3:
                    foreach (Player p in players)
                    {
                        if (p.idPlayer != player.idPlayer)
                        {
                            p.setInfluence(p.getInfluence() + 1);
                            p.setServants(p.getServants() + 1);
                        }
                    }
                    break;
                case 4:
                    /*
                     * Talvez salvar a influencia do jogador aqui e setar uma flag, no fim no foreach caso a flag seja setada
                     * voltar a influencia para o que foi salvo
                     */
                    break;
                case 5:
                    //Talvez tenha que ser uma lista e nao uma stack
                    break;
                case 6:
                    //Fora do turno, verificar
                    break;
                case 7:
                    players[card.idPlayerAlvo].setInfluence(players[card.idPlayerAlvo].getInfluence() - player.getLevel());
                    break;
                case 8:
                    player.setInfluence(player.getInfluence() + player.getLevel());
                    break;
                case 9:
                    players[card.idPlayerAlvo].setServants(players[card.idPlayerAlvo].getServants() - player.getLevel());
                    break;
                case 10:
                    player.setInfluence(player.getInfluence() + player.getLevel());
                    break;
                case 11:
                    //Pop no discard
                    break;
                case 12:
                    //Fim do turno, verificar tambem
                    break;
                case 13:
                    //Talvez alguns efeitos devam ser processados em outro metodo
                    break;
                case 14:
                    /*
                     * Talvez salvar a influencia do jogador aqui e setar uma flag, no fim no foreach caso a flag seja setada
                     * voltar a influencia para o que foi salvo
                     */
                    break;
                case 15:
                    //Verificar tambem
                    break;
                case 16:
                    if (player.getInfluence() == 2)
                    {
                        player.setServants(player.getServants() - 1);
                        player.setInfluence(player.getInfluence() - 2);
                    }
                    else if (player.getInfluence() == 4)
                    {
                        player.setServants(player.getServants() - 2);
                        player.setInfluence(player.getInfluence() - 4);
                    }
                    else if (player.getInfluence() == 6)
                    {
                        player.setServants(player.getServants() - 3);
                        player.setInfluence(player.getInfluence() - 6);
                    }
                    
                    break;
                case 17:
                    player.setServants(player.getServants() - player.getLevel());
                    break;
                case 18:
                    player.setServants(player.getServants() - player.getLevel());
                    players[card.idPlayerAlvo].setServants(players[card.idPlayerAlvo].getServants() + player.getLevel());
                    break;
                case 19:
                    //Nao pode mudar o lugar da play
                    break;
            }

            player.playStack.Pop();
        }
    }
}
