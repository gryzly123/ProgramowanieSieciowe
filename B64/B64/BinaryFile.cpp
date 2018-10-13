#include "BinaryFile.h"

BinaryFileIn::BinaryFileIn(std::string Path)
{
	FileStream = std::ifstream(Path, std::ios::binary | std::ios::ate);
	FileSize = FileStream.tellg(); //dzi�ki ios::ate jeste�my od razu na ko�cu pliku, wi�c sprawdzamy
	FileStream.seekg(0);           //jego wielko�� poprzez tell() i wracamy na pocz�tek poprzez seek()
	if (FileSize == 0) std::cout << "BinaryFile - warning - input file is empty";
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
	return !(FileStream.bad() || FileStream.fail());
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
