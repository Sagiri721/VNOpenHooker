#include <winsock2.h>
#include <windows.h>
#include "detours.h"
#include <string>
#include <fstream>

const int port = 8080;
const std::string host = "127.0.0.1";

using namespace std;

const std::string targetFile = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";

void send_message(string message) {

    try
    {

        // Save the buffer
        std::ofstream outfile;
        outfile.open(targetFile, std::ios_base::app);
        outfile << message;

        outfile.close();

    }
    catch (const std::exception&)
    {
    }

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

            break;

        case DLL_THREAD_ATTACH:
            break;
        case DLL_THREAD_DETACH:
            break;
    }
    return TRUE;
}