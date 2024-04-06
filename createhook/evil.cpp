#include <string>
#include <fstream>
#include <iostream>
#include <vector>

#include "utils.h"

#include "signatures.h"

using namespace std;

const std::string targetFile = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";

//void sanitizeTask(std::string text) {
//
//    std::string copyText = text;
//    //copyText = SanitizeBuffer(copyText);
//    if (copyText == "" || copyText.find_first_not_of(' ') == std::string::npos) return;
//
//    send_message(copyText);
//}

LPSTR WINAPI MyCharNextA(LPCSTR lpsz)
{
    // extremely evil code
    std::string text(lpsz);
    send_message(text, "CharNextA");

    return TrueCharNextA(lpsz);
}

BOOL WINAPI MyTextOutA(HDC hdc, int x, int y, LPCSTR lpString, int c)
{
    // extremely evil code
    std::string text(lpString);
    send_message(text, "TextOutA");

    return TrueTextOutA(hdc, x, y, lpString, c);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  nReason, LPVOID lpReserved) {

    // Write the output file UTF-8 BOM
    std::ofstream outFile(targetFile, std::ios_base::out | std::ios::binary);
    if (outFile.is_open()) {
        outFile << '\xEF\xBB\xBF';
        outFile.close();
    }

    // List of function to hook
    std::vector<std::string> functions_to_hook = { "CharNextA", "TextOutA" };

    switch (nReason) {

        case DLL_PROCESS_ATTACH:

            MessageBoxA(
                NULL,
                "Meow from evil.dll!!!",
                "=^..^=",
                MB_OK
            );

            // Start the socket connection
            connect();

            // Install the hook
            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());
            
            // Attach each function from the array
            for (const std::string& function : functions_to_hook)
            {
                // Get address
                FARPROC function_address = get_address_of(function);
                if (function_address != NULL) {

                    if (function == "CharNextA") {
                        DetourAttach(&(PVOID&)TrueCharNextA, MyCharNextA);
                    }
                    else if (function == "TextOutA") {
                        DetourAttach(&(PVOID&)TrueTextOutA, MyTextOutA);
                    }
                }
            }

            DetourTransactionCommit();

            break;

        case DLL_PROCESS_DETACH:

            // Remove the hook
            DetourTransactionBegin();
            DetourUpdateThread(GetCurrentThread());
            // Attach each function from the array
            for (const std::string& function : functions_to_hook)
            {   
                if (function == "CharNextA") {
                    DetourDetach(&(PVOID&)TrueCharNextA, MyCharNextA);
                }
                else if (function == "TextOutA") {
                    DetourDetach(&(PVOID&)TrueTextOutA, MyTextOutA);
                }
            }
            DetourTransactionCommit();

            // Close connection
            disconnect();

            break;

        case DLL_THREAD_ATTACH:
            break;
        case DLL_THREAD_DETACH:
            break;
    }
    return TRUE;
}