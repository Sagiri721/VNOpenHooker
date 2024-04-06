#include "client.h"

using asio::ip::tcp;

asio::io_context ioContext;
tcp::socket sock(ioContext);

const int port = 8080;
const std::string host = "127.0.0.1";

void connect() {

    try
    {
        tcp::resolver resolver(ioContext);
        tcp::resolver::results_type endpoints = resolver.resolve(host, std::to_string(port));
        asio::connect(sock, endpoints);
    }
    catch (const std::exception&)
    {
        MessageBoxA(NULL, "Error connecting to server", "aw :(", MB_OK);
    }
}

void disconnect() {

    sock.close();
}

void send_message(std::string message, std::string header) {

    try
    {
        // Save the buffer
        /*std::ofstream outfile;
        outfile.open(targetFile, std::ios_base::app);
        outfile << message;

        outfile.close();*/

        // Stop writing to the funny file

        // Incorporate header in the message
        std::string message_head = "{head:" + header + ":end}";
        std::string message_full = message_head + message;

        asio::write(sock, asio::buffer(message_full));
    }
    catch (const std::exception&)
    {
        // Do something pleaseeee
        // Maybe write to a log?
    }
}