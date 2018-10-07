#pragma once
#include "stdafx.h"

class BinaryFile
{
public:
	virtual bool NextBytes(byte*& Bytes, int64& Length) = 0;
	virtual bool IsOK() = 0;
	virtual bool Reset() = 0;

protected:
	int64 CurrentPos = 0;
};


class BinaryFileIn : public BinaryFile
{
public:
	BinaryFileIn(std::string Path);
	~BinaryFileIn();

	virtual bool NextBytes(byte*& Bytes, int64& Length) override;
	virtual bool IsOK()  override;
	virtual bool Reset() override;
	bool EndReached();

private:
	int64 FileSize;
	std::ifstream FileStream;
};

class BinaryFileOut : public BinaryFile
{
public:
	BinaryFileOut(std::string Path);
	~BinaryFileOut();

	virtual bool NextBytes(byte*& Bytes, int64& Length) override;
	virtual bool IsOK()  override;
	virtual bool Reset() override;
	
private:
	std::ofstream FileStream;
};
