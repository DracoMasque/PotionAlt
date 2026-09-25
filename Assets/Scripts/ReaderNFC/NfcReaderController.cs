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
        private string[] current_uid;
        private string[] _readerNames;
        public Chaudron _chaudronComponent;
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
                _readerNames = readerNames;
                current_uid = new string[readerNames.Length];
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
            foreach (var readerName in readerNames)
            {
                print(readerName + " started");
                _monitor.Start(readerNames);
            }
        }

        private void OnCardInserted(object sender, CardStatusEventArgs eventArgs)
        {
            int capteur_i = Array.IndexOf(_readerNames, eventArgs.ReaderName);
            print(capteur_i);
            print(current_uid);
            try
            {
                using var context = ContextFactory.Instance.Establish(SCardScope.System);
                using var isoReader = new IsoReader(context, eventArgs.ReaderName, SCardShareMode.Shared, SCardProtocol.Any, false);

                var card = new MifareCard(isoReader);
                var uid = card.GetData();
                
                if (uid == null)
                    return;

                current_uid[capteur_i] = BitConverter.ToString(uid);
                _mainThreadContext.Post(_ => { Debug.Log("tya une carte de détectée : bip " + current_uid); }, null);
                
            }
            catch (RemovedCardException exception)
            {
                _mainThreadContext.Post(_ => { Debug.LogError("tyé flash pour passer aussi vite fdp ? " + exception); }, null);
                _mainThreadContext.Post(_ => { Debug.Log("tya pas une carte de détectée : pas-bip "); }, null);
                if (_chaudronComponent)
                {
                    _mainThreadContext.Post(_ => {_chaudronComponent.RetireObjects(current_uid[capteur_i]);}, null);
                }
                
            }
            catch (Exception exception)
            {
                _mainThreadContext.Post(_ => {Debug.LogError("jsp c'est quoi le problème cette fois mon coco mais y'a un truc qui va pas... " + exception); }, null);
            }
            if (_chaudronComponent)
            {
                _mainThreadContext.Post(_ => {_chaudronComponent.AjouteObjects(current_uid[capteur_i]);}, null);
            }
        }
        
        private void OnCardRemoved(object sender, CardStatusEventArgs e)
        {
            int capteur_i = Array.IndexOf(_readerNames, e.ReaderName);
            _mainThreadContext.Post(_ => { Debug.Log("tya pas une carte de détectée : pas-bip "); }, null);
            if (_chaudronComponent)
            {
                _mainThreadContext.Post(_ => {_chaudronComponent.RetireObjects(current_uid[capteur_i]);}, null);
            }
            current_uid[capteur_i] = "";
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