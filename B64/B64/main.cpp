#include "stdafx.h"
typedef char byte;

//class BinaryFileIn
//{
//public:
//	BinaryFileIn(std::string Path);
//
//	bool GetNextBytes(uint8_t*& Bytes, int& Length);
//	bool EndReached();
//
//	bool IsOpened = false;
//private:
//	std::ifstream FileStream;
//};
//
//class BinaryFileOut
//{
//public:
//	BinaryFileOut(std::string Path);
//
//	bool SetNextBytes(uint8_t* Bytes, int Length);
//	bool IsAvailable = false;
//
//private:
//	std::ofstream FileStream;
//};

class Base64Converter
{
public:
	static void File(std::ifstream& In, std::ofstream& Out);
	static byte LookupTableFromBytes(byte In);
	static void ConvertBytes(byte* In, int InLength, byte*& Out, int& OutLength);

};


int main()
{
	std::ifstream In("in.txt", std::ifstream::binary);
	std::ofstream Ou("ou.txt", std::ofstream::binary);
	Base64Converter::File(In, Ou);
	int a;
	return 0;
}




void Base64Converter::File(std::ifstream& In, std::ofstream& Out)
{
	FILE* F = new FILE();

	byte* InBytes = new byte[3];
	byte* OutBytes = new byte[4];
	int InLen, OutLen;
	In.seekg(0);
	
	while (!In.eof())
	{
		In.read(InBytes, 3);
		ConvertBytes(InBytes, 3, OutBytes, OutLen);
		Out.write(OutBytes, OutLen);
	}

	delete[] InBytes;
	delete[] OutBytes;
	Out.flush();
}

void Base64Converter::ConvertBytes(byte* In, int InLength, byte *& Out, int & OutLength)
{
#define BYTE1 (*(In + 0))
#define BYTE2 (*(In + 1))
#define BYTE3 (*(In + 2))
	
	switch (InLength)
	{
	case 3:
			OutLength = 4;

			Out[0] = LookupTableFromBytes(
				BYTE1 >> 2);                 //sześć pierwszych bitów pierwszego bajtu
			
			Out[1] = LookupTableFromBytes(
				((byte)(BYTE1 & 0x03) << 4)    //pozostałe dwa bity pierwszego bajtu
				| (BYTE2 >> 4));             //pierwsze cztery bity drugiego bajtu
			
			Out[2] = LookupTableFromBytes(
				((byte)(BYTE2 & 0x0F) << 2)  //pozostałe cztery bity drugiego bajtu
				| ((BYTE3 >> 6)/* & 0x03*/));    //pierwsze dwa bity trzeciego bajtu
			
			Out[3] = LookupTableFromBytes(
				BYTE3 & 0x3F);               //pozostałe sześć bitów trzeciego bajtu


			std::cout << std::bitset<8>(In[0]) << std::endl;
			std::cout << std::bitset<8>(In[1]) << std::endl;
			std::cout << std::bitset<8>(In[2]) << std::endl << std::endl;
			std::cout << std::bitset<8>(Out[0]) << std::endl;
			std::cout << std::bitset<8>(Out[1]) << std::endl;
			std::cout << std::bitset<8>(Out[2]) << std::endl;
			std::cout << std::bitset<8>(Out[3]) << std::endl << std::endl;



			return;

	case 2:
			OutLength = 3;

			Out[0] = LookupTableFromBytes(
				BYTE1 >> 2);                  //sześć pierwszych bitów pierwszego bajtu
			
			Out[1] = LookupTableFromBytes(
				((byte)(BYTE1 & 0x03) << 4)     //pozostałe dwa bity pierwszego bajtu
				| (BYTE2 >> 4));              //pierwsze cztery bity drugiego bajtu
			
			Out[2] = LookupTableFromBytes(
				((byte)(BYTE2 & 0x0F) << 2)); //pozostałe cztery bity drugiego bajtu

			return;

	case 1:
		OutLength = 2;

		Out[0] = LookupTableFromBytes(
			BYTE1 >> 2);                  //sześć pierwszych bitów pierwszego bajtu

		Out[1] = LookupTableFromBytes(
			((byte)(BYTE1 << 6) >> 2));     //pozostałe dwa bity pierwszego bajtu

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
