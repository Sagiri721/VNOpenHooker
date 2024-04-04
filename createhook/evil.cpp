#define WIN32_LEAN_AND_MEAN
#include <winsock2.h>
#include <windows.h>
#include <detours.h>
#include <string>
#include <fstream>
#include <thread>
#include <iostream>
#include <asio.hpp>

const int port = 8080;
const std::string host = "127.0.0.1";

using namespace std;
using asio::ip::tcp;

const std::string targetFile = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";

// Global socket connection
asio::io_context ioContext;
tcp::socket sock(ioContext);

void send_message(string message) {

    try
    {
        // Save the buffer
        /*std::ofstream outfile;
        outfile.open(targetFile, std::ios_base::app);
        outfile << message;

        outfile.close();*/

        // Stop writing to the funny file
        asio::write(sock, asio::buffer(message));
    }
    catch (const std::exception&)
    {
    }

}

void sanitizeTask(std::string text) {

    std::string copyText = text;
    //copyText = SanitizeBuffer(copyText);
    if (copyText == "" || copyText.find_first_not_of(' ') == std::string::npos) return;

    send_message(copyText);
}

typedef LPSTR(WINAPI* CharNextAType)(LPCSTR);
CharNextAType TrueCharNextA = CharNextA;

LPSTR WINAPI MyCharNextA(LPCSTR lpsz)
{
    // extremely evil code
    string text(lpsz);
    send_message(text);

    return TrueCharNextA(lpsz);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  nReason, LPVOID lpReserved) {

    // Write the output file UTF-8 BOM
    std::ofstream outFile(targetFile, std::ios_base::out | std::ios::binary);
    if (outFile.is_open()) {
        outFile << '\xEF\xBB\xBF';
        outFile.close();
    }

    switch (nReason) {

        case DLL_PROCESS_ATTACH:

            MessageBoxA(
                NULL,
                "Meow from evil.dll!",
                "=^..^=",
                MB_OK
            );

            // Start the socket connection
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

            // Install the hook
            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());
            DetourAttach(&(PVOID&)TrueCharNextA, MyCharNextA);
            DetourTransactionCommit();

            break;

        case DLL_PROCESS_DETACH:

            // Remove the hook
            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());
            DetourDetach(&(PVOID&)TrueCharNextA, MyCharNextA);
            DetourTransactionCommit();

            // Close connection
            sock.close();

            break;

        case DLL_THREAD_ATTACH:
            break;
        case DLL_THREAD_DETACH:
            break;
    }
    return TRUE;
}