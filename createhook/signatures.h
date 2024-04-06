#pragma once

#include <string>

#include <Windows.h>
#include <detours.h>

typedef LPSTR(WINAPI* CharNextAType)(LPCSTR);
extern CharNextAType TrueCharNextA;

typedef BOOL(WINAPI* TextOutAType)(HDC, int, int, LPCSTR, int);
extern TextOutAType TrueTextOutA;