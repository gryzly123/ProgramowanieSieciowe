#include "stdafx.h"
#include <bitset>
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
	static byte LookupTableFromBytes(byte In);
	static void ConvertBytes(byte* In, int InLength, byte*& Out, int& OutLength);
	static void File(std::ifstream In, std::ofstream Out);

};


int main()
{
	byte* bytes = new byte[3];
	byte* out; int outlen;
	bytes[0] = 'y';
	bytes[1] = 'D';
	bytes[2] = '!';
	Base64Converter::ConvertBytes(bytes, 3, out, outlen);
	std::cout << out[0] << out[1] << out[2] << out[3] << std::endl;
	std::cin >> outlen;
	return 0;
}




void Base64Converter::ConvertBytes(byte * In, int InLength, byte *& Out, int & OutLength)
{
	//while (InLength)
	//{
	//	if (InLength > 3)
	//	{
	//		//specjalny case
	//	}
	//	else
	//	{
			Out = new byte[4];
			OutLength = 4;
			
	///		Out[0] = LookupTableFromBytes(((*(In + 0)) >> 2));
	///		Out[1] = LookupTableFromBytes((((*(In + 0)) << 6) >> 2) && ((*(In + 1)) >> 4));
	///		Out[2] = LookupTableFromBytes(((*(In + 1)) >> 4 << 2) && ((*(In + 2)) << 6));
	///		Out[3] = LookupTableFromBytes((((*(In + 2)) << 6) >> 2) && ((*(In + 3)) << 2 >> 2));

			// 11001100 -> 00110011
			// 11001100 -> 00 + 
			// 11001100 -> 


			Out[0] = (( (*(In + 0)) >> 2));
			Out[1] = ((((*(In + 0)) << 6) >> 2) || ((*(In + 1)) >> 4));
			Out[2] = (( (*(In + 1)) << 4 >> 2)  || ((*(In + 2)) >> 6));
			Out[3] = ((((*(In + 2)) << 2) >> 2));


			std::cout << std::bitset<8>(In[0]) << std::endl;
			std::cout << std::bitset<8>(In[1]) << std::endl;
			std::cout << std::bitset<8>(In[2]) << std::endl;
			std::cout << std::bitset<8>(Out[0]) << std::endl;
			std::cout << std::bitset<8>(Out[1]) << std::endl;
			std::cout << std::bitset<8>(Out[2]) << std::endl;
			std::cout << std::bitset<8>(Out[3]) << std::endl;

			Out[0] = LookupTableFromBytes(Out[0]);
			Out[1] = LookupTableFromBytes(Out[1]);
			Out[2] = LookupTableFromBytes(Out[2]);
			Out[3] = LookupTableFromBytes(Out[3]);


			//InLength -= 3;
	//	}
	//}
}

byte Base64Converter::LookupTableFromBytes(byte In)
{
	if (In < 25) return In + 'A';
	if (In < 52) return In - 25 + 'a';
	if (In < 62) return In - 51 + '0';
	if (In == 62) return '+';
	if (In == 63) return '/';
	std::cout << "Error: byte was not 6 bits long";
	return '?';
}
