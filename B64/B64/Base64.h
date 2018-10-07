#pragma once
#include "stdafx.h"

class Base64Converter
{
public:
	Base64Converter(bool UsePadding);
	void FromFile(class BinaryFileIn& In, class BinaryFileOut& Out);
	
private:
	bool UsePadding;
	void RawConvertChunk(byte* In, int64 InLength, byte*& Out, int64& OutLength);
	byte LookupTableFromBytes(byte In);

};