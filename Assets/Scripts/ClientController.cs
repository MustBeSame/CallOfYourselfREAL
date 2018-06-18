using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using MessagesLib;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClientController : MonoBehaviour {
    Socket sender;
    IPHostEntry ipHostInfo;
    IPAddress ipAddress;
    IPEndPoint remoteEP;
    byte[] bytes = new byte[1024];
    char[] data;
    public Player player;
    public Table table;
    public List<Card> cards = new List<Card>();
    public List<Card> discard = new List<Card>();
    // Use this for initialization
    void Start () {
        ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        ipAddress = ipHostInfo.AddressList[0];
        remoteEP = new IPEndPoint(ipAddress, 11000);
        sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
    }

    void Aweke()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createTable()
    {
        try
        {
            sender.Connect(remoteEP);
            char[] password = { '1', '2', '3' };
            MessagesLib.MsgTableCreateRequest request = new MsgTableCreateRequest('0', ipAddress.ToString().ToCharArray(), '4',password);
            byte[] msg = Encoding.ASCII.GetBytes(request.createMessage());

            int bytesSent = sender.Send(msg);

            int bytesRec = sender.Receive(bytes);
            data = Encoding.ASCII.GetChars(bytes, 0, bytesRec);
            Debug.Log(data[0]);
            for(int i = 0;i< data.Length;i++)
            {
                Debug.Log(data[i]);
            }
            Debug.Log(data.ToString());
            processMessages();

        }
        catch (ArgumentNullException ane)
        {
            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException : {0}", se.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
    }

    private void processMessages()
    {
        switch (data[0])
        {
            case '0':
                Debug.Log("Entrou");
                for (int i = 0; i < data.Length; i++)
                {
                    Debug.Log(data[i]);
                }
                processCreateTableResponse();
                break;
            case '1':
                processJoinTableResponse();
                break;
            case '2':
                processPlayResponse();
                break;
            case '3':
                processDrawResponse();
                break;
            default:
                Debug.Log("N entrou");
                break;
        }
    }

    private void processDrawResponse()
    {
        throw new NotImplementedException();
    }

    private void processPlayResponse()
    {
        throw new NotImplementedException();
    }

    private void processJoinTableResponse()
    {
        throw new NotImplementedException();
    }

    private void processCreateTableResponse()
    {
        Debug.Log("Entrou2");
        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }
        player.idPlayer = data[1];
        Debug.Log(data[1]);
        table.id = data[2];
        Debug.Log(data[2]);
        for (int i= 0; i< 5;i++)
        {
            player.hand.Add(cards.Find(a => a.id == data[i+3]));
        }
    }
}
