#pragma once
#include <iostream>
#include <asio.hpp>

using namespace asio;

class tcp_client
{
    private:
        std::shared_ptr<ip::tcp::socket> socket;
        std::array<char, 1024> recv_buf;

        io_service ioService_;
        void start_receive()
        {
            // TODO
        }

    public:
        tcp_client(std::string host, int port)
        {
            socket = std::make_shared<ip::tcp::socket>(ioService_);
            socket->connect(ip::tcp::endpoint(ip::address::from_string(host), port));

            start_receive();
        }

        ~tcp_client() {
            ioService_.stop();
        }

        void send(std::string const& message) {
            this->socket->send(asio::buffer(message));
        }

        void close() {

            this->socket->close();
        }

        void RunIoService() {
            ioService_.run();
        }
};