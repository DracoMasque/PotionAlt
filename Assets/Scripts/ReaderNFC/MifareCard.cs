using System;
using System.Diagnostics;
using PCSC;
using PCSC.Iso7816;

namespace AltControllerSettings
{
    public enum KeyStructure : byte {
        VolatileMemory = 0x00,
        NonVolatileMemory = 0x20
    }
    public enum KeyType : byte {
        KeyA = 0x60,
        KeyB = 0x61
    }

    public class MifareCard 
    {
        
        private const byte _CustomCla = 0xFF;
        private readonly IIsoReader _isoReader;

        public MifareCard(IIsoReader isoReader) 
        {
            _isoReader = isoReader ?? throw new ArgumentNullException(nameof(isoReader));
        }

        public byte[] GetData() 
        {
            var getDataCmd = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.GetData,
                P1 = 0x00,
                P2 = 0x00
            };

            var response = _isoReader.Transmit(getDataCmd);
            return IsSuccess(response) ? response.GetData() ?? Array.Empty<byte>() : null;
        }

        public bool LoadKey(KeyStructure keyStructure, byte keyNumber, byte[] key) 
        {
            var loadKeyCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.ExternalAuthenticate,
                P1 = (byte)keyStructure,
                P2 = keyNumber,
                Data = key
            };

            Debug.WriteLine($"Load Authentication Keys: {BitConverter.ToString(loadKeyCmd.ToArray())}");
            var response = _isoReader.Transmit(loadKeyCmd);
            Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2}");

            return IsSuccess(response);
        }


        public bool Authenticate(byte msb, byte lsb, KeyType keyType, byte keyNumber) 
        {
            var authBlock = new GeneralAuthenticate 
            {
                Msb = msb,
                Lsb = lsb,
                KeyNumber = keyNumber,
                KeyType = keyType
            };

            var authKeyCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.InternalAuthenticate,
                P1 = 0x00,
                P2 = 0x00,
                Data = authBlock.ToArray()
            };

            Debug.WriteLine($"General Authenticate: {BitConverter.ToString(authKeyCmd.ToArray())}");
            var response = _isoReader.Transmit(authKeyCmd);
            Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2}");

            return response.SW1 == 0x90 && response.SW2 == 0x00;
        }

        public byte[] ReadBinary(byte msb, byte lsb, int size) 
        {
            unchecked 
            {
                var readBinaryCmd = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any) 
                {
                    CLA = _CustomCla,
                    Instruction = InstructionCode.ReadBinary,
                    P1 = msb,
                    P2 = lsb,
                    Le = size
                };

                Debug.WriteLine($"Read Binary: {BitConverter.ToString(readBinaryCmd.ToArray())}");
                var response = _isoReader.Transmit(readBinaryCmd);
                Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2} Data: {BitConverter.ToString(response.GetData())}");

                return IsSuccess(response) ? response.GetData() ?? Array.Empty<byte>() : null;
            }
        }

        public bool UpdateBinary(byte msb, byte lsb, byte[] data) 
        {
            var updateBinaryCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.UpdateBinary,
                P1 = msb,
                P2 = lsb,
                Data = data
            };

            Debug.WriteLine($"Update Binary: {BitConverter.ToString(updateBinaryCmd.ToArray())}");
            var response = _isoReader.Transmit(updateBinaryCmd);
            Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2}");

            return IsSuccess(response);
        }

        public bool Decrement(byte msb, byte lsb, byte[] data) 
        {
            var decrementCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.Decrement,
                P1 = 0x00,
                P2 = lsb,
                Data = data,
            };


            Debug.WriteLine($"Decrement Binary: {BitConverter.ToString(decrementCmd.ToArray())}");
            var response = _isoReader.Transmit(decrementCmd);
            Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2}");

            return IsSuccess(response);
        }


        public bool Increment(byte msb, byte lsb, byte[] data) 
        {
            var incrementCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any) 
            {
                CLA = _CustomCla,
                Instruction = InstructionCode.Increment,
                P1 = 0x00,
                P2 = lsb,
                Data = data
            };


            Debug.WriteLine($"Increment Binary: {BitConverter.ToString(incrementCmd.ToArray())}");
            var response = _isoReader.Transmit(incrementCmd);
            Debug.WriteLine($"SW1 SW2 = {response.SW1:X2} {response.SW2:X2}");

            return IsSuccess(response);
        }

        private static bool IsSuccess(Response response)
        {
            return response.SW1 == (byte)SW1Code.Normal && response.SW2 == 0x00;
        }
    }

    public class GeneralAuthenticate 
    {
        public byte Version { get; } = 0x01;
        public byte Msb { get; set; }
        public byte Lsb { get; set; }
        public KeyType KeyType { get; set; }
        public byte KeyNumber { get; set; }

        public byte[] ToArray() 
        {
            return new[] {Version, Msb, Lsb, (byte)KeyType, KeyNumber};
        }
    }
}