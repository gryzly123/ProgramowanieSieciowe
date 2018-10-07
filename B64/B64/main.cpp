#include "stdafx.h"
#include "BinaryFile.h"
#include "Base64.h"

#define USE_PADDING true
#define NO_PADDING false

int main()
{
	BinaryFileIn  In ("in.txt");
	BinaryFileOut Out_Padded("out_pad.txt");
	BinaryFileOut Out_Raw("out_raw.txt");

	Base64Converter Converter(USE_PADDING);
	Converter.FromFile(In, Out_Padded);
	Converter = Base64Converter(NO_PADDING);
	Converter.FromFile(In, Out_Raw);

	return 0;
}
