using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PCSC;
using PCSC.Monitoring;

public class LecturedePuces : MonoBehaviour
{
    /*ContextFactory contextFactory = ContextFactory.Instance;
        using (var context = contextFactory.Establish(SCardScope.System)) {
        // exemple venant du site du plugin, ilf aut toujours un context valid pour faire nos trucs
    }*/

    public static void ReadReaderAttribute()
    {
        using (var ctx = ContextFactory.Instance.Establish(SCardScope.System))
        {
            using (var reader = ctx.ConnectReader("Je crois on a besoin du nom...", SCardShareMode.Shared,
                       SCardProtocol.Any))
            {
                var cardAtr = reader.GetAttrib(SCardAttribute.AtrString);
                Debug.Log("ATR: " + cardAtr);
            }
        }
    }
    public static void MonitorReaderEvent()
    {
        var monitorFactory = MonitorFactory.Instance;
        var monitor = monitorFactory.Create(SCardScope.System);
        
        //connect events here..
        monitor.StatusChanged += (sender, args) =>
            Debug.Log($"New State: {args.NewState}"); 
        monitor.Start("Le nom du reader sans doute");
        //Il faut que je mette quelque chose ici pour enlever le mnonitor
        //mais j'ai la flemme de reregarder comment reagire quand t'appuis sur une touche
        
        //monitor.Cancel();
        //monitor.Dispose();
    }

}
