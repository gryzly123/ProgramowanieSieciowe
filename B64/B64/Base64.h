#pragma once
#include "stdafx.h"

enum class ConversionType : uint8_t
{
	Encode_Padded,
	Encode_Raw,
	Decode,
	Undefined
};

class Base64Converter
{
public:
	Base64Converter(ConversionType Type);
	void FromFile(class BinaryFileIn& In, class BinaryFileOut& Out);
	static ConversionType CtFromString(std::string Type);
	
private:
	ConversionType CT;

	void RawEncodeChunk(byte* In, int64 InLength, byte*& Out, int64& OutLength);
	void RawDecodeChunk(byte* In, int64 InLength, byte*& Out, int64& OutLength);
	inline byte EncodeLookupTable(byte In);
	inline byte DecodeLookupTable(byte In);
};