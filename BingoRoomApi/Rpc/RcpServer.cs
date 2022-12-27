using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using BingoRoomApi.Interfaces;

public class RpcServer : IDisposable
{
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly IBingoRoomService _bingoRoomService;

    public RpcServer(IBingoRoomService bingoRoomService)
    {
        // Connect to the RabbitMQ server
        var factory = new ConnectionFactory() { HostName = "host.docker.internal", Port = 5672 };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        // Declare a queue to act as the RPC server
        channel.QueueDeclare("rpc_queue", false, false, false, null);

        _bingoRoomService = bingoRoomService;
    }

    public void Start()
    {
        // Set up the message consumer to handle request messages
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += HandleRpcRequest;
        channel.BasicConsume("rpc_queue", true, consumer);
    }

    private async void HandleRpcRequest(object model, BasicDeliverEventArgs ea)
    {
        var request = Encoding.UTF8.GetString(ea.Body.ToArray());
        var requestParts = request.Split('|');
        var requestType = requestParts[0];
        var parametersString = requestParts[1];
        var parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(parametersString);
        bool isOwnerOfBingoRoom = false;

        // Use the requestType and parameters in your code
        switch (requestType)
        {
            case "get_user_is_owner_of_bingoroom":
                var bingoRoomId = parameters["bingoRoomId"];
                var userId = parameters["userId"];
                // Use the bingoRoomId and userId to know if user is owner of the bingoroom
                isOwnerOfBingoRoom = await _bingoRoomService.GetUserIsOwnerOfBingoRoom(bingoRoomId, userId);

                break;
        }

        // Send a response message to the RPC client
        var response = Encoding.UTF8.GetBytes(isOwnerOfBingoRoom.ToString());

        var props = ea.BasicProperties;
        var replyProps = channel.CreateBasicProperties();
        replyProps.CorrelationId = props.CorrelationId;

        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: response);
    }

    public void Dispose()
    {
        connection.Close();
    }
}


