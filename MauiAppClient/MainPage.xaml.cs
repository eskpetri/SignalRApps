using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace MauiAppClient;

public partial class MainPage : ContentPage
{
    HubConnection connection;
    public class ChatMessage
    {
        public string Message { get; set; }             //Bind property
    }
    ObservableCollection<ChatMessage> chatmessages = new ObservableCollection<ChatMessage>();
    public ObservableCollection<ChatMessage> ChatMessages { get { return chatmessages; } }

    public MainPage()
    {
        InitializeComponent();
        chatmessages.Add(new ChatMessage() { Message = "Make Connection" });
        messages.ItemsSource = chatmessages;                                    //Eventually "ListBox" working Almost everything goes differently than in WPF lol

        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7181/chathub")
            .WithAutomaticReconnect()
            .Build();

        connection.Reconnecting += (sender) =>
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                var newMessage = "Attempting to reconnect...";
                chatmessages.Add(new ChatMessage() { Message = newMessage });
            });

            return Task.CompletedTask;
        };

        connection.Reconnected += (sender) =>
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                var newMessage = "Reconnected to the server";
                chatmessages.Add(new ChatMessage() { Message = newMessage });
            });

            return Task.CompletedTask;
        };

        connection.Closed += (sender) =>
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                var newMessage = "Connection Closed";
                chatmessages.Add(new ChatMessage() { Message = newMessage });
                ConnectServer.IsEnabled = true;
                sendMessage.IsEnabled = false;
            });

            return Task.CompletedTask;
        };
    }

    private async void sendMessage_Click(object sender, EventArgs e)
    {
        try
        {
            await connection.InvokeAsync("SendMessage",
                "MAUI Client", messageInput.Text);
        }
        catch (Exception ex)
        {
            chatmessages.Add(new ChatMessage() { Message = ex.Message });
        }
    }
    private async void OnConnectServer(object sender, EventArgs e)
    {
        //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };//Don't check cert
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                var newMessage = $"{user}: {message}";
                chatmessages.Add(new ChatMessage() { Message = newMessage });
            });
        });

        try
        {
            await connection.StartAsync();
            chatmessages.Add(new ChatMessage() { Message = "Connection Started" });
            ConnectServer.IsEnabled = false;   //Change buttons visibility/clickability
            sendMessage.IsEnabled = true;
            messageInput.Text = "";
        }
        catch (Exception ex)
        {
            chatmessages.Add(new ChatMessage() { Message = ex.Message });
            Console.WriteLine(ex.Message);
        }
    }
}