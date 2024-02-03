#include <iostream>
#include <asio.hpp>

using namespace std;
using namespace asio;

class TCPServer {
public:
    TCPServer(io_service& ioService, short port)
        : acceptor(ioService, ip::tcp::endpoint(ip::tcp::v4(), port)),
        socket(ioService) {
        startAccept();
    }

private:
    void startAccept() {
        acceptor.async_accept(socket,
            [this](const error_code& ec) {
                if (!ec) {
                    cout << "Client connected: " << socket.remote_endpoint() << endl;
                    handleData();
                }
                startAccept();
            });
    }

    void handleData() {
        async_read_until(socket, buffer, '\n',
            [this](const error_code& ec, size_t bytes_transferred) {
                std::cout << ec;
                if (!ec) {
                    istream is(&buffer);
                    string message;
                    getline(is, message);
                    cout << "Received message: " << message << endl;
                    buffer.consume(bytes_transferred);  // Consume the read data
                    handleData();  // Continue reading data
                }
                else {
                    cout << "Client disconnected: " << socket.remote_endpoint() << endl;
                }
            });
    }

    ip::tcp::acceptor acceptor;
    ip::tcp::socket socket;
    asio::streambuf buffer;
};