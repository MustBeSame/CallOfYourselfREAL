using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using MessagesLib;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Threading;

public class ClientController : MonoBehaviour
{
    public Player player;
    public Table table;
    public string password;
    public List<Card> cards = new List<Card>();
    public List<Card> discard = new List<Card>();
    private string NomeUsuario = "Desconhecido";
    private StreamWriter stwEnviador;
    private StreamReader strReceptor;
    private TcpClient tcpServidor;
    private delegate void AtualizaLogCallBack(string strMensagem);
    private delegate void FechaConexaoCallBack(string strMotivo);
    private Thread mensagemThread;
    private IPAddress enderecoIP;
    public string servidorIP = "";
    private bool Conectado;
    private string txtLog = "";
    private string txtUsuario = "Player1";
    private string txtMensagem = "";
    public char idTable;
    private char msgType;
    // Use this for initialization

    void Start()
    {
    }

    void Aweke()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Conectado && msgType != 'N')
        {
            EnviaMensagem();
        }
    }

    public void InicializaConexao()
    {
        try
        {
            enderecoIP = IPAddress.Parse(servidorIP);
            tcpServidor = new TcpClient();
            tcpServidor.Connect(enderecoIP, 2502);

            Conectado = true;
            stwEnviador = new StreamWriter(tcpServidor.GetStream());
            mensagemThread = new Thread(new ThreadStart(RecebeMensagens));
            mensagemThread.Start();
            msgType = '1';
            
        }
        catch (Exception ex)
        {
            Debug.Log("Erro : " + ex.Message + "Erro na conexão com servidor");
        }
    }

    public void JoinGame()
    {
        try
        {
            Debug.Log("Join Game");
            enderecoIP = IPAddress.Parse(servidorIP);
            tcpServidor = new TcpClient();
            tcpServidor.Connect(enderecoIP, 2502);

            Conectado = true;
            stwEnviador = new StreamWriter(tcpServidor.GetStream());
            mensagemThread = new Thread(new ThreadStart(RecebeMensagens));
            mensagemThread.Start();
            msgType = '2';

        }
        catch (Exception ex)
        {
            Debug.Log("Erro : " + ex.Message + "Erro na conexão com servidor");
        }
    }

    private void RecebeMensagens()
    {
        Debug.Log("RecebeMensagens");
        char[] dados = new char[100];
        strReceptor = new StreamReader(tcpServidor.GetStream());
        strReceptor.Read(dados, 0, dados.Length);
        Debug.Log(dados);

        switch (dados[0])
        {
            case '1':
                Debug.Log("mensagem 0");
                MsgTableCreateResponse msg = new MsgTableCreateResponse();
                msg.readMessage(dados);
                player.idPlayer = msg.IdPlayer;
                player.idTable = msg.IdTable;
                player.roomCreator = msg.RoomCreator;
                table.id = msg.IdTable;
                for (int i = 0; i < 5;i++)
                {
                    Card c = cards.Find(a => a.id == msg.Hand[i]);
                    player.hand.Add(c);
                }
                table.deck.size.text = msg.DeckCount.ToString();
                table.players.Add(player);
                break;
            case '2':
                MsgTableJoinResponse sgTableJoinResponse = new MsgTableJoinResponse();
                sgTableJoinResponse.readMessage(dados);
                player.idPlayer = sgTableJoinResponse.IdPlayer;
                player.idTable = sgTableJoinResponse.IdTable;
                player.roomCreator = sgTableJoinResponse.RoomCreator;
                table.id = sgTableJoinResponse.IdTable;
                for (int i = 0; i < 5; i++)
                {
                    Card c = cards.Find(a => a.id == sgTableJoinResponse.Hand[i]);
                    player.hand.Add(c);
                }
                table.deck.size.text = sgTableJoinResponse.DeckCount.ToString();
                sgTableJoinResponse.Players.ForEach(a=> {
                    if (a.Ip != player.ip)
                    {
                        table.players.Add(new Player(a.Id,a.Servos,a.Influence,0,a.SizeHand,a.Ip));
                    }
                });
                table.players.Add(player);
                break;
            case '3':
                MsgTableJoinInfoResponse sgTableJoinInfoResponse = new MsgTableJoinInfoResponse();
                sgTableJoinInfoResponse.readMessage(dados);
                Player p = new Player();
                p.name = sgTableJoinInfoResponse.IpPlayer;
                p.servants = sgTableJoinInfoResponse.ServosCount;
                p.influence = sgTableJoinInfoResponse.InfluenceCount;
                p.handCount = sgTableJoinInfoResponse.HandSize;
                Debug.Log("Entrou :" +p.name + "servos : " + p.servants + "influ: " + p.influence + "cartas na mao: " + p.handCount);
                table.players.Add(p);
                break;
            default:
                break;
        }
    }

    public void EnviaMensagem()
    {
        Debug.Log("EnviaMensagem" + msgType);
        switch (msgType)
        {
            case '1':
                Debug.Log("mensagem 0");
                MsgTableCreateRequest msgTableCreateRequest = new MsgTableCreateRequest('1',txtUsuario,'4',password);
                Debug.Log("mensagem : " + msgTableCreateRequest.createMessage());
                stwEnviador.WriteLine(msgTableCreateRequest.createMessage());
                stwEnviador.Flush();
                msgType = 'N';
                break;
            case '2':
                MsgTableJoinRequest sgTableJoinRequest = new MsgTableJoinRequest('2', idTable, txtUsuario, password);
                Debug.Log("mensagem : " + sgTableJoinRequest.createMessage());
                stwEnviador.WriteLine(sgTableJoinRequest.createMessage());
                stwEnviador.Flush();
                msgType = 'N';
                break;
            default:
                break;
        }
    }

    private void FechaConexao(string Motivo)
    {
        Conectado = false;
        stwEnviador.Close();
        strReceptor.Close();
        tcpServidor.Close();
    }

    public void OnApplicationExit(object sender, EventArgs e)
    {
        if (Conectado == true)
        {
            Conectado = false;
            stwEnviador.Close();
            strReceptor.Close();
            tcpServidor.Close();
        }
    }

    public void setIp(InputField text)
    {
        this.servidorIP = text.text;
        Debug.Log(servidorIP);
    }

    public void setPasswordTable(InputField text)
    {
        this.password = text.text;
        Debug.Log(password);
    }

    public void setPLayerName(InputField text)
    {
        this.txtUsuario = text.text;
        player.ip = txtUsuario;
        Debug.Log(txtUsuario);
    }

    public void setIdTable(InputField text)
    {
        this.idTable = text.text[0];
        Debug.Log(idTable);
    }
}
