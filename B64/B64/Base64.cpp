#include "Base64.h"
#include "BinaryFile.h"

Base64Converter::Base64Converter(ConversionType Type)
	: CT(Type) { }

void Base64Converter::FromFile(class BinaryFileIn& In, class BinaryFileOut& Out)
{
	byte* InBytes;
	byte* OutBytes;
	int64 InLen, OutLen;
	In.Reset();
	Out.Reset();

	switch (CT)
	{
	case ConversionType::Decode:
		InLen = 4;
		InBytes = new byte[4];
		OutBytes = new byte[3];

		while (In.NextBytes(InBytes, InLen))
		{
			RawDecodeChunk(InBytes, InLen, OutBytes, OutLen);
			Out.NextBytes(OutBytes, OutLen);
		}
		break;

	case ConversionType::Encode_Raw:
		InLen = 3;
		InBytes = new byte[3];
		OutBytes = new byte[4];

		while (In.NextBytes(InBytes, InLen))
		{
			RawEncodeChunk(InBytes, InLen, OutBytes, OutLen);
			Out.NextBytes(OutBytes, OutLen);
		}
		break;

	case ConversionType::Encode_Padded:
		InLen = 3;
		InBytes = new byte[3];
		OutBytes = new byte[4];

		while (In.NextBytes(InBytes, InLen))
		{
			RawEncodeChunk(InBytes, InLen, OutBytes, OutLen);
			Out.NextBytes(OutBytes, OutLen);
		}

		OutBytes[0] = OutBytes[1] = '=';
		OutLen = 4 - OutLen;
		Out.NextBytes(OutBytes, OutLen);
		break;

	case ConversionType::Undefined:
		std::cout << "Internal error - conversion type was undefined.\n";
		return;
	}

	delete[] InBytes;
	delete[] OutBytes;
}

ConversionType Base64Converter::CtFromString(std::string Type)
{
		if (Type.compare("encode_raw") == 0) return ConversionType::Encode_Raw;
		if (Type.compare("encode_padded") == 0) return ConversionType::Encode_Padded;
		if (Type.compare("decode") == 0) return ConversionType::Decode;
		return ConversionType::Undefined;
}

#define BYTE1 (*(In + 0))
#define BYTE2 (*(In + 1))
#define BYTE3 (*(In + 2))
#define BYTE4 (*(In + 3))

void Base64Converter::RawEncodeChunk(byte* In, int64 InLength, byte *& Out, int64& OutLength)
{
	switch (InLength)
	{
	case 3:
		OutLength = 4;

		//sześć pierwszych bitów pierwszego bajtu
		Out[0] = EncodeLookupTable(0x3F & (BYTE1 >> 2)                                  );

		//pozostałe dwa bity pierwszego bajtu | pierwsze cztery bity drugiego bajtu
		Out[1] = EncodeLookupTable(0x3F & (((byte)(BYTE1 & 0x03) << 4) | (BYTE2 >> 4))  );

		//pozostałe cztery bity drugiego bajtu | pierwsze dwa bity trzeciego bajtu
		Out[2] = EncodeLookupTable(0x3F & (((byte)(BYTE2 & 0x0F) << 2) | ((BYTE3 >> 6))));

		//pozostałe sześć bitów trzeciego bajtu
		Out[3] = EncodeLookupTable(0x3F & (BYTE3)                                       );
		//getchar();

		return;

	case 2:
		OutLength = 3;

		Out[0] = EncodeLookupTable(0x3F & (BYTE1 >> 2));
		Out[1] = EncodeLookupTable(0x3F & (((byte)(BYTE1 & 0x03) << 4) | (BYTE2 >> 4)));
		Out[2] = EncodeLookupTable(0x3F & (((byte)(BYTE2 & 0x0F) << 2)));
		return;

	case 1:
		OutLength = 2;

		Out[0] = EncodeLookupTable(0x3F & (BYTE1 >> 2));
		Out[1] = EncodeLookupTable(0x3F & (((byte)(BYTE1 & 0x03) << 4)));
		return;
	}
}

void Base64Converter::RawDecodeChunk(byte* In, int64 InLength, byte *& Out, int64& OutLength)
{
	if (InLength == 4)
	{
		if (In[3] == '=') InLength = 3;
		if (In[2] == '=') InLength = 2;
	}

	switch (InLength)
	{
	case 4:
		OutLength = 3;

		In[0] = DecodeLookupTable(In[0]);
		In[1] = DecodeLookupTable(In[1]);
		In[2] = DecodeLookupTable(In[2]);
		In[3] = DecodeLookupTable(In[3]);
		Out[0] = ((BYTE1 << 2) | (BYTE2 >> 4));
		Out[1] = ((byte)(BYTE2) << 4) | (BYTE3 >> 2);
		Out[2] = ((byte)(BYTE3) << 6) | ((BYTE4));
		return;

	case 3:
		OutLength = 2;

		In[0] = DecodeLookupTable(In[0]);
		In[1] = DecodeLookupTable(In[1]);
		In[2] = DecodeLookupTable(In[2]);
		Out[0] = ((BYTE1 << 2) | (BYTE2 >> 4));
		Out[1] = ((byte)(BYTE2) << 4) | (BYTE3 >> 2);
		return;

	case 2:
		OutLength = 1;

		In[0] = DecodeLookupTable(In[0]);
		In[1] = DecodeLookupTable(In[1]);
		Out[0] = ((BYTE1 << 2) | (BYTE2 >> 4));
		return;

	default:
		std::cout << "Error - incorrect input file length.\n";
		return;
	}
}

byte Base64Converter::EncodeLookupTable(byte In)
{
	if (In < 26)  return In + 'A';
	if (In < 52)  return In - 26 + 'a';
	if (In < 62)  return In - 52 + '0';
	if (In == 62) return '+';
	if (In == 63) return '/';
	std::cout << "Error: byte was not 6 bits long\n";
	return '?';
}

byte Base64Converter::DecodeLookupTable(byte In)
{
	if (In >= 'A' && In <= 'Z') return In - 'A';
	if (In >= 'a' && In <= 'z') return In + 26 - 'a';
	if (In >= '0' && In <= '9') return In + 52 - '0';
	if (In == '+')              return 62;
	if (In == '/')              return 63;
	std::cout << "Error: byte " << (int)In << " was not a valid character\n";
	return '?';
}
