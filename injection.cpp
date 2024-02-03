#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <windows.h>
#include <tlhelp32.h>

char evilDLL[] = "D:\\TIAGO\\program\\text hooking\\vnhooker\\createhook\\Release\\createhook.dll";
unsigned int evilLen = sizeof(evilDLL) + 1;

int main(int argc, char* argv[]) {
    
    HANDLE ph; // process handle
    HANDLE rt; // remote thread
    LPVOID rb; // remote buffer

    // handle to kernel32 and pass it to GetProcAddress
    HMODULE hKernel32 = GetModuleHandle("Kernel32");
    FARPROC lb = GetProcAddress(hKernel32, "LoadLibraryA");

    // parse process ID
    if (atoi(argv[1]) == 0) {
        printf("PID not found :( exiting...\n");
        return -1;
    }

    printf("Injecting to PID: %i\n", atoi(argv[1]));
    ph = OpenProcess(PROCESS_ALL_ACCESS, FALSE, DWORD(atoi(argv[1])));

    // allocate memory buffer for remote process
    rb = VirtualAllocEx(ph, NULL, evilLen, (MEM_RESERVE | MEM_COMMIT), PAGE_EXECUTE_READWRITE);

    // "copy" evil DLL between processes
    WriteProcessMemory(ph, rb, evilDLL, evilLen, NULL);

    // our process start new thread
    rt = CreateRemoteThread(ph, NULL, 0, (LPTHREAD_START_ROUTINE)lb, rb, 0, NULL);
    CloseHandle(ph);

    printf("Injection succesfull you should see a greetings message from the target application\n");

    return 0;
}