#include <iostream>
#include <string>

#include "utils.h"

FARPROC get_address_of(std::string function_name) {

	HMODULE hModule = GetModuleHandle(TEXT("user32.dll"));
	if (hModule == NULL) {

		MessageBoxA(NULL, "Could not get handle of user32.dll", "error", MB_OK);
		return nullptr;
	}

	FARPROC pFunc = GetProcAddress(hModule, function_name.c_str());
	if (pFunc == NULL) {

		MessageBoxA(NULL, (std::string("Failed to get address of ") + function_name).c_str(), "error", MB_OK);
		return nullptr;
	}

	return pFunc;
}