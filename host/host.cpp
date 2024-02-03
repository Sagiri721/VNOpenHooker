#include <iostream>
#include <asio.hpp>

#include "server.h"
#include "client.h"

using namespace asio;

#define PORT 8080

int main(int argc, char* argv[]) {

    if (argc != 2) {
        std::cout << "Missing argument start mode" << std::endl;
        return -1;
    }

    // Execution mode
    int m = std::stoi(argv[1]);

    if (m == 0) {

        try {

            std::cout << "Starting server mode on port " << PORT << std::endl;

            io_service ioService;
            TCPServer server(ioService, PORT);
            ioService.run();
        }
        catch (const std::exception& e) {
            std::cerr << "Exception: " << e.what() << std::endl;
        }
    }
    else {

        std::cout << "Starting client mode" << std::endl;

        // always add \n
        send_message(PORT, "127.0.0.1", "Connection established :3\n");
    }

    return 0;
}

