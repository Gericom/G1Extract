﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace G1Extract
{
	class Program
	{
		private static byte[] pal = {
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x17, 0x23, 0x23, 0x00, 0x23, 0x33, 0x33, 0x00,
			0x2F, 0x43, 0x43, 0x00, 0x3F, 0x53, 0x53, 0x00, 0x4B, 0x63, 0x63, 0x00,
			0x5B, 0x73, 0x73, 0x00, 0x6F, 0x83, 0x83, 0x00, 0x83, 0x97, 0x97, 0x00,
			0x9F, 0xAF, 0xAF, 0x00, 0xB7, 0xC3, 0xC3, 0x00, 0xD3, 0xDB, 0xDB, 0x00,
			0xEF, 0xF3, 0xF3, 0x00, 0x33, 0x2F, 0x00, 0x00, 0x3F, 0x3B, 0x00, 0x00,
			0x4F, 0x4B, 0x0B, 0x00, 0x5B, 0x5B, 0x13, 0x00, 0x6B, 0x6B, 0x1F, 0x00,
			0x77, 0x7B, 0x2F, 0x00, 0x87, 0x8B, 0x3B, 0x00, 0x97, 0x9B, 0x4F, 0x00,
			0xA7, 0xAF, 0x5F, 0x00, 0xBB, 0xBF, 0x73, 0x00, 0xCB, 0xCF, 0x8B, 0x00,
			0xDF, 0xE3, 0xA3, 0x00, 0x43, 0x2B, 0x07, 0x00, 0x57, 0x3B, 0x0B, 0x00,
			0x6F, 0x4B, 0x17, 0x00, 0x7F, 0x57, 0x1F, 0x00, 0x8F, 0x63, 0x27, 0x00,
			0x9F, 0x73, 0x33, 0x00, 0xB3, 0x83, 0x43, 0x00, 0xBF, 0x97, 0x57, 0x00,
			0xCB, 0xAF, 0x6F, 0x00, 0xDB, 0xC7, 0x87, 0x00, 0xE7, 0xDB, 0xA3, 0x00,
			0xF7, 0xEF, 0xC3, 0x00, 0x47, 0x1B, 0x00, 0x00, 0x5F, 0x2B, 0x00, 0x00,
			0x77, 0x3F, 0x00, 0x00, 0x8F, 0x53, 0x07, 0x00, 0xA7, 0x6F, 0x07, 0x00,
			0xBF, 0x8B, 0x0F, 0x00, 0xD7, 0xA7, 0x13, 0x00, 0xF3, 0xCB, 0x1B, 0x00,
			0xFF, 0xE7, 0x2F, 0x00, 0xFF, 0xF3, 0x5F, 0x00, 0xFF, 0xFB, 0x8F, 0x00,
			0xFF, 0xFF, 0xC3, 0x00, 0x23, 0x00, 0x00, 0x00, 0x4F, 0x00, 0x00, 0x00,
			0x5F, 0x07, 0x07, 0x00, 0x6F, 0x0F, 0x0F, 0x00, 0x7F, 0x1B, 0x1B, 0x00,
			0x8F, 0x27, 0x27, 0x00, 0xA3, 0x3B, 0x3B, 0x00, 0xB3, 0x4F, 0x4F, 0x00,
			0xC7, 0x67, 0x67, 0x00, 0xD7, 0x7F, 0x7F, 0x00, 0xEB, 0x9F, 0x9F, 0x00,
			0xFF, 0xBF, 0xBF, 0x00, 0x1B, 0x33, 0x13, 0x00, 0x23, 0x3F, 0x17, 0x00,
			0x2F, 0x4F, 0x1F, 0x00, 0x3B, 0x5F, 0x27, 0x00, 0x47, 0x6F, 0x2B, 0x00,
			0x57, 0x7F, 0x33, 0x00, 0x63, 0x8F, 0x3B, 0x00, 0x73, 0x9B, 0x43, 0x00,
			0x83, 0xAB, 0x4B, 0x00, 0x93, 0xBB, 0x53, 0x00, 0xA3, 0xCB, 0x5F, 0x00,
			0xB7, 0xDB, 0x67, 0x00, 0x1F, 0x37, 0x1B, 0x00, 0x2F, 0x47, 0x23, 0x00,
			0x3B, 0x53, 0x2B, 0x00, 0x4B, 0x63, 0x37, 0x00, 0x5B, 0x6F, 0x43, 0x00,
			0x6F, 0x87, 0x4F, 0x00, 0x87, 0x9F, 0x5F, 0x00, 0x9F, 0xB7, 0x6F, 0x00,
			0xB7, 0xCF, 0x7F, 0x00, 0xC3, 0xDB, 0x93, 0x00, 0xCF, 0xE7, 0xA7, 0x00,
			0xDF, 0xF7, 0xBF, 0x00, 0x0F, 0x3F, 0x00, 0x00, 0x13, 0x53, 0x00, 0x00,
			0x17, 0x67, 0x00, 0x00, 0x1F, 0x7B, 0x00, 0x00, 0x27, 0x8F, 0x07, 0x00,
			0x37, 0x9F, 0x17, 0x00, 0x47, 0xAF, 0x27, 0x00, 0x5B, 0xBF, 0x3F, 0x00,
			0x6F, 0xCF, 0x57, 0x00, 0x8B, 0xDF, 0x73, 0x00, 0xA3, 0xEF, 0x8F, 0x00,
			0xC3, 0xFF, 0xB3, 0x00, 0x4F, 0x2B, 0x13, 0x00, 0x63, 0x37, 0x1B, 0x00,
			0x77, 0x47, 0x2B, 0x00, 0x8B, 0x57, 0x3B, 0x00, 0xA7, 0x63, 0x43, 0x00,
			0xBB, 0x73, 0x53, 0x00, 0xCF, 0x83, 0x63, 0x00, 0xD7, 0x97, 0x73, 0x00,
			0xE3, 0xAB, 0x83, 0x00, 0xEF, 0xBF, 0x97, 0x00, 0xF7, 0xCF, 0xAB, 0x00,
			0xFF, 0xE3, 0xC3, 0x00, 0x0F, 0x13, 0x37, 0x00, 0x27, 0x2B, 0x57, 0x00,
			0x33, 0x37, 0x67, 0x00, 0x3F, 0x43, 0x77, 0x00, 0x53, 0x53, 0x8B, 0x00,
			0x63, 0x63, 0x9B, 0x00, 0x77, 0x77, 0xAF, 0x00, 0x8B, 0x8B, 0xBF, 0x00,
			0x9F, 0x9F, 0xCF, 0x00, 0xB7, 0xB7, 0xDF, 0x00, 0xD3, 0xD3, 0xEF, 0x00,
			0xEF, 0xEF, 0xFF, 0x00, 0x00, 0x1B, 0x6F, 0x00, 0x00, 0x27, 0x97, 0x00,
			0x07, 0x33, 0xA7, 0x00, 0x0F, 0x43, 0xBB, 0x00, 0x1B, 0x53, 0xCB, 0x00,
			0x2B, 0x67, 0xDF, 0x00, 0x43, 0x87, 0xE3, 0x00, 0x5B, 0xA3, 0xE7, 0x00,
			0x77, 0xBB, 0xEF, 0x00, 0x8F, 0xD3, 0xF3, 0x00, 0xAF, 0xE7, 0xFB, 0x00,
			0xD7, 0xF7, 0xFF, 0x00, 0x0B, 0x2B, 0x0F, 0x00, 0x0F, 0x37, 0x17, 0x00,
			0x17, 0x47, 0x1F, 0x00, 0x23, 0x53, 0x2B, 0x00, 0x2F, 0x63, 0x3B, 0x00,
			0x3B, 0x73, 0x4B, 0x00, 0x4F, 0x87, 0x5F, 0x00, 0x63, 0x9B, 0x77, 0x00,
			0x7B, 0xAF, 0x8B, 0x00, 0x93, 0xC7, 0xA7, 0x00, 0xAF, 0xDB, 0xC3, 0x00,
			0xCF, 0xF3, 0xDF, 0x00, 0x3F, 0x00, 0x5F, 0x00, 0x4B, 0x07, 0x73, 0x00,
			0x53, 0x0F, 0x7F, 0x00, 0x5F, 0x1F, 0x8F, 0x00, 0x6B, 0x2B, 0x9B, 0x00,
			0x7B, 0x3F, 0xAB, 0x00, 0x87, 0x53, 0xBB, 0x00, 0x9B, 0x67, 0xC7, 0x00,
			0xAB, 0x7F, 0xD7, 0x00, 0xBF, 0x9B, 0xE7, 0x00, 0xD7, 0xC3, 0xF3, 0x00,
			0xF3, 0xEB, 0xFF, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x57, 0x00, 0x00, 0x00,
			0x73, 0x00, 0x00, 0x00, 0x8F, 0x00, 0x00, 0x00, 0xAB, 0x00, 0x00, 0x00,
			0xC7, 0x00, 0x00, 0x00, 0xE3, 0x07, 0x00, 0x00, 0xFF, 0x07, 0x00, 0x00,
			0xFF, 0x4F, 0x43, 0x00, 0xFF, 0x7B, 0x73, 0x00, 0xFF, 0xAB, 0xA3, 0x00,
			0xFF, 0xDB, 0xD7, 0x00, 0x4F, 0x27, 0x00, 0x00, 0x6F, 0x33, 0x00, 0x00,
			0x93, 0x3F, 0x00, 0x00, 0xB7, 0x47, 0x00, 0x00, 0xDB, 0x4F, 0x00, 0x00,
			0xFF, 0x53, 0x00, 0x00, 0xFF, 0x6F, 0x17, 0x00, 0xFF, 0x8B, 0x33, 0x00,
			0xFF, 0xA3, 0x4F, 0x00, 0xFF, 0xB7, 0x6B, 0x00, 0xFF, 0xCB, 0x87, 0x00,
			0xFF, 0xDB, 0xA3, 0x00, 0x00, 0x33, 0x2F, 0x00, 0x00, 0x3F, 0x37, 0x00,
			0x00, 0x4B, 0x43, 0x00, 0x00, 0x57, 0x4F, 0x00, 0x07, 0x6B, 0x63, 0x00,
			0x17, 0x7F, 0x77, 0x00, 0x2B, 0x93, 0x8F, 0x00, 0x47, 0xA7, 0xA3, 0x00,
			0x63, 0xBB, 0xBB, 0x00, 0x83, 0xCF, 0xCF, 0x00, 0xAB, 0xE7, 0xE7, 0x00,
			0xCF, 0xFF, 0xFF, 0x00, 0x3F, 0x00, 0x1B, 0x00, 0x67, 0x00, 0x33, 0x00,
			0x7B, 0x0B, 0x3F, 0x00, 0x8F, 0x17, 0x4F, 0x00, 0xA3, 0x1F, 0x5F, 0x00,
			0xB7, 0x27, 0x6F, 0x00, 0xDB, 0x3B, 0x8F, 0x00, 0xEF, 0x5B, 0xAB, 0x00,
			0xF3, 0x77, 0xBB, 0x00, 0xF7, 0x97, 0xCB, 0x00, 0xFB, 0xB7, 0xDF, 0x00,
			0xFF, 0xD7, 0xEF, 0x00, 0x27, 0x13, 0x00, 0x00, 0x37, 0x1F, 0x07, 0x00,
			0x47, 0x2F, 0x0F, 0x00, 0x5B, 0x3F, 0x1F, 0x00, 0x6B, 0x53, 0x33, 0x00,
			0x7B, 0x67, 0x4B, 0x00, 0x8F, 0x7F, 0x6B, 0x00, 0xA3, 0x93, 0x7F, 0x00,
			0xBB, 0xAB, 0x93, 0x00, 0xCF, 0xC3, 0xAB, 0x00, 0xE7, 0xDB, 0xC3, 0x00,
			0xFF, 0xF3, 0xDF, 0x00, 0x37, 0x4B, 0x4B, 0x00, 0xFF, 0xB7, 0x00, 0x00,
			0xFF, 0xDB, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0x07, 0x6B, 0x63, 0x00,
			0x27, 0x8F, 0x87, 0x00, 0x1B, 0x83, 0x7B, 0x00, 0x07, 0x6B, 0x63, 0x00,
			0x07, 0x6B, 0x63, 0x00, 0x37, 0x9B, 0x97, 0x00, 0x9B, 0xE3, 0xE3, 0x00,
			0x73, 0xCB, 0xCB, 0x00, 0x37, 0x9B, 0x97, 0x00, 0x37, 0x9B, 0x97, 0x00,
			0x43, 0x5B, 0x5B, 0x00, 0x53, 0x6B, 0x6B, 0x00, 0x63, 0x7B, 0x7B, 0x00,
			0x2F, 0x2F, 0x2F, 0x00, 0x2F, 0x2F, 0x2F, 0x00, 0x57, 0x47, 0x2F, 0x00,
			0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
			0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
			0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
			0xFF, 0xFF, 0xFF, 0x00
		};

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage:");
				Console.WriteLine("G1Extract.exe [g1path] [outdir]");
				return;
			}
			byte[] data = File.ReadAllBytes(args[0]);
			EndianBinaryReader er = new EndianBinaryReader(new MemoryStream(data), Endianness.LittleEndian);
			uint nrimg = er.ReadUInt32();
			uint datalength = er.ReadUInt32();
			for (int i = 0; i < nrimg; i++)
			{
				uint start = er.ReadUInt32();
				short width = er.ReadInt16();
				short height = er.ReadInt16();
				short xoff = er.ReadInt16();
				short yoff = er.ReadInt16();
				ushort flags = er.ReadUInt16();
				ushort padding = er.ReadUInt16();

				long curpos = er.BaseStream.Position;
				er.BaseStream.Position = start + nrimg * 16 + 8;
				switch (flags & 0xF)
				{
					case 1:
						{
							Bitmap b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
							ColorPalette pall = b.Palette;
							for (int j = 0; j < 256; j++)
								pall.Entries[j] = Color.FromArgb(pal[j * 4], pal[j * 4 + 1], pal[j * 4 + 2]);
							pall.Entries[0] = Color.Transparent;
							b.Palette = pall;
							BitmapData d = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
							byte[] dat = er.ReadBytes(width * height);
							for (int y = 0; y < height; y++)
							{
								Marshal.Copy(dat, y * width, d.Scan0 + y * d.Stride, width);
							}
							b.UnlockBits(d);
							b.Save(args[1] + "\\" + string.Format("{0:x8}", i) + ".png");
							break;
						}
					case 5:
						{
							Bitmap b = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
							ColorPalette pall = b.Palette;
							for (int j = 0; j < 256; j++)
								pall.Entries[j] = Color.FromArgb(pal[j * 4], pal[j * 4 + 1], pal[j * 4 + 2]);
							pall.Entries[0] = Color.Transparent;
							b.Palette = pall;
							BitmapData d = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
							long basepos = er.BaseStream.Position;
							for (int y = 0; y < height; y++)
							{
								er.BaseStream.Position = basepos + y * 2;
								ushort offs = er.ReadUInt16();
								er.BaseStream.Position = basepos + offs;
								while (true)
								{
									byte length = er.ReadByte();
									byte xoffset = er.ReadByte();
									byte[] dat = er.ReadBytes(length & 0x7F);
									Marshal.Copy(dat, 0, d.Scan0 + y * d.Stride + xoffset, length & 0x7F);
									if ((length & 0x80) != 0) break;
								}
							}
							b.UnlockBits(d);
							b.Save(args[1] + "\\" + string.Format("{0:x8}", i) + ".png");
							break;
						}
					case 8:
						//palettes are currently not exported!
						Console.WriteLine("Skipped palette (" + string.Format("{0:x8}", i) + ")");
						break;
					default:
						break;
				}
				er.BaseStream.Position = curpos;
			}
			er.Close();
		}
	}
}
