using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Text;

public sealed class Crypto
{

    private const int TokenSizeInBytes = 16;
    private const int Pbkdf2Count = 1000;
    private const int Pbkdf2SubkeyLength = 256 / 8;

    private const int SaltSize = 128 / 8;
    static internal string GenerateToken()
    {
        byte[] TokenBytes = new byte[TokenSizeInBytes];
        using (RNGCryptoServiceProvider Prng = new RNGCryptoServiceProvider())
        {
            Prng.GetBytes(TokenBytes);
            return Convert.ToBase64String(TokenBytes);
        }
    }

    public static string GenerateSalt(int ByteLength = SaltSize)
    {
        byte[] Buff = new byte[ByteLength];
        using (RNGCryptoServiceProvider Prng = new RNGCryptoServiceProvider())
        {
            Prng.GetBytes(Buff);
        }
        return Convert.ToBase64String(Buff);
    }

    public static string Hash(string Input, string Algorithm = "sha256")
    {
        if (string.IsNullOrEmpty(Input))
        {
            throw new ArgumentNullException("Hash input null");
        }
        return Hash(Encoding.UTF8.GetBytes(Input), Algorithm);
    }

    public static string Hash(byte[] Input, string Algorithm = "sha256")
    {
        if (Input == null)
        {
            throw new ArgumentNullException("Hash input null");
        }

        using (HashAlgorithm Alg = HashAlgorithm.Create(Algorithm))
        {
            if (Alg != null)
            {
                byte[] HashData = Alg.ComputeHash(Input);
                return BinaryToHex(HashData);
            }
            else
            {
                throw new InvalidOperationException(string.Format(string.Format("Not supported hash algorhitm {0}", Algorithm)));
            }
        }
    }

    public static string SHA1(string Input)
    {
        return Hash(Input, "sha1");
    }

    public static string SHA256(string Input)
    {
        return Hash(Input, "sha256");
    }

    //=======================
    //HASHED PASSWORD FORMATS
    //=======================
    //Version 0:
    //PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
    //See also: SDL crypto guidelines v5.1, Part III)
    //Format: { 0x00, salt, subkey }      

    public static string HashPassword(string Password)
    {
        if (string.IsNullOrEmpty(Password))
        {
            throw new ArgumentNullException("Password input null");
        }

        byte[] Salt = null;
        byte[] SubKey = null;
        using (var DeriveBytes = new Rfc2898DeriveBytes(Password, SaltSize, Pbkdf2Count))
        {
            Salt = DeriveBytes.Salt;
            SubKey = DeriveBytes.GetBytes(Pbkdf2SubkeyLength);
        }

        byte[] OutputBytes = new byte[1 + SaltSize + (Pbkdf2SubkeyLength - 1) + 1];
        Buffer.BlockCopy(Salt, 0, OutputBytes, 1, SaltSize);
        Buffer.BlockCopy(SubKey, 0, OutputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
        return Convert.ToBase64String(OutputBytes);
    }

    //HashedPassword must be of the format of HashWithPassword (Salt + Hash(Salt+Input)
    public static bool VerifyHashedPassword(string HashedPassword, string Password)
    {
        if (string.IsNullOrEmpty(HashedPassword))
        {
            throw new ArgumentNullException("HashedPassword is null");
        }
        if (string.IsNullOrEmpty(Password))
        {
            throw new ArgumentNullException("Password is null");
        }

        byte[] HashedPasswordBytes = Convert.FromBase64String(HashedPassword);

        if (HashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubkeyLength) || HashedPasswordBytes[0] != Convert.ToByte(0x0))
        {
            //Wrong length or version header.
            return false;
        }

        byte[] Salt = new byte[SaltSize];
        Buffer.BlockCopy(HashedPasswordBytes, 1, Salt, 0, SaltSize);
        byte[] StoredSubkey = new byte[Pbkdf2SubkeyLength];
        Buffer.BlockCopy(HashedPasswordBytes, 1 + SaltSize, StoredSubkey, 0, Pbkdf2SubkeyLength);

        byte[] GeneratedSubkey = null;
        using (var DeriveBytes = new Rfc2898DeriveBytes(Password, Salt, Pbkdf2Count))
        {
            GeneratedSubkey = DeriveBytes.GetBytes(Pbkdf2SubkeyLength);
        }
        return ByteArraysEqual(StoredSubkey, GeneratedSubkey);
    }

    static internal string BinaryToHex(byte[] Data)
    {
        char[] Hex = new char[Data.Length * 2];

        for (int Iter = 0; Iter <= Data.Length - 1; Iter++)
        {
            byte HexChar = Convert.ToByte(Data[Iter] >> 4);
            Hex[Iter * 2] = Convert.ToChar(HexChar > 9 ? HexChar + 0x37 : HexChar + 0x30);
            HexChar = Convert.ToByte(Data[Iter] & 0xf);
            Hex[Iter * 2 + 1] = Convert.ToChar(HexChar > 9 ? HexChar + 0x37 : HexChar + 0x30);
        }
        return new string(Hex);
    }

    //Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
    [MethodImpl(MethodImplOptions.NoOptimization)]
    private static bool ByteArraysEqual(byte[] A, byte[] B)
    {
        if (object.ReferenceEquals(A, B))
        {
            return true;
        }

        if (A == null || B == null || A.Length != B.Length)
        {
            return false;
        }

        bool AreSame = true;
        for (int i = 0; i <= A.Length - 1; i++)
        {
            AreSame = AreSame & (A[i] == B[i]);
        }
        return AreSame;
    }

}