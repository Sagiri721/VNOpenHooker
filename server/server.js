const net = require('net');
const fs = require('fs');
const iconv = require('iconv-lite');

var settings = JSON.parse(fs.readFileSync("D:\\TIAGO\\program\\text hooking\\vnhooker\\server\\hosting_settings.json"));

const PORT = settings.port;
const host = settings.host;

var source = null;

const server = net.createServer((socket) => {
    
    const remotePort = socket.remotePort;

    console.log(`Client connected: ${socket.remoteAddress}:${socket.remotePort}`);
    //socket.setEncoding('Shift_JIS');

    socket.on('data', (data) => {

        const utf8Data = iconv.decode(data, 'Shift_JIS');
        
        if(utf8Data == "E" /* utf8 equivalent of byte array ["69"] */ ){
            source = socket;
            console.log("Reader = " + remotePort);
            source.write("We're connected as the reader! This client will receive messages with the hooked output!");
            return;
        }

        onMessageReceive(utf8Data, socket);
    });

    socket.on('end', () => {
        console.log(`Client disconnected: ${socket.remoteAddress}:${socket.remotePort}`);
    });
    
    socket.on('error', (err) => {
        console.error(`Socket error: ${err.message}`);
    });
});

const onMessageReceive = (message, socket) => {
    
    if(source == null) return;
    // Send output to the reader
    source.write(message);
}

// Start listening
server.listen(PORT, host, () => {
    
    console.log("Listening on port " + PORT);
});