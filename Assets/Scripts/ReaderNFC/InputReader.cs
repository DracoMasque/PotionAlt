using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class InputReader : MonoBehaviour
{
    static SerialPort sp = new SerialPort("COM5", 115200);
	List<string> detectedIds = new List<string>();
	
    void Start()
    {
	    if (sp.IsOpen)
	    {
		    sp.Close();
	    }
	    sp.Open();
	    sp.ReadTimeout = 1;
    }

   	void Update () 
   	{
		try{
			print (sp.ReadLine());
			//add some cooldown
		}
		catch(System.Exception){
		}
   	}

    void addToList(string text)
    {
	    if (text[0] == '-')
	    {
		    //reset list
	    }
		    
    }
    
}
