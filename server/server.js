const net = require('net');
const PORT = 8080;

const server = net.createServer((socket) => {
    
    console.log(`Client connected: ${socket.remoteAddress}:${socket.remotePort}`);
    socket.setEncoding('utf-8');

    socket.on('data', (data) => {
        onMessageReceive(data);
    });

    socket.on('end', () => {
        console.log(`Client disconnected: ${socket.remoteAddress}:${socket.remotePort}`);
    });
    
    socket.on('error', (err) => {
        console.error(`Socket error: ${err.message}`);
    });
});

const onMessageReceive = (message) => {


}

// Start listening
server.listen(PORT, () => {
    console.log(`Listening on port ${PORT}`);
});