#pragma once
#include <iostream>
#include <string.h>

std::string SanitizeBuffer(std::string raw) {

	std::string cleanString = raw;
	char removeCheck[] = { '<', '>', '#', '"' };
	for (char chara : removeCheck)
	{
		if (raw.find(chara) != std::string::npos) {
			return "";
		}
	}
}