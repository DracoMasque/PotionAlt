using System;
using System.Collections.Generic;
using System.Threading;
using PCSC;
using PCSC.Exceptions;
using PCSC.Iso7816;
using UnityEngine;
using PCSC.Monitoring;

namespace AltControllerSettings
{
    public class NfcReaderManager : MonoBehaviour
    {
        
        private static readonly byte[] _DataToWrite = 
        {
            0x0F, 0x0E, 0x0D, 0x0C, 0x0B, 0x0A, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00
        };

        private const byte _Msb = 0x00;
        private const byte _Lsb = 0x08;
        
        private ISCardMonitor _monitor;
        private SynchronizationContext _mainThreadContext;

        private void Start()
        {
            
            _mainThreadContext = SynchronizationContext.Current;

            string[] readerNames;

            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                readerNames = context.GetReaders();
            }

            if (IsEmpty(readerNames))
            {
                Debug.LogWarning("tya un lecteur de détecté :)");
                return;
            }

            foreach (string readerName in readerNames)
            {
                Debug.Log("Reader : " + readerName);
            }

            InitMonitor(readerNames);
        }
        
        private void InitMonitor(string[] readerNames)
        {
            _monitor = MonitorFactory.Instance.Create(SCardScope.System);
            _monitor.CardInserted += OnCardInserted;
            _monitor.CardRemoved += OnCardRemoved;
            _monitor.StatusChanged += OnStatusChanged;
            _monitor.Start(readerNames[0]);
        }
        
        private void OnCardInserted(object sender, CardStatusEventArgs eventArgs)
        {
            try
            {
                using var context = ContextFactory.Instance.Establish(SCardScope.System);
                using var isoReader = new IsoReader(context, eventArgs.ReaderName, SCardShareMode.Shared, SCardProtocol.Any, false);

                var card = new MifareCard(isoReader);
                var uid = card.GetData();
                
                if (uid == null)
                    return;

                string uidString = BitConverter.ToString(uid);
                _mainThreadContext.Post(_ => { Debug.Log("tya une carte de détectée : bip " + uidString); }, null);
            }
            catch (RemovedCardException exception)
            {
                _mainThreadContext.Post(_ => { Debug.LogError("tyé flash pour passer aussi vite fdp ? " + exception); }, null);
            }
            catch (Exception exception)
            {
                _mainThreadContext.Post(_ => {Debug.LogError("jsp c'est quoi le problème cette fois mon coco mais y'a un truc qui va pas... " + exception); }, null);
            }
        }
        
        private void OnCardRemoved(object sender, CardStatusEventArgs e)
        {
            _mainThreadContext.Post(_ => { Debug.Log("tya pas une carte de détectée : pas-bip "); }, null);
        }
        
        private void OnDestroy()
        {
            if (_monitor == null) 
                return;
            
            _monitor.Cancel();
            _monitor.Dispose();
        }

        private static bool IsEmpty(ICollection<string> readerNames)
        {
            return readerNames == null || readerNames.Count < 1;
        }
        
    }
}


/*var loadKeySuccessful = card.LoadKey(
                        KeyStructure.NonVolatileMemory,
                        0x00, // first key slot
                        new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF} // key
                    );

                    if (!loadKeySuccessful) {
                        throw new Exception("LOAD KEY failed.");
                    }

                    var authSuccessful = card.Authenticate(MSB, LSB, KeyType.KeyA, 0x00);
                    if (!authSuccessful) {
                        throw new Exception("AUTHENTICATE failed.");
                    }

                    var result = card.ReadBinary(MSB, LSB, 16);
                    Console.WriteLine("Result (before BINARY UPDATE): {0}",
                        (result != null)
                            ? BitConverter.ToString(result)
                            : null);

                    var updateSuccessful = card.UpdateBinary(MSB, LSB, DATA_TO_WRITE);

                    if (!updateSuccessful) {
                        throw new Exception("UPDATE BINARY failed.");
                    }

                    result = card.ReadBinary(MSB, LSB, 16);
                    Console.WriteLine("Result (after BINARY UPDATE): {0}",
                        (result != null)
                            ? BitConverter.ToString(result)
                            : null);*/