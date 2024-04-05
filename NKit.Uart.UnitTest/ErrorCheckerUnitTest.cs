using IotDevKit;

namespace NKit.Uart.UnitTest
{
    public class ErrorCheckerUnitTest
    {
        [Fact]
        public void LrcTestMethod()
        {
            var lrc = DataVerifier.LrcModbus(new[] { "01", "17", "00", "00", "00", "02", "00", "0D", "00", "01", "02", "F6", "4D" }).ToString("X2");
            Assert.Equal<string>(lrc, "93");
            lrc = DataVerifier.LrcModbus(new[] { ":", "01", "17", "00", "00", "00", "02", "00", "0D", "00", "01", "02", "F6", "4D" }, 1, 13).ToString("X2");
            Assert.Equal(lrc, "93");
        }

        [Fact]
        public void CrcTestMethod()
        {
            var crc = DataVerifier.Crc16Modbus(new byte[] { 0xAB, 0xCD, 0xEF, 0xDA, 0x27 });
            Assert.Equal<byte>(crc.high, 0xFB);
            Assert.Equal<byte>(crc.low, 0x15);
            crc = DataVerifier.Crc16Modbus(new byte[] { 0xAB, 0xCD, 0xEF, 0xDA, 0x27 }, 1, 3);
            Assert.Equal<byte>(crc.high, 0x94);
            Assert.Equal<byte>(crc.low, 0x2D);
        }

        [Fact]
        public void XorTestMethod()
        {
            var ret = DataVerifier.Xor(new byte[] { 0x0A, 0x06, 0x64, 0x00, 0X68 });
            Assert.Equal<byte>(ret, 0x00);
            ret = DataVerifier.Xor(new byte[] { 0xFF, 0x0A, 0x06, 0x64, 0x00, 0X68, 0x99 }, 1, 5);
            Assert.Equal<byte>(ret, 0x00);
        }
    }
}