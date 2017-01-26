using System;
using System.Text;

namespace TwitchLib.TwitchIRC
{
    internal class PrimaryOrFallbackEncoding : Encoding
    {
        public Encoding PrimaryEncoding { get; private set; }
        public Encoding FallbackEncoding { get; private set; }

        public PrimaryOrFallbackEncoding(Encoding primary, Encoding fallback)
        {
            try {
                PrimaryEncoding = Encoding.GetEncoding(primary.WebName, new EncoderExceptionFallback(), new DecoderExceptionFallback());
            } catch (ArgumentException) {
                if (!(primary.EncoderFallback is EncoderExceptionFallback)) {
                    throw new System.ArgumentException("a custom primary encoding's encoder fallback must be an EncoderExceptionFallback");
                }
                if (!(primary.DecoderFallback is DecoderExceptionFallback)) {
                    throw new System.ArgumentException("a custom primary encoding's decoder fallback must be a DecoderExceptionFallback");
                }
            }

            FallbackEncoding = fallback;
        }

        public override int GetByteCount(char[] chars, int index, int count)
        {
            try {
                return PrimaryEncoding.GetByteCount(chars, index, count);
            } catch (EncoderFallbackException) {
                return FallbackEncoding.GetByteCount(chars, index, count);
            }
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            try {
                return PrimaryEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
            } catch (EncoderFallbackException) {
                return FallbackEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
            }
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            try {
                return PrimaryEncoding.GetCharCount(bytes, index, count);
            } catch (DecoderFallbackException) {
                return FallbackEncoding.GetCharCount(bytes, index, count);
            }
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            try {
                return PrimaryEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
            } catch (DecoderFallbackException) {
                return FallbackEncoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
            }
        }

        public override int GetMaxByteCount(int charCount)
        {
            try {
                int pri = PrimaryEncoding.GetMaxByteCount(charCount);
                int fab = FallbackEncoding.GetMaxByteCount(charCount);
                return Math.Max(pri, fab);
            } catch (EncoderFallbackException) {
                return FallbackEncoding.GetMaxByteCount(charCount);
            }
        }

        public override int GetMaxCharCount(int byteCount)
        {
            try {
                int pri = PrimaryEncoding.GetMaxCharCount(byteCount);
                int fab = FallbackEncoding.GetMaxCharCount(byteCount);
                return Math.Max(pri, fab);
            } catch (DecoderFallbackException) {
                return FallbackEncoding.GetMaxCharCount(byteCount);
            }
        }

        public override byte[] GetPreamble()
        {
            return PrimaryEncoding.GetPreamble();
        }
    }
}

