using BlogAgro.Services;
using Moq;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace BlogAgro.Test
{
    public class WebSocketServerTests
    {
        private readonly WebSocketServer _server;

        public WebSocketServerTests()
        {
            _server = new WebSocketServer();
        }

        [Fact]
        public async Task StartAsync_ShouldAddClientAndHandleMessages()
        {
            // Arrange
            var mockWebSocket = new Mock<WebSocket>();
            mockWebSocket.Setup(ws => ws.State).Returns(WebSocketState.Open);
            mockWebSocket.Setup(ws => ws.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

            // Act
            await _server.StartAsync(mockWebSocket.Object);

        }

        [Fact]
        public async Task SendNotificationAsync_ShouldSendMessagesToAllClients()
        {
            // Arrange
            var mockWebSocket = new Mock<WebSocket>();
            mockWebSocket.Setup(ws => ws.State).Returns(WebSocketState.Open);
            mockWebSocket.Setup(ws => ws.SendAsync(It.IsAny<ArraySegment<byte>>(), WebSocketMessageType.Text, true, It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            var clientId = Guid.NewGuid();
            var clients = (ConcurrentDictionary<Guid, WebSocket>)typeof(WebSocketServer)
                .GetField("_clients", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(_server);
            clients.TryAdd(clientId, mockWebSocket.Object);

            var message = "Test message";

            // Act
            await _server.SendNotificationAsync(message);

            // Assert
            mockWebSocket.Verify(ws => ws.SendAsync(It.Is<ArraySegment<byte>>(a => Encoding.UTF8.GetString(a.Array, a.Offset, a.Count) == message), WebSocketMessageType.Text, true, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
