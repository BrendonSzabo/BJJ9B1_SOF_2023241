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