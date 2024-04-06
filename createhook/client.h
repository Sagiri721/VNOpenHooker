#pragma once

#include <asio.hpp>

#include <iostream>
#include <string>

// Global socket connection
extern asio::io_context ioContext;
extern asio::ip::tcp::socket sock;

void connect();
void disconnect();

void send_message(std::string, std::string header);