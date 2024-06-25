function submitMatchForm(element) {
    const matchId = element.getAttribute('data-match-id');
    const form = document.getElementById('matchForm-' + matchId);
    form.submit();
}

function submitPlayerForm(element) {
    const playerId = element.getAttribute('data-player-id');
    const form = document.getElementById('playerForm-' + playerId);
    form.submit();
}

function onDragStart(event) {
    event.dataTransfer.setData('text/plain', event.target.id);
}

function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    event.preventDefault();
    const playerId = event.dataTransfer.getData('text/plain');
    const playerCard = document.getElementById(playerId);
    const target = event.target;

    if (target.classList.contains('laner-box')) {
        target.innerHTML = '';
        target.appendChild(playerCard.cloneNode(true));
    }
}

document.querySelectorAll('.laner-box').forEach(box => {
    box.addEventListener('dragover', onDragOver);
    box.addEventListener('drop', onDrop);
});

document.addEventListener('DOMContentLoaded', (event) => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub")
        .build();

    const buttons = document.getElementsByClassName('playerButton');

    buttons.forEach(function (button) {
        button.addEventListener('dblclick', function () {
            var form = button.closest('playerForm');
            if (form) {
                form.submit();
            }
        });

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

    function playerProfile(playerId) {
        const url = '/Home/PlayerDetails';
        const formData = new FormData();
        formData.append('id', playerId);
        fetch(url, {
            method: 'POST',
            body: formData,
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text(); // Assuming the response is HTML to be inserted into the page
            })
            .then(data => {
                // Handle the response data
                console.log(data);
                // Assuming you want to insert the response HTML into a specific element
                document.getElementById('playerDetailsContainer').innerHTML = data;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }

    // Make openChat and closeChat functions available globally
    window.openChat = openChat;
    window.closeChat = closeChat;
    window.playerProfile = playerProfile;
});

