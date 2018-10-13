#include "stdafx.h"
#include "BinaryFile.h"
#include "Base64.h"

void PrintHelp(std::string PrintReason)
{
	std::cout << "\n";
	std::cout << PrintReason;
	std::cout << "\nHow to use the app:\n";
	std::cout << "Encoding file (no padding)  : b64 infile outfile encode_raw.\n";
	std::cout << "Encoding file (with padding): b64 infile outfile encode_padded.\n";
	std::cout << "Decoding file               : b64 infile outfile decode.\n";
}

int main(int argc, const char* argv[])
{
	for (int i = 0; i < argc; ++i)
	{
		std::string Arg(argv[i]);
		std::cout << "Arg #";
		std::cout << i;
		std::cout << " ";
		std::cout << Arg;
		std::cout << std::endl;
	}

	if (argc != 4)
	{
		PrintHelp("Incorrect number of arguments passed.\n");
		return 0;
	}

	//czytamy parametry wejsciowe

	std::string InFilePath(argv[1]);
	std::string OutFilePath(argv[2]);
	ConversionType Type = Base64Converter::CtFromString(argv[3]);
	if (Type == ConversionType::Undefined)
	{
		PrintHelp("Incorrect argument - type.\n");
		return 0;
	}

	//otwieramy plik wejsciowy

	BinaryFileIn InFile(InFilePath);
	if (!InFile.IsOK())
	{
		PrintHelp("Couldn't open file for reading.\n");
		return 0;
	}

	//otwieramy plik wyjsciowy

	BinaryFileOut OutFile(OutFilePath);
	if (!OutFile.IsOK())
	{
		PrintHelp("Couldn't open file for writing.\n");
		return 0;
	}

	//dokonujemy konwersji
	Base64Converter Converter(Type);
	Converter.FromFile(InFile, OutFile);
	return 0;
}
