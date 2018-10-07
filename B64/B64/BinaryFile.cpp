#include "BinaryFile.h"

BinaryFileIn::BinaryFileIn(std::string Path)
{
	FileStream = std::ifstream(Path, std::ios::binary | std::ios::ate);
	FileSize = FileStream.tellg(); //dziêki ios::ate jesteœmy od razu na koñcu pliku, wiêc sprawdzamy
	FileStream.seekg(0);           //jego wielkoœæ poprzez tell() i wracamy na pocz¹tek poprzez seek()
}

BinaryFileIn::~BinaryFileIn()
{
	FileStream.close();
}

bool BinaryFileIn::NextBytes(byte*& Bytes, int64& Length)
{
	if (!Bytes || EndReached()) return false;
	if (CurrentPos + Length > FileSize)
		Length = FileSize - CurrentPos;
	FileStream.read((char*)Bytes, Length);
	CurrentPos += Length;
	return true;
}

bool BinaryFileIn::IsOK()
{
	return !FileStream.bad();
}

bool BinaryFileIn::Reset()
{
	FileStream.seekg(0);
	CurrentPos = 0;
	return true;
}

bool BinaryFileIn::EndReached()
{
	return (CurrentPos >= FileSize);
}

BinaryFileOut::BinaryFileOut(std::string Path)
{
	FileStream = std::ofstream(Path, std::ios::binary);
}

BinaryFileOut::~BinaryFileOut()
{
	FileStream.flush();
	FileStream.close();
}

bool BinaryFileOut::NextBytes(byte*& Bytes, int64& Length)
{
	if (!Bytes || !IsOK()) return false;
	FileStream.write((char*)Bytes, Length);
	return true;
}

bool BinaryFileOut::IsOK()
{
	return FileStream.is_open();
}

bool BinaryFileOut::Reset()
{
	CurrentPos = 0;
	return true;
}
