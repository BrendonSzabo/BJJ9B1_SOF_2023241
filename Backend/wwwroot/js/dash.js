document.addEventListener('DOMContentLoaded', (event) => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

    console.log('Connecting to hub');

    connection.on("ReceiveMessage", (user, message) => {
        const msg = document.createElement('div');
        msg.textContent = `${user}: ${message}`;
        msg.style.color = 'white';
        document.getElementById('messagesList').appendChild(msg);
    });

    connection.on("Connected", (connectionId) => {
        let button = document.getElementById('chatButton');
        if (button) {
            button.style.backgroundColor = '#89ff8978';
        }
    });

    connection.on("Disconnected", (connectionId) => {
        let button = document.getElementById('chatButton');
        if (button) {
            button.style.backgroundColor = 'red';
        }
    });

    connection.start().then(() => {
        const userName = document.getElementById('userInput').value;
        connection.invoke('RegisterUser', userName).catch(err => console.error(err.toString()));
    }).catch(err => console.error(err.toString()));

    document.getElementById('chatForm').addEventListener('submit', event => {
        event.preventDefault();
        const user = document.getElementById('userInput').value;
        const message = document.getElementById('messageInput').value;
        const destination = document.getElementById('destinationInput').value;
        connection.invoke('SendMessage', user, message, destination).catch(err => console.error(err.toString()));
        document.getElementById('messageInput').value = '';
    });

    function openChat() {
        document.getElementById('chatPopup').style.display = 'block';
    }

    function closeChat() {
        document.getElementById('chatPopup').style.display = 'none';
    }

    window.openChat = openChat;
    window.closeChat = closeChat;
});

