using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

    //public string title;
    //public string description;
    public enum CardName { AdquirirRecursos, Ambicao,  AumentarRitual, Barganhar, ConhecimentoOculto, Contramagica,
                       CortarConexoes, Espionar, EspreitarMundo, FalsaAjuda, GanhandoFama, LevantarMortos, MudancaRumo,
                       MudandoReceita, PortaSecreta, Potencializar, RitualPoder, RitualSombrio, RitualTrans, TrancarConhecimento};

    public CardName cn;

    public Sprite artWork;

    public Player player;

    public char id;

    public int idPlayerAlvo;

    public int type;    

    public void setPlayer (Player p) { this.player = p; }
    
}
