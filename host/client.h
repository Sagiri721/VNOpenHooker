#pragma once
#include <iostream>
#include <asio.hpp>

using namespace std;
using namespace asio;

void send_message(int port, string ip, string message) {

    try {
        io_service ioService;
        ip::tcp::socket socket(ioService);
        ip::tcp::resolver resolver(ioService);

        // Resolve the server endpoint
        ip::tcp::resolver::results_type endpoints = resolver.resolve(ip, std::to_string(port));

        // Connect to the server
        asio::connect(socket, endpoints);

        // Send a message to the server
        auto result = asio::write(socket, asio::buffer(message));

        // Close the connection
        socket.shutdown(ip::tcp::socket::shutdown_send);
        socket.close();
    }
    catch (const exception& e) {
        cerr << "Exception: " << e.what() << endl;
    }
}