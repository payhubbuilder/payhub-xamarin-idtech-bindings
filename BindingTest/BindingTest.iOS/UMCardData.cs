using System;
using System.Runtime.InteropServices;
using Foundation;

namespace BindingTest.iOS
{
    public class UMCardData
    {
        public NSData ByteData;
        public bool IsValid;
        public bool IsEncrypted;
        public bool IsAesEncrypted;

        public NSData Track1;
        public NSData Track2;
        public NSData Track3;

        public NSData Track1_Encrypted;
        public NSData Track2_Encrypted;
        public NSData Track3_Encrypted;

        public NSData SerialNumber;
        public NSData KSN;

        public UMCardData(NSData cardData)
        {
            if (cardData != null)
            {

                byte[] bytes = new byte[cardData.Length];

                Marshal.Copy(cardData.Bytes, bytes, 0, (int)cardData.Length);
                nuint len = cardData.Length;

                if (len > 1)
                {
                    if (bytes[0] == '\x02')
                    {
                        //Encrypted Swipe
                        IsEncrypted = true;
                        VerifyAndParseEncrypted(cardData);
                    }
                    else
                    {
                        //Unencrypted Swipe
                        IsEncrypted = false;
                        VerifyAndParseUnEncrypted(cardData);
                    }
                }
            }
        }

        private void VerifyAndParseEncrypted(NSData cardData)
        {
            NSData[] tracks = new NSData[3];
            NSData[] encryptedTracks = new NSData[3];
            NSData serialNumber = null;
            NSData ksn = null;

            bool isAES = false;
            bool isSwipeDataValid = false;

            int len = (int)cardData.Length;
            byte[] bytes = new byte[len];
            Marshal.Copy(cardData.Bytes, bytes, 0, len);

            if (len < 6)
                return;
            if (bytes[0] != '\x02' ||
              bytes[len - 1] != '\x03')
                return;

            int payloadLen = (bytes[2] << 8) + (bytes[1]);
            if (payloadLen + 6 != len)
                return;

            byte checksum = 0, checkXor = 0;
            for (int i = 3; i < (int)(len - 3); i++)
            {
                checkXor ^= bytes[i];
                checksum += bytes[i];
            }

            if (bytes[len - 2] != checksum || bytes[len - 3] != checkXor)
                return;

            //Get Track Lengths
            int idx = 5;
            int[] trackLens = new int[3];
            if ((idx + 3) > (len - 3)) goto stopParse;
            for (int i = 0; i < 3; i++)
                trackLens[i] = bytes[idx + i];

            //Get Masked Track
            int trackLensSum = 0;
            idx = 10;
            for (int i = 0; i < 3; i++)
            {
                if (trackLens[i] == 0 || !isBitSet(bytes[8], i))
                    continue;
                if ((idx + trackLens[i]) > (len - 3)) goto stopParse;
                tracks[i] = cardData.Subdata(new NSRange(idx, trackLens[i]));
                idx += trackLens[i];
                trackLensSum += trackLens[i];
            }

            //Determine Encryption Type (TDES or AES)
            idx = 8;
            if ((idx + 1) > (len - 3)) goto stopParse;
            int encType = ((bytes[idx] >> 4) & 0x03);
            if (encType == 0x00)
                isAES = false;
            else if (encType == 0x01)
                isAES = true;
            else
                goto stopParse;

            //Get Encrypted Section
            int[] trackLensEnc = new int[3];
            int encryptionBlockSize = (isAES ? 16 : 8);
            for (int i = 0; i < 3; i++)
            {
                trackLensEnc[i] = (int)Math.Ceiling(trackLens[i] / (double)encryptionBlockSize) * encryptionBlockSize;
            }

            int trackLensSumEnc = 0;
            idx = 10 + trackLensSum;
            for (int i = 0; i < 3; i++)
            {
                if (trackLensEnc[i] == 0 || !isBitSet(bytes[9], i))
                    continue;
                if ((idx + trackLensEnc[i]) > (len - 3)) goto stopParse;
                encryptedTracks[i] = cardData.Subdata(new NSRange(idx, trackLensEnc[i]));
                idx += trackLensEnc[i];
                trackLensSumEnc += trackLensEnc[i];

            }

            //Get KSN
            if ((isBitSet(bytes[9], 7)) &&
              (isBitSet(bytes[9], 0) ||
            isBitSet(bytes[9], 1) ||
            isBitSet(bytes[9], 2)))
            {
                idx = (int)(len - 3 - 10);
                if (idx < 10 + trackLensSum + trackLensSumEnc)
                    goto stopParse;
                ksn = cardData.Subdata(new NSRange(idx, 10));
            }

            //Get Serial Number
            if (isBitSet(bytes[8], 7))
            {
                idx = (int)len - 3 - 10 - (ksn != null ? 10 : 0);
                if (idx < 10 + trackLensSum + trackLensSumEnc)
                    goto stopParse;
                serialNumber = cardData.Subdata(new NSRange(idx, 10));
            }


            isSwipeDataValid = true;


            stopParse:
            IsValid = isSwipeDataValid;
            IsAesEncrypted = isAES;
            Track1 = tracks[0];
            Track2 = tracks[1];
            Track3 = tracks[2];
            Track1_Encrypted = encryptedTracks[0];
            Track2_Encrypted = encryptedTracks[1];
            Track3_Encrypted = encryptedTracks[2];
            SerialNumber = serialNumber;
            KSN = ksn;
        }

        private void VerifyAndParseUnEncrypted(NSData cardData)
        {
        }


        bool isBitSet(byte b, int bitIndex)
        {
            byte mask = (byte)(1 << bitIndex);
            return (b & mask) != 0;
        }


    }
}
