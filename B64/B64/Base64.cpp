#include "Base64.h"
#include "BinaryFile.h"

Base64Converter::Base64Converter(bool UsePadding)
	: UsePadding(UsePadding) { }

void Base64Converter::FromFile(class BinaryFileIn& In, class BinaryFileOut& Out)
{
	byte* InBytes = new byte[3];
	byte* OutBytes = new byte[4];
	int64 InLen = 3;
	int64 OutLen;
	In.Reset();
	Out.Reset();

	while (In.NextBytes(InBytes, InLen))
	{
		RawConvertChunk(InBytes, InLen, OutBytes, OutLen);
		Out.NextBytes(OutBytes, OutLen);
	}

	if (UsePadding)
	{
		OutBytes[0] = OutBytes[1] = '=';
		OutLen = 4 - OutLen;
		Out.NextBytes(OutBytes, OutLen);
	}

	delete[] InBytes;
	delete[] OutBytes;
}

void Base64Converter::RawConvertChunk(byte* In, int64 InLength, byte *& Out, int64& OutLength)
{
#define BYTE1 (*(In + 0))
#define BYTE2 (*(In + 1))
#define BYTE3 (*(In + 2))

	switch (InLength)
	{
	case 3:
		OutLength = 4;

		Out[0] = LookupTableFromBytes(
			BYTE1 >> 2);                 //szeœæ pierwszych bitów pierwszego bajtu

		Out[1] = LookupTableFromBytes(
			((byte)(BYTE1 & 0x03) << 4)    //pozosta³e dwa bity pierwszego bajtu
			| (BYTE2 >> 4));             //pierwsze cztery bity drugiego bajtu

		Out[2] = LookupTableFromBytes(
			((byte)(BYTE2 & 0x0F) << 2)  //pozosta³e cztery bity drugiego bajtu
			| ((BYTE3 >> 6)/* & 0x03*/));    //pierwsze dwa bity trzeciego bajtu

		Out[3] = LookupTableFromBytes(
			BYTE3 & 0x3F);               //pozosta³e szeœæ bitów trzeciego bajtu


		//std::cout << std::bitset<8>(In[0]) << std::endl;
		//std::cout << std::bitset<8>(In[1]) << std::endl;
		//std::cout << std::bitset<8>(In[2]) << std::endl << std::endl;
		//std::cout << std::bitset<8>(Out[0]) << std::endl;
		//std::cout << std::bitset<8>(Out[1]) << std::endl;
		//std::cout << std::bitset<8>(Out[2]) << std::endl;
		//std::cout << std::bitset<8>(Out[3]) << std::endl << std::endl;

		return;

	case 2:
		OutLength = 3;

		Out[0] = LookupTableFromBytes(
			BYTE1 >> 2);                  //szeœæ pierwszych bitów pierwszego bajtu

		Out[1] = LookupTableFromBytes(
			((byte)(BYTE1 & 0x03) << 4)     //pozosta³e dwa bity pierwszego bajtu
			| (BYTE2 >> 4));              //pierwsze cztery bity drugiego bajtu

		Out[2] = LookupTableFromBytes(
			((byte)(BYTE2 & 0x0F) << 2)); //pozosta³e cztery bity drugiego bajtu

		return;

	case 1:
		OutLength = 2;

		Out[0] = LookupTableFromBytes(
			BYTE1 >> 2);                  //szeœæ pierwszych bitów pierwszego bajtu

		Out[1] = LookupTableFromBytes(
			((byte)(BYTE1 << 6) >> 2));     //pozosta³e dwa bity pierwszego bajtu

		return;
	}
}

byte Base64Converter::LookupTableFromBytes(byte In)
{
	//return In;
	if (In < 26) return In + 'A';
	if (In < 52) return In - 26 + 'a';
	if (In < 62) return In - 52 + '0';
	if (In == 62) return '+';
	if (In == 63) return '/';
	std::cout << "Error: byte was not 6 bits long";
	return '?';
}
