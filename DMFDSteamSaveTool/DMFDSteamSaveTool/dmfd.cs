using System;
using System.Text;

namespace DMFDSteamSaveTool
{
    public class dmfd
    {
        public static void Encrypt(string password, ref byte[] fileContents)
        {
            uint size = (uint)fileContents.Length;
            uint numeric_0 = 0xCAD705B2;
            uint numeric_1 = 0xA1B34F58;

            byte[] passBytes = Encoding.UTF8.GetBytes(password);

            uint[] results = new uint[] { numeric_0, numeric_1 };

            // loop through password
            for (int i = 0; i < password.Length; i++)
            {
                numeric_0 = (uint)passBytes[i] + results[0];
                results = __allmul(numeric_0, numeric_1, 0x8d, 0);
                numeric_0 = results[0];
                numeric_1 = results[1];
            }

            // encrypt the data
            byte encryptedChar = 0;
            for (int i = 0, counter = 0; i < size; i++)
            {
                counter = i & 0x1F;
                results = __aullshr(numeric_0, numeric_1, counter);
                // xor the bytes
                encryptedChar = fileContents[i];
                encryptedChar ^= (byte)(results[0] & 0xFF);
                fileContents[i] = encryptedChar;

                numeric_0 += encryptedChar;
                results = __allmul(numeric_0, numeric_1, 0x8d, 0);
                numeric_0 = results[0];
                numeric_1 = results[1];
            }

        }


        public static void Decrypt(string password, ref byte[] fileContents)
        {
            uint size = (uint)fileContents.Length;
            uint numeric_0 = 0xCAD705B2;
            uint numeric_1 = 0xA1B34F58;

            byte[] passBytes = Encoding.UTF8.GetBytes(password);

            uint[] results = new uint[] { numeric_0, numeric_1 };

            // loop through password
            for (int i = 0; i < password.Length; i++)
            {
                numeric_0 = (uint)passBytes[i] + results[0];
                results = __allmul(numeric_0, numeric_1, 0x8d, 0);
                numeric_0 = results[0];
                numeric_1 = results[1];
            }

            // decrypt data
            byte encryptedChar = 0;
            for (int i = 0, counter = 0; i < size; i++)
            {
                counter = i & 0x1F;
                results = __aullshr(numeric_0, numeric_1, counter);

                // xor the bytes
                encryptedChar = fileContents[i];
                fileContents[i] ^= (byte)(results[0] & 0xFF);

                numeric_0 += encryptedChar;
                results = __allmul(numeric_0, numeric_1, 0x8d, 0);
                numeric_0 = results[0];
                numeric_1 = results[1];
            }

        }

        public static uint Checksum(byte[] data)
        {
            uint result = 0;

            for (int i = 0; i < data.Length; i++)
            {
                result = result + data[i];
                result = ((result >> 0x10) | result) & 0xFFFF;
            }

            return result;
        }

        public static string ObfuscateString(string fileName)
        {
            uint length = 0;
            uint lengthMultiplied = 0;

            uint itsZero = 0;
            uint anotherZero = 0;


            byte[] stringBytes = Encoding.UTF8.GetBytes(fileName);


            if (stringBytes.Length >= 0x40)
            {
                // call the obfuscate function
                stringBytes = Encoding.UTF8.GetBytes(CursedObfuscationFunction(stringBytes, (uint)stringBytes.Length));
            }

            lengthMultiplied = (uint)(stringBytes.Length * 8);

            byte[] obfuscatedStringBytes = new byte[0x80];
            Array.Copy(stringBytes, obfuscatedStringBytes, stringBytes.Length);

            length = (uint)(stringBytes.Length & 0x3F); // irrelevant?

            obfuscatedStringBytes[stringBytes.Length] = 0x80;
            obfuscatedStringBytes[0x38] = (byte)lengthMultiplied;

            if (stringBytes.Length >= 0x38)
            {
                obfuscatedStringBytes[0x78] = (byte)lengthMultiplied;
                obfuscatedStringBytes[0x79] = (byte)Shrd(lengthMultiplied, anotherZero, 0x8);
                obfuscatedStringBytes[0x7A] = (byte)Shrd(lengthMultiplied, anotherZero, 0x10);
                obfuscatedStringBytes[0x7B] = (byte)Shrd(lengthMultiplied, anotherZero, 0x18);
                obfuscatedStringBytes[0x7C] = (byte)itsZero;
                obfuscatedStringBytes[0x7D] = (byte)(anotherZero >> 8);
                obfuscatedStringBytes[0x7E] = (byte)(anotherZero >> 10);
                obfuscatedStringBytes[0x7F] = (byte)(anotherZero >> 18);
                length = 0x80;
            }

            else
            {
                obfuscatedStringBytes[0x38] = (byte)lengthMultiplied;
                obfuscatedStringBytes[0x39] = (byte)Shrd(lengthMultiplied, anotherZero, 0x8);
                obfuscatedStringBytes[0x3A] = (byte)Shrd(lengthMultiplied, anotherZero, 0x10);
                obfuscatedStringBytes[0x3B] = (byte)Shrd(lengthMultiplied, anotherZero, 0x18);
                obfuscatedStringBytes[0x3C] = (byte)itsZero;
                obfuscatedStringBytes[0x3D] = (byte)(anotherZero >> 8);
                obfuscatedStringBytes[0x3E] = (byte)(anotherZero >> 10);
                obfuscatedStringBytes[0x3F] = (byte)(anotherZero >> 18);
                length = 0x40;
            }

            return CursedObfuscationFunction(obfuscatedStringBytes, length);
        }


        static string CursedObfuscationFunction(byte[] input, uint length)
        {
            uint lengthShifted = length >> 0x6;

            //if (lengthShifted == 0) return "nope";

            byte[] outputBytes = input;

            uint num0 = 0x67452301;
            uint num1 = 0xEFCDAB89;
            uint num2 = 0x98BADCFE;
            uint num3 = 0x10325476;

            uint counter = 0; // EBP-0xC

            uint ecx = 0;
            uint ebx = 0;
            uint edx = 0;
            uint esi = 0;
            uint edi = 0;

            uint savedEBX = 0;
            uint savedECX = 0;
            uint savedEDX = 0;
            uint savedESI = 0;
            uint savedEDI = 0;

            for (counter = 0; counter < lengthShifted; counter++)
            {
                ecx = (((num3 ^ num2) & num1) ^ num3) + 0xD76AA478 + BitConverter.ToUInt32(input, 0);

                ebx = RotateLeft(ecx + num0, 0x7) + num1;
                ecx = (((num2 ^ num1) & ebx) ^ num2) + 0xE8C7B756 + BitConverter.ToUInt32(input, 4);

                edx = RotateLeft(ecx + num3, 0xC) + ebx;
                ecx = (((num1 ^ ebx) & edx) ^ num1) + 0x242070DB + BitConverter.ToUInt32(input, 8);

                esi = RotateRight(num2 + ecx, 0xF) + edx;
                ecx = (((edx ^ ebx) & esi) ^ ebx) + 0xC1BDCEEE + BitConverter.ToUInt32(input, 0xC);

                edi = RotateRight(ecx + num1, 0xA) + esi;
                ecx = (((edx ^ esi) & edi) ^ edx) + 0xF57C0FAF + BitConverter.ToUInt32(input, 0x10);

                ebx = RotateLeft(ebx + ecx, 0x7) + edi;
                ecx = (((esi ^ edi) & ebx) ^ esi) + 0x4787C62A + BitConverter.ToUInt32(input, 0x14);

                edx = RotateLeft(edx + ecx, 0xC) + ebx;
                ecx = (((edi ^ ebx) & edx) ^ edi) + 0xA8304613 + BitConverter.ToUInt32(input, 0x18);

                esi = RotateRight(esi + ecx, 0xF) + edx;
                ecx = (((edx ^ ebx) & esi) ^ ebx) + 0xFD469501 + BitConverter.ToUInt32(input, 0x1C);

                edi = RotateRight(ecx + edi, 0xA) + esi;
                ecx = (((edx ^ esi) & edi) ^ edx) + 0x698098D8 + BitConverter.ToUInt32(input, 0x20);

                ebx = RotateLeft(ebx + ecx, 0x7) + edi;
                ecx = (((esi ^ edi) & ebx) ^ esi) + 0x8B44F7AF + BitConverter.ToUInt32(input, 0x24);

                edx = RotateLeft(edx + ecx, 0xC) + ebx;
                ecx = (((edi ^ ebx) & edx) ^ edi) + 0xFFFF5BB1 + BitConverter.ToUInt32(input, 0x28);

                esi = RotateRight(esi + ecx, 0xF) + edx;
                ecx = (((edx ^ ebx) & esi) ^ ebx) + 0x895CD7BE + BitConverter.ToUInt32(input, 0x2C);

                savedESI = esi;
                edi = RotateRight(edi + ecx, 0xA) + esi;
                ecx = (((edx ^ esi) & edi) ^ edx) + BitConverter.ToUInt32(input, 0x30); ;
                savedEDI = edi;

                esi = RotateLeft(ebx + 0x6B901122 + ecx, 0x7) + edi;
                ecx = (((savedESI ^ edi) & esi) ^ savedESI) + BitConverter.ToUInt32(input, 0x34);
                ebx = savedESI + 0xA679438E;

                edi = RotateLeft(edx - 0x2678E6D + ecx, 0xC) + esi;
                ecx = (((savedEDI ^ esi) & edi) ^ savedEDI) + BitConverter.ToUInt32(input, 0x38);
                edx = savedEDI + 0x49B40821;

                ebx = RotateRight(ebx + ecx, 0xF) + edi;
                ecx = (((edi ^ esi) & ebx) ^ esi) + BitConverter.ToUInt32(input, 0x3C);

                edx = RotateRight(edx + ecx, 0xA) + ebx;
                ecx = (((ebx ^ edx) & edi) ^ ebx) + 0xF61E2562 + BitConverter.ToUInt32(input, 0x4);

                esi = RotateLeft(esi + ecx, 0x5) + edx;
                ecx = (((edx ^ esi) & ebx) ^ edx) + 0xC040B340 + BitConverter.ToUInt32(input, 0x18);

                edi = RotateLeft(edi + ecx, 0x9) + esi;
                ecx = (((edi ^ esi) & edx) ^ esi) + 0x265E5A51 + BitConverter.ToUInt32(input, 0x2C);

                ebx = (RotateLeft(ebx + ecx, 0xE)) + edi;
                ecx = (((edi ^ ebx) & esi) ^ edi) + 0xE9B6C7AA + BitConverter.ToUInt32(input, 0x0);

                edx = RotateRight(edx + ecx, 0xC) + ebx;
                ecx = (((ebx ^ edx) & edi) ^ ebx) + 0xD62F105D + BitConverter.ToUInt32(input, 0x14);

                esi = (RotateLeft(esi + ecx, 0x5)) + edx;
                ecx = (((edx ^ esi) & ebx) ^ edx) + 0x2441453 + BitConverter.ToUInt32(input, 0x28);

                edi = RotateLeft(edi + ecx, 0x9) + esi;
                ecx = (((edi ^ esi) & edx) ^ esi) + 0xD8A1E681 + BitConverter.ToUInt32(input, 0x3C);

                ebx = (RotateLeft(ebx + ecx, 0xE)) + edi;
                ecx = (((edi ^ ebx) & esi) ^ edi) + 0xE7D3FBC8 + BitConverter.ToUInt32(input, 0x10);

                edx = RotateRight(edx + ecx, 0xC) + ebx;
                ecx = (((ebx ^ edx) & edi) ^ ebx) + 0x21E1CDE6 + BitConverter.ToUInt32(input, 0x24);

                esi = RotateLeft(esi + ecx, 0x5) + edx;
                ecx = (((edx ^ esi) & ebx) ^ edx) + 0xC33707D6 + BitConverter.ToUInt32(input, 0x38);

                edi = RotateLeft(edi + ecx, 0x9) + esi;
                ecx = (((edi ^ esi) & edx) ^ esi) + 0xF4D50D87 + BitConverter.ToUInt32(input, 0xC);
                savedEDI = edi;

                ebx = RotateLeft(ebx + ecx, 0xE) + edi;
                ecx = (((edi ^ ebx) & esi) ^ edi) + 0x455A14ED + BitConverter.ToUInt32(input, 0x20);

                edx = RotateRight(edx + ecx, 0xC) + ebx;
                ecx = (((ebx ^ edx) & edi) ^ ebx) + BitConverter.ToUInt32(input, 0x34);

                savedEDX = edx;
                edi = (esi - 0x561C16FB) + ecx;

                esi = savedEDI + 0xFCEFA3F8;
                edi = RotateLeft(edi, 0x5) + edx;
                ecx = (((edx ^ edi) & ebx) ^ edx) + BitConverter.ToUInt32(input, 0x8);

                esi = RotateLeft(esi + ecx, 0x9) + edi;
                ecx = (((esi ^ edi) & savedEDX) ^ edi) + 0x676F02D9 + BitConverter.ToUInt32(input, 0x1C);
                savedESI = esi;

                ebx = RotateLeft(ebx + ecx, 0xE) + esi;
                edx = esi ^ ebx;
                ecx = ((edx & edi) ^ esi) + BitConverter.ToUInt32(input, 0x30);
                savedEBX = ebx;

                esi = savedEDX + 0x8D2A4C8A;
                esi = RotateRight(esi + ecx, 0xC) + ebx;
                edx = (edx ^ esi) + 0xFFFA3942 + BitConverter.ToUInt32(input, 0x14);

                edi = RotateLeft(edi + edx, 0x4) + esi;
                ecx = ((ebx ^ esi) ^ edi) + BitConverter.ToUInt32(input, 0x20);
                ebx = savedESI + 0x8771F681;

                ebx = RotateLeft(ebx + ecx, 0xB) + edi;
                ecx = ((ebx ^ esi) ^ edi) + BitConverter.ToUInt32(input, 0x2C);
                esi = esi + 0xFDE5380C;

                ecx = RotateLeft((ecx + (savedEBX + 0x6D9D6122)), 0x10) + ebx;
                edx = ebx ^ ecx;
                savedECX = ecx;
                ecx = (edx ^ edi) + BitConverter.ToUInt32(input, 0x38);
                edi = edi + 0xA4BEEA44;

                ecx = RotateRight(ecx + esi, 0x9) + savedECX;
                edx = (edx ^ ecx) + BitConverter.ToUInt32(input, 0x4);
                esi = savedECX;
                savedECX = ecx;

                edx = RotateLeft(edx + edi, 0x4) + ecx;
                edi = savedECX;
                savedEDX = edx;
                ecx = ((esi ^ edi) ^ edx) + 0x4BDECFA9 + BitConverter.ToUInt32(input, 0x10);
                esi = esi + 0xF6BB4B60;

                ebx = RotateLeft(ebx + ecx, 0xB) + edx;
                ecx = ((ebx ^ edi) ^ edx) + BitConverter.ToUInt32(input, 0x1C);
                edi = edi + 0xBEBFBC70;

                ecx = RotateLeft(ecx + esi, 0x10);
                esi = savedEDX;

                ecx = ecx + ebx;
                edx = ebx ^ ecx;
                savedECX = ecx;
                ecx = (edx ^ esi) + BitConverter.ToUInt32(input, 0x28);
                esi = esi + 0x289B7EC6;

                ecx = ecx + edi;
                edi = savedECX;
                ecx = RotateRight(ecx, 0x9) + edi;
                savedECX = ecx;
                edx = (edx ^ ecx) + BitConverter.ToUInt32(input, 0x34);

                edx = RotateLeft(edx + esi, 0x4) + ecx;
                esi = savedECX;
                savedEDX = edx;
                ecx = ((edi ^ esi) ^ edx) + 0xEAA127FA + BitConverter.ToUInt32(input, 0x0);

                ebx = RotateLeft(ebx + ecx, 0xB) + edx;
                ecx = ((ebx ^ esi) ^ edx) + BitConverter.ToUInt32(input, 0xC);

                esi = esi + 0x4881D05;
                ecx = RotateLeft(ecx + edi + 0xD4EF3085, 0x10) + ebx;
                edi = savedEDX;
                edx = ebx ^ ecx;
                savedECX = ecx;
                ecx = (edx ^ edi) + BitConverter.ToUInt32(input, 0x18);

                edi = edi + 0xD9D4D039;
                esi = esi + ecx;
                ecx = savedECX;

                esi = RotateRight(esi, 0x9) + ecx;
                edx = (edx ^ esi) + BitConverter.ToUInt32(input, 0x24);

                edi = RotateLeft(edi + edx, 0x4) + esi;
                ecx = ((ecx ^ esi) ^ edi) + 0xE6DB99E5 + BitConverter.ToUInt32(input, 0x30);

                ebx = RotateLeft(ebx + ecx, 0xB) + edi;
                ecx = ((ebx ^ esi) ^ edi) + BitConverter.ToUInt32(input, 0x3C);

                edx = RotateLeft(savedECX + 0x1FA27CF8 + ecx, 0x10) + ebx;
                ecx = ((ebx ^ edx) ^ edi) + 0xC4AC5665 + BitConverter.ToUInt32(input, 0x8);

                esi = RotateRight((esi + ecx), 0x9) + edx;
                ecx = ((~ebx | esi) ^ edx) + 0xF4292244 + BitConverter.ToUInt32(input, 0x0);

                edi = RotateLeft(edi + ecx, 0x6) + esi;
                ecx = ((~edx | edi) ^ esi) + 0x432AFF97 + BitConverter.ToUInt32(input, 0x1C);

                ebx = RotateLeft(ebx + ecx, 0xA) + edi;
                ecx = ((~esi | ebx) ^ edi) + 0xAB9423A7 + BitConverter.ToUInt32(input, 0x38);

                edx = RotateLeft(edx + ecx, 0xF) + ebx;
                ecx = ((~edi | edx) ^ ebx) + 0xFC93A039 + BitConverter.ToUInt32(input, 0x14);

                esi = RotateRight(esi + ecx, 0xB) + edx;
                ecx = ((~ebx | esi) ^ edx) + 0x655B59C3 + BitConverter.ToUInt32(input, 0x30);

                edi = RotateLeft(edi + ecx, 0x6) + esi;
                ecx = ((~edx | edi) ^ esi) + 0x8F0CCC92 + BitConverter.ToUInt32(input, 0xC);

                ebx = RotateLeft(ebx + ecx, 0xA) + edi;
                ecx = ((~esi | ebx) ^ edi) + 0xFFEFF47D + BitConverter.ToUInt32(input, 0x28);

                edx = RotateLeft(edx + ecx, 0xF) + ebx;
                ecx = ((~edi | edx) ^ ebx) + 0x85845DD1 + BitConverter.ToUInt32(input, 0x4);

                esi = RotateRight(esi + ecx, 0xB) + edx;
                ecx = ((~ebx | esi) ^ edx) + 0x6FA87E4F + BitConverter.ToUInt32(input, 0x20);

                edi = RotateLeft(edi + ecx, 0x6) + esi;
                ecx = ((~edx | edi) ^ esi) + 0xFE2CE6E0 + BitConverter.ToUInt32(input, 0x3C);

                ebx = RotateLeft(ebx + ecx, 0xA) + edi;
                ecx = ((~esi | ebx) ^ edi) + 0xA3014314 + BitConverter.ToUInt32(input, 0x18);
                savedEBX = ebx;

                edx = RotateLeft(edx + ecx, 0xF) + ebx;
                ecx = ((~edi | edx) ^ ebx) + 0x4E0811A1 + BitConverter.ToUInt32(input, 0x34);
                savedEDX = edx;

                esi = RotateRight(esi + ecx, 0xB) + edx;
                ecx = ((~ebx | esi) ^ edx) + BitConverter.ToUInt32(input, 0x10);
                savedESI = esi;

                ebx = RotateLeft((edi - 0x8AC817E) + ecx, 0x6) + esi;
                ecx = ((~edx | ebx) ^ esi) + BitConverter.ToUInt32(input, 0x2C);
                edi = savedEBX + 0xBD3AF235;

                edi = RotateLeft(edi + ecx, 0xA) + ebx;
                ecx = ((~esi | edi) ^ ebx) + BitConverter.ToUInt32(input, 0x8);
                esi = savedEDX + 0x2AD7D2BB;

                esi = RotateLeft(esi + ecx, 0xF) + edi;
                ecx = ((~ebx | esi) ^ edi) + BitConverter.ToUInt32(input, 0x24);

                edx = RotateRight(savedESI + 0xEB86D391 + ecx, 0xB) + esi;
                ecx = num0 + ebx;

                num0 = ecx;
                num1 = num1 + edx;
                num2 = num2 + esi;
                num3 = num3 + edi;

                if ((((ulong)counter + 1) & 0x100000000) != 0)
                {
                    // workaround for infinite loop?
                    break;
                }

            }

            return ReverseBytes(num0).ToString("x" + 8)
                 + ReverseBytes(num1).ToString("x" + 8)
                 + ReverseBytes(num2).ToString("x" + 8)
                 + ReverseBytes(num3).ToString("x" + 8);
        }

        //
        // other functions
        //
        static uint[] __allmul(uint a1, uint a2, uint a3, uint a4)
        {
            ulong numeric0 = a1;
            ulong numeric1 = a2;
            uint mulNumeric0Low = 0;
            uint mulNumeric0High = 0;
            uint mulNumeric1Low = 0;

            if (numeric1 == 0)
            {
                numeric0 *= (ulong)a3;
                mulNumeric0Low = (uint)numeric0;
                mulNumeric0High = (uint)(numeric0 >> 0x20);
                return new uint[] { mulNumeric0Low, mulNumeric0High };
            }
            else
            {
                numeric1 *= (ulong)a3;
                mulNumeric1Low = (uint)numeric1;

                mulNumeric1Low += ((uint)numeric0 * a4);

                numeric0 *= (ulong)a3;
                mulNumeric0High = (uint)(numeric0 >> 0x20);
                mulNumeric0Low = (uint)numeric0;

                return new uint[] { mulNumeric0Low, mulNumeric0High + mulNumeric1Low };

            }
        }

        static uint[] __aullshr(uint eax, uint edx, int cl)
        {
            uint[] result = new uint[2];
            result[0] = Shrd(eax, edx, cl);
            result[1] = edx >> cl;

            return result;
        }


        static uint Shld(uint a, uint b, int c)
        {
            if (c == 0)
                return a;

            else
                return (a << c) | (b >> (32 - c));
        }

        static uint Shrd(uint a, uint b, int c)
        {
            if (c == 0)
                return a;

            else
                return (a >> c) | (b << (32 - c));
        }

        static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        static uint RotateRight(uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }

        static uint ReverseBytes(uint value)
        {
            return (value & 0x000000FFU) << 24
                  | (value & 0x0000FF00U) << 8
                  | (value & 0x00FF0000U) >> 8
                  | (value & 0xFF000000U) >> 24;
        }


    }

}
